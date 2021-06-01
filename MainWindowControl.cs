using System;
using System.Windows;
using System.Xml;

namespace ic_req_helper
{
    public partial class MainWindow : Window // eslint-disable-line no-eval
    {
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
            else if (element.Name.Equals("menuAbout"))
            {
                ShowAboutWindow();
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
            // Todo: Add confirmation box
            var result = MessageBox.Show("Are you sure you want to continue?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.OK)
            {
                File.ProcessXML(fieldAppfilter.Text);
            }
        }

        private void MainWindow_LocationChanged(object sender, EventArgs e)
        {
            foreach (Window win in this.OwnedWindows)
            {
                win.Top = this.Top + ((this.ActualHeight - win.ActualHeight) / 2);
                win.Left = this.Left + ((this.ActualWidth - win.ActualWidth) / 2);
            }
        }
    }
}
