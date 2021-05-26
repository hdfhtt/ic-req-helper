using System;
using System.Windows;
using System.Windows.Controls;

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

            MenuItem[] menuItems = { menuLocate0, menuLocate1, menuLocate2, menuLocate3, menuLocate4, menuLocate5, menuReset, menuExit }; 

            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].Click += menu_Click;
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
            if (isFileLoaded[0] == true && isFileLoaded[1] == true && isFileLoaded[2] == true)
            {
                btnOverwrite.IsEnabled = true;
            }
        }

        private void LocateFiles(int num)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();

            string[] fileNames = { "appfilter.xml", "appfilter.xml", "appmap.xml", "theme_resource.xml", "drawable.xml", "icon_pack.xml" };
            CheckBox[] cbControls = { checkbox0, checkbox1, checkbox2, checkbox3, checkbox4, checkbox5 };

            ofd.FileName = fileNames[num];
            ofd.DefaultExt = ".xml";
            ofd.Filter = "XML document (.xml)|*.xml";

            Nullable<bool> result = ofd.ShowDialog();

            if (result == true)
            {
                // Check checkbox
                cbControls[num].IsChecked = true;

                // Copy document to temp
            }

            // CheckOverwriteButton()
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
