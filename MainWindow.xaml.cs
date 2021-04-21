namespace JsonEditorSharp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.Win32;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            JsonTreeView.Background = JsonTreeView.BorderBrush = StackPanelView.Background = (Brush) new BrushConverter().ConvertFrom("#121212");
        }

        private void ButtonBrowseFile_OnClick(object sender, RoutedEventArgs e)
        {
            // Clear TreeView.
            JsonTreeView.ItemsSource = null;
            JsonTreeView.Items.Clear();

            // Create OpenFileDialog.
            var openFileDialog = new OpenFileDialog { Filter = "JSON files (.json)|*.json" };

            // Display OpenFileDialog by calling ShowDialog method.
            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                // Get the selected file name.
                JsonFileName.Content = openFileDialog.FileName;

                // Read JSON raw file and convert to string.
                string jsonString = File.ReadAllText(JsonFileName.Content.ToString() ?? string.Empty);

                try
                {
                    // Parse JSON to JToken.
                    JToken token = JToken.Parse(jsonString);

                    // Add JSON native tree structure to TreeView.
                    var children = new List<JToken> { token };
                    JsonTreeView.ItemsSource = new List<JToken>(children);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not parse the JSON string:\r\n" + ex.Message);
                }
            }
        }

        private void ButtonExportJson_OnClick(object sender, RoutedEventArgs e)
        {
            if (JsonTreeView.ItemsSource == null)
            {
                // Empty file.
                return;
            }

            foreach (object root in JsonTreeView.ItemsSource)
            {
                dynamic json = JsonConvert.DeserializeObject(root.ToString() ?? string.Empty);
                string formattedJsonString = JsonConvert.SerializeObject(json, Formatting.Indented);
                File.WriteAllText(JsonFileName.Content.ToString() ?? string.Empty, formattedJsonString);
                return;
            }
        }

        private void ButtonRemoveNode_OnClick(object sender, RoutedEventArgs e)
        {
            if (((JToken) JsonTreeView.SelectedItem)?.Parent == null)
            {
                return;
            }

            ((JToken) JsonTreeView.SelectedItem).Remove();

            JsonTreeView.Items.Refresh();
            JsonTreeView.UpdateLayout();
        }

        private void KeyDownRemoveNode_OnKeyDelete(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (((JToken)JsonTreeView.SelectedItem)?.Parent == null)
                {
                    return;
                }

                ((JToken)JsonTreeView.SelectedItem).Remove();

                JsonTreeView.Items.Refresh();
                JsonTreeView.UpdateLayout();
            }
        }
    }
}
