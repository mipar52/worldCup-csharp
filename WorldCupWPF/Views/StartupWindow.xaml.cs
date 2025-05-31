using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorldCupData.Service;
using WorldCupWPF.ViewModels;

namespace WorldCupWPF.Views
{
    /// <summary>
    /// Interaction logic for StartupWindow.xaml
    /// </summary>
    public partial class StartupWindow : Window
    {
        private bool _isFromSettings;
        public StartupWindow(bool isFromSettings = false)
        {
            InitializeComponent();
            ChangeLanguageStrings();
            var vm = new StartupViewModel();
            DataContext = vm;
            _isFromSettings = isFromSettings;
            if (!isFromSettings)
            {
                

                    vm.OnLanguageChange += () =>
                    {

                        ChangeLanguageStrings();
                        vm.Championships = new() { LanguageService.SetMenWorldChampion(), LanguageService.SetWomenWorldChampion() };
                        vm.DisplayModes = new() { "1024x768", "1366x768", "1920x1080", LanguageService.FullScreen() };
                    };

                    vm.OnConfirmed += () =>
                    {
                        var mainWindow = new MainWindow();

                        if (AppSettings.DisplayMode == LanguageService.FullScreen())
                        {
                            mainWindow.WindowState = WindowState.Maximized;
                        }
                        else
                        {
                            switch (AppSettings.DisplayMode)
                            {
                                case "1024x768":
                                    mainWindow.Width = 1024;
                                    mainWindow.Height = 768;
                                    break;
                                case "1366x768":
                                    mainWindow.Width = 1366;
                                    mainWindow.Height = 768;
                                    break;
                                case "1920x1080":
                                    mainWindow.Width = 1920;
                                    mainWindow.Height = 1080;
                                    break;
                            }
                        }

                        mainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
                        Application.Current.MainWindow = mainWindow;
                        mainWindow.Show();

                        this.Close();
                    };

                    vm.OnCanceled += () =>
                    {
                        this.Close();
                    };
            }
        }

        private void ChangeLanguageStrings()
        {
            if (_isFromSettings) 
            {
                this.Title = LanguageService.SettingsTitle();
            }else
            {
                this.Title = LanguageService.StartupTitle();
            }
                
            PickAppLanguage.Text = LanguageService.SetApplicationLangugeString();
            PickWorldChampionship.Text = LanguageService.SetWorldChampionShipPicker();
            PickDisplaySize.Text = LanguageService.PickDisplaySize();
            BtnCancel.Content = LanguageService.Cancel();
            BtnConfirm.Content = LanguageService.Confirm();
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is StartupViewModel vm)
            {

                ((StartupViewModel)DataContext).LanguageChangeCommand.Execute(null);
            }
        }

        private void StartupWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ((StartupViewModel)DataContext).ConfirmCommand.Execute(null);
            }
            else if (e.Key == Key.Escape)
            {
                ((StartupViewModel)DataContext).CancelCommand.Execute(null);
            }
        }
    }
}
