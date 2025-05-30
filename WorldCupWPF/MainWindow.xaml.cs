using System.ComponentModel;
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

            if (DataContext is MainViewModel vm)
            {
                    _ = vm.LoadTeamsAsync();
                vm.PropertyChanged += Vm_PropertyChanged;
            }

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
            var settingsWindow = new StartupWindow();
            settingsWindow.ShowDialog();

            // Optional: reload or apply settings after dialog closes
            // e.g., check if settings changed and refresh UI or restart app
        }

        private void Vm_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainViewModel.MatchResult))
            {
                Dispatcher.Invoke(() => AnimateMatchResult());
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


    }

}