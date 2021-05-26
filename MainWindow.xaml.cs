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
    public partial class MainWindow : Window //NOSONAR
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

        private void CheckOverwriteButton()
        {
            if (isFileLoaded[0] == true && isFileLoaded[1] == true && isFileLoaded[2] == true)
            {
                btnOverwrite.IsEnabled = true;
            }
        }

        /*
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
        */

        private void menuReset_Click(object sender, RoutedEventArgs e)
        {
            ResetWindow();
        }

        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void menuLocate0_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuLocate1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuLocate2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuLocate3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuLocate4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuLocate5_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOverwrite_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
