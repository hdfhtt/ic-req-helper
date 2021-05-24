using System;
using System.Windows;
using System.Windows.Media;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace ic_req_helper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window // NOSONAR
    {
        readonly Boolean[] isFileLoaded = new Boolean[3];
        readonly string[] currentPath = new string[3];
        readonly string[] tempPath = new string[3];

        public MainWindow()
        {
            InitializeComponent();
            CenterWindowOnScreen();
        }

        private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void ResetWindow()
        {
            System.Windows.Forms.Application.Restart();
            System.Windows.Application.Current.Shutdown();
        }

        private void CheckAddButton()
        {
            if (isFileLoaded[0] == true && isFileLoaded[1] == true && isFileLoaded[2] == true)
            {
                btnAdd.IsEnabled = true;
            }
        }

        private void AddFileToTemp(int fileNum)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            string[] filters = { "appfilter.xml", "appmap.xml", "theme_resource.xml" };

            dlg.FileName = filters[fileNum];
            dlg.DefaultExt = ".xml"; // Default file extension
            dlg.Filter = "XML document (.xml)|*.xml"; // Filter files by extension

            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                currentPath[fileNum] = dlg.FileName;
                tempPath[fileNum] = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(currentPath[fileNum]) + ".xml");

                // Store document to temp
                File.Copy(currentPath[fileNum], tempPath[fileNum], true);

                switch (fileNum)
                {
                    case 0:
                        // Change status
                        lblStatus1.Content = "(" + currentPath[0] + ")";
                        lblStatus1.Foreground = new SolidColorBrush(Colors.Green);
                        txtField1.IsEnabled = true;
                        break;
                    case 1:
                        // Change status
                        lblStatus2.Content = "(" + currentPath[1] + ")";
                        lblStatus2.Foreground = new SolidColorBrush(Colors.Green);
                        txtField2.IsEnabled = true;
                        break;
                    case 2:
                        // Change status
                        lblStatus3.Content = "(" + currentPath[2] + ")";
                        lblStatus3.Foreground = new SolidColorBrush(Colors.Green);
                        txtField3.IsEnabled = true;
                        break;
                    default: throw new NotImplementedException();
                }

                isFileLoaded[fileNum] = true;
                CheckAddButton();
            }

        }

        private void menuSelect1_Click(object sender, RoutedEventArgs e)
        {
            AddFileToTemp(0);
        }

        private void menuSelect2_Click(object sender, RoutedEventArgs e)
        {
            AddFileToTemp(1);
        }

        private void menuSelect3_Click(object sender, RoutedEventArgs e)
        {
            AddFileToTemp(2);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Add confirmation window
            MessageBoxResult result = MessageBox.Show("Are you sure that you want to add these lines?", "Confirmation", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                ProcessDocuments();
            }
            else
            {
                return;
            }
        }

        private void ProcessDocuments()
        {
            int[] lastLine = new int[3];
            String[] xmlParent = { "</resources>", "</appmap>", "</Theme>" };
            string[] files = { "appfilter.xml", "appmap.xml", "theme_resource.xml" };

            // Check if file valid first
            for (int i = 0; i < 3; i++)
            {
                List<string> lines = File.ReadAllLines(tempPath[i]).ToList();
                int lineCount = 0;
                Boolean valid = false;

                foreach (var line in lines)
                {
                    if (line.Contains(xmlParent[i]))
                    {
                        lastLine[i] = lineCount;
                        valid = true;
                    }

                    lineCount++;
                }

                if (!valid)
                {
                    MessageBox.Show(files[i] + " is not the valid output file!");

                    return;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                List<string> lines = File.ReadAllLines(tempPath[i]).ToList();

                string text = i switch
                {
                    0 => txtField1.Text,
                    1 => txtField2.Text,
                    2 => txtField3.Text,
                    _ => throw new NotImplementedException()
                };

                lines.Insert(lastLine[i], Environment.NewLine + text);
                File.WriteAllLines(tempPath[i], lines);

                File.Copy(tempPath[i], currentPath[i], true);
            }

            MessageBox.Show("All new lines has been added!");
        }

        private void menuReset_Click(object sender, RoutedEventArgs e)
        {
            ResetWindow();
        }
    }
}
