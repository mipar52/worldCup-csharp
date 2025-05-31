using System;
using System.Collections.Generic;
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

namespace WorldCupWPF.Views
{
    /// <summary>
    /// Interaction logic for TeamInfoWindow.xaml
    /// </summary>
    public partial class TeamInfoWindow : Window
    {
        public TeamInfoWindow()
        {
            InitializeComponent();
            ChangeLanguageStrings();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ChangeLanguageStrings()
        {
            this.Title = LanguageService.TeamInfoWindowTitle();
            lbDraws.Text = LanguageService.DrawsLabel();
            lbFifaCode.Text = LanguageService.FifaCodeLabel();
            lbGamesPlayed.Text = LanguageService.GamesPlayedLabel();
            lbGoalsAgainst.Text = LanguageService.GoalsAgainstLabel();
            lbGoalsFor.Text = LanguageService.GoalsForLabel();
            lbLoses.Text = LanguageService.LossesLabel();
            lbWins.Text = LanguageService.WinsLabel();
            lbDraws.Text = LanguageService.DrawsLabel();
            lbGoalsDifference.Text = LanguageService.GoalsDifferenceLabel();
            BtnClose.Content = LanguageService.Close();
        }
    }
}
