using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ic_req_helper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window //NOSONAR
    {
        readonly Boolean[] isFileReady = new Boolean[6];
        readonly string[] currentPath = new string[6];
        readonly string[] tempPath = new string[6];

        public MainWindow()
        {
            InitializeComponent();
            CenterWindowOnScreen();

            MenuItem[] menuItems = { menuLocate0, menuLocate1, menuLocate2, menuLocate3, menuLocate4, menuLocate5, menuReset, menuExit };

            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].Click += menu_Click;
            }

            for (int i = 0; i < isFileReady.Length; i++)
            {
                isFileReady[i] = false;
            }
        }

        private void CenterWindowOnScreen()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
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
            if (isFileReady[0] && isFileReady[1] && isFileReady[2] && isFileReady[3] && isFileReady[4] && isFileReady[5])
            {
                btnOverwrite.IsEnabled = true;
            }
        }

        private void LocateFiles(int num)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();

            string[] fileNames = { "appfilter.xml", "appfilter.xml", "appmap.xml", "theme_resource.xml", "drawable.xml", "icon_pack.xml" };

            ofd.FileName = fileNames[num];
            ofd.DefaultExt = ".xml";
            ofd.Filter = "XML document (.xml)|*.xml";

            Nullable<bool> result = ofd.ShowDialog();

            if (result == true)
            {
                // Check if file valid

                // Copy document to temp
                currentPath[num] = ofd.FileName;
                tempPath[num] = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(ofd.FileName) + ".xml");
                File.Copy(ofd.FileName, tempPath[num], true);

                // Check checkbox
                var checkbox = this.FindName($"checkbox{num}") as CheckBox;
                checkbox.IsChecked = true;

                isFileReady[num] = true;
            }

            CheckOverwriteButton();
        }

        private void menu_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;

            if (element.Name.Equals("menuExit"))
            {
                Application.Current.Shutdown();
            }
            else if (element.Name.Equals("menuReset"))
            {
                ResetWindow();
            }
            else
            {
                int result = element.Name switch
                {
                    "menuLocate0" => 0,
                    "menuLocate1" => 1,
                    "menuLocate2" => 2,
                    "menuLocate3" => 3,
                    "menuLocate4" => 4,
                    "menuLocate5" => 5,
                    _ => throw new NotImplementedException()
                };

                LocateFiles(result);
            }
        }

        private void btnOverwrite_Click(object sender, RoutedEventArgs e)
        {
            // Todo
        }
    }
}
