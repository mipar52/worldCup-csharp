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
        private readonly Random _rand = new();

        public MainWindow()
        {
            InitializeComponent();
            ChangeLanguageStrings();
            if (DataContext is MainViewModel vm)
            {
                vm.PropertyChanged += Vm_PropertyChanged;

                vm.MatchLoaded += () =>
                {
                    spinner.Message = LanguageService.RenderingField();
                    spinner.Visibility = Visibility.Visible;
                    Dispatcher.Invoke(() =>
                    {
                        Debug.WriteLine($"Home Team Events Count: {vm.HomeTeamEvents?.Count}");
                        Debug.WriteLine($"Away Team Events Count: {vm.AwayTeamEvents?.Count}");

                        if (vm.HomeTeamEvents == null || vm.AwayTeamEvents == null)
                        {
                            MessageBox.Show(
                                LanguageService.ErrorRendering("No home team or away team selected"),
                                LanguageService.Warning(),
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning
                            );
                            spinner.Visibility = Visibility.Collapsed;
                            return;
                        } else
                        {
                            fieldLayoutControl.HomeTeamEvents = vm.HomeTeamEvents;
                            fieldLayoutControl.AwayTeamEvents = vm.AwayTeamEvents;
                            spinner.Visibility = Visibility.Collapsed;
                        }

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
                    spinner.Message = LanguageService.LoadingApp();
                    spinner.Visibility = Visibility.Visible;
                    ApplySettings();
                    settingsWindow.Close();
                    spinner.Visibility = Visibility.Collapsed;
                };
            }

            settingsWindow.Owner = this;
            settingsWindow.ShowDialog();
        }

        private async void ApplySettings()
        {
            if (DataContext is MainViewModel vm)
            {
                
            
            try
            {
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
                        case "1250x768":
                            Width = 1250;
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
               await vm.LoadTeamsAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error applying settings: {ex.Message}");
                MessageBox.Show(LanguageService.ErrorApplyingSettings(), LanguageService.Warning(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
          }
        }


        private void Vm_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (DataContext is MainViewModel vm)
            {
                if (e.PropertyName == nameof(MainViewModel.MatchResult))
                {
                    Dispatcher.Invoke(() =>
                    {
                        spinner.Message = LanguageService.RenderingField();
                        spinner.Visibility = Visibility.Visible;
                        AnimateMatchResult();

                        Debug.WriteLine($"Home Team Events Count: {vm.HomeTeamEvents?.Count}");
                        Debug.WriteLine($"Home Team Events Count: {vm.AwayTeamEvents?.Count}");
                        if (vm.HomeTeamEvents == null || vm.AwayTeamEvents == null)
                        {
                            MessageBox.Show(
                                LanguageService.ErrorRendering("No home team or away team selected"),
                                LanguageService.Warning(),
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning
                            );
                            spinner.Visibility = Visibility.Collapsed;
                        } else
                        {
                            fieldLayoutControl.HomeTeamEvents = vm.HomeTeamEvents;
                            fieldLayoutControl.AwayTeamEvents = vm.AwayTeamEvents;
                        }
                        spinner.Visibility = Visibility.Collapsed;

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

            LaunchConfetti();
        }

        private void LaunchConfetti()
        {
            int confettiCount = 25;

            for (int i = 0; i < confettiCount; i++)
            {
                var shape = new Rectangle
                {
                    Width = _rand.Next(6, 12),
                    Height = _rand.Next(6, 12),
                    Fill = new SolidColorBrush(Color.FromRgb(
                        (byte)_rand.Next(50, 256),
                        (byte)_rand.Next(50, 256),
                        (byte)_rand.Next(50, 256)))
                };

                double startX = _rand.NextDouble() * ConfettiCanvas.ActualWidth;
                double endY = ConfettiCanvas.ActualHeight + 30;

                Canvas.SetLeft(shape, startX);
                Canvas.SetTop(shape, -20);

                ConfettiCanvas.Children.Add(shape);

                var fall = new DoubleAnimation
                {
                    From = -20,
                    To = endY,
                    Duration = TimeSpan.FromSeconds(_rand.NextDouble() * 1 + 1),
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
                };

                var fade = new DoubleAnimation
                {
                    From = 1,
                    To = 0,
                    BeginTime = TimeSpan.FromSeconds(1),
                    Duration = TimeSpan.FromSeconds(1)
                };

                Storyboard.SetTarget(fall, shape);
                Storyboard.SetTargetProperty(fall, new PropertyPath("(Canvas.Top)"));

                Storyboard.SetTarget(fade, shape);
                Storyboard.SetTargetProperty(fade, new PropertyPath("Opacity"));

                var sb = new Storyboard();
                sb.Children.Add(fall);
                sb.Children.Add(fade);
                sb.Completed += (s, e) => ConfettiCanvas.Children.Remove(shape);
                sb.Begin();
            }
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