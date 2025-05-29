using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
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
    }

    }