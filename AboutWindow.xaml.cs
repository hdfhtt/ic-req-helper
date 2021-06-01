using System.Windows;

namespace ic_req_helper
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window // eslint-disable-line no-eval
    {
        public AboutWindow()
        {
            Owner = MainWindow.Instance();

            InitializeComponent();
        }
    }
}
