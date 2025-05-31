using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorldCupData.Service;
using WorldCupWPF.ViewModels;
using WorldCupWPF.Views;

namespace WorldCupWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ChangeLanguageStrings();
            if (DataContext is MainViewModel vm)
            {
                vm.PropertyChanged += Vm_PropertyChanged;

                vm.MatchLoaded += () =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        Debug.WriteLine($"Home Team Events Count: {vm.HomeTeamEvents?.Count}");
                        Debug.WriteLine($"Away Team Events Count: {vm.AwayTeamEvents?.Count}");

                        fieldLayoutControl.HomeTeamEvents = vm.HomeTeamEvents;
                        fieldLayoutControl.AwayTeamEvents = vm.AwayTeamEvents;
                    });
                };
            }



        }

        private void ChangeLanguageStrings()
        {
            this.Title = LanguageService.MainWindowTitle();
            BtnHomeTeamInfo.Content = LanguageService.ViewTeamInfo();
            BtnOpponentInfo.Content = LanguageService.ViewTeamInfo();
            BtnSettings.Content = LanguageService.SettingsButton();
        }
        private void ViewInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel vm && vm.SelectedTeam != null)
            {
                var teamInfoWindow = new TeamInfoWindow
                {
                    DataContext = new TeamInfoViewModel(vm.SelectedTeam.Team)
                };

                teamInfoWindow.ShowDialog();
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new StartupWindow(isFromSettings: true);

            if (settingsWindow.DataContext is StartupViewModel vm)
            {
                vm.OnConfirmed += () =>
                {
                    ApplySettings();
                    settingsWindow.Close();
                };
            }

            settingsWindow.Owner = this;
            settingsWindow.ShowDialog();
        }

        private void ApplySettings()
        {
            // Apply display mode
            if (AppSettings.DisplayMode == LanguageService.FullScreen())
            {
                WindowStyle = WindowStyle.None;
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowStyle = WindowStyle.SingleBorderWindow;
                WindowState = WindowState.Normal;

                switch (AppSettings.DisplayMode)
                {
                    case "1024x768":
                        Width = 1024;
                        Height = 768;
                        break;
                    case "1366x768":
                        Width = 1366;
                        Height = 768;
                        break;
                    case "1920x1080":
                        Width = 1920;
                        Height = 1080;
                        break;
                }
            }

            ChangeLanguageStrings();
        }


        private void Vm_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (DataContext is MainViewModel vm)
            {
                if (e.PropertyName == nameof(MainViewModel.MatchResult))
                {
                    Dispatcher.Invoke(() =>
                    {
                        AnimateMatchResult();

                        Debug.WriteLine($"Home Team Events Count: {vm.HomeTeamEvents?.Count}");
                        Debug.WriteLine($"Home Team Events Count: {vm.AwayTeamEvents?.Count}");

                        fieldLayoutControl.HomeTeamEvents = vm.HomeTeamEvents;
                        fieldLayoutControl.AwayTeamEvents = vm.AwayTeamEvents;
                    });
                }
            }
        }

        private void AnimateMatchResult()
        {
            var sb = new Storyboard();

            var fade = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));
            Storyboard.SetTarget(fade, MatchResultText);
            Storyboard.SetTargetProperty(fade, new PropertyPath("Opacity"));

            var scaleX = new DoubleAnimation(0.8, 1, TimeSpan.FromSeconds(0.5));
            Storyboard.SetTarget(scaleX, MatchResultText);
            Storyboard.SetTargetProperty(scaleX, new PropertyPath("RenderTransform.ScaleX"));

            var scaleY = new DoubleAnimation(0.8, 1, TimeSpan.FromSeconds(0.5));
            Storyboard.SetTarget(scaleY, MatchResultText);
            Storyboard.SetTargetProperty(scaleY, new PropertyPath("RenderTransform.ScaleY"));

            sb.Children.Add(fade);
            sb.Children.Add(scaleX);
            sb.Children.Add(scaleY);

            sb.Begin();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                var result = MessageBox.Show(
                    LanguageService.ExitConfirmation(),
                    LanguageService.Exit(),
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (result == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }

                e.Handled = true;
            }
            else if (e.Key == Key.S)
            {
                BtnSettings.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }
    }
}