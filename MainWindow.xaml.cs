namespace JsonEditorSharp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.Win32;
    using System.Windows;
    using Newtonsoft.Json.Linq;

    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
                string fileName = openFileDialog.FileName;

                // Read JSON raw file and convert to string.
                string jsonString = File.ReadAllText(fileName);

                try
                {
                    // Parse JSON to JToken.
                    JToken token = JToken.Parse(jsonString);

                    // Add JSON native tree structure to TreeView.
                    JsonTreeView.ItemsSource = new List<JToken>(token);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not parse the JSON string:\r\n" + ex.Message);
                }
            }
        }
    }
}
