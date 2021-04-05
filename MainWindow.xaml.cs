namespace JsonEditorSharp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.Win32;
    using System.Windows;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
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

            // Get the selected file name.
            if (result == true)
            {
                string fileName = openFileDialog.FileName;
                string jsonString = File.ReadAllText(fileName); 
                var children = new List<JToken>();

                try
                {
                    JToken token = JToken.Parse(jsonString);
                    children.Add(token);
                    JsonTreeView.ItemsSource = children;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not open the JSON string:\r\n" + ex.Message);
                }
            }
        }
    }
}
