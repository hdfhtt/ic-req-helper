using System.Windows;

namespace ic_req_helper
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            Owner = MainWindow.Instance();

            InitializeComponent();

            CloseButton.Click += CloseButton_Click;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            Owner.IsEnabled = true;
        }
    }
}
