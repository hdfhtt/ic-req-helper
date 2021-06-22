using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
using System.Reflection;

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

            string versionMajor = Assembly.GetExecutingAssembly().GetName().Version.Major.ToString();
            string versionMinor = Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
            string versionBuild = Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();

            version.Text = $"{versionMajor}.{versionMinor}.{versionBuild}";

            CloseButton.Click += CloseButton_Click;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            Owner.IsEnabled = true;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            // for .NET Core you need to add UseShellExecute = true
            // see https://docs.microsoft.com/dotnet/api/system.diagnostics.processstartinfo.useshellexecute#property-value
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
