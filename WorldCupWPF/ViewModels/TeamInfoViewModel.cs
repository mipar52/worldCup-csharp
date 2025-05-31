using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WorldCupData.Model;
using WorldCupData.Service;

namespace WorldCupWPF.ViewModels
{
    public class TeamInfoViewModel : INotifyPropertyChanged
    {
        public string Country { get; }
        public string FifaCode { get; }
        public int GamesPlayed { get; private set; }
        public int Wins { get; private set; }
        public int Losses { get; private set; }
        public int Draws { get; private set; }
        public int GoalsFor { get; private set; }
        public int GoalsAgainst { get; private set; }

        public int GoalDifference => Math.Abs(GoalsFor - GoalsAgainst);

        public TeamInfoViewModel(Team team)
        {
            Country = team.Country;
            FifaCode = team.FifaCode;

            _ = LoadTeamStatsAsync(team.FifaCode);
        }

        private async Task LoadTeamStatsAsync(string code)
        {
            var dataProvider = new DataProvider();

            var matches = await dataProvider.GetMatchesByCountryAsync(AppSettings.Championship, AppSettings.DataSourceMode, code);

            GamesPlayed = matches.Count;

            foreach (var match in matches)
            {
                bool isHome = match.HomeTeam.Code == code;
                var myTeam = isHome ? match.HomeTeam : match.AwayTeam;
                var opponentTeam = isHome ? match.AwayTeam : match.HomeTeam;

                GoalsFor += (int)myTeam.Goals;
                GoalsAgainst += (int)opponentTeam.Goals;

                if (myTeam.Goals > opponentTeam.Goals) Wins++;
                else if (myTeam.Goals < opponentTeam.Goals) Losses++;
                else Draws++;
            }

            OnPropertyChanged(nameof(GamesPlayed));
            OnPropertyChanged(nameof(Wins));
            OnPropertyChanged(nameof(Losses));
            OnPropertyChanged(nameof(Draws));
            OnPropertyChanged(nameof(GoalsFor));
            OnPropertyChanged(nameof(GoalsAgainst));
            OnPropertyChanged(nameof(GoalDifference));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
