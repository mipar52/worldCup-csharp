using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCupWPF.ViewModels;
using WorldCupWPF.Views;

namespace WorldCupWPF.Service
{
    public class NavigationService
    {
        public void OpenStartupWindow()
        {
            var startupWindow = new StartupWindow
            {
                DataContext = new StartupViewModel()
            };
            startupWindow.ShowDialog();
        }

        public void OpenMainWindow()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
