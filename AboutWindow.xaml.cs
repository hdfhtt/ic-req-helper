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
            Owner = MainWindow.instance;

            InitializeComponent();
        }
    }
}
