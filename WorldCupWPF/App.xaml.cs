using System.Configuration;
using System.Data;
using System.Windows;
using WorldCupData.Service;
using WorldCupWPF.Service;
using WorldCupWPF.Views;

namespace WorldCupWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var settingsService = new SettingsService();
            settingsService.Load();

            if (settingsService.WasLoaded)
            {
                var startupWindow = new StartupWindow();
                startupWindow.Show();
            }
            else
            {
                var mainWindow = new MainWindow();
                Application.Current.MainWindow = mainWindow;
                mainWindow.Show();
            }

        }
    }
}
