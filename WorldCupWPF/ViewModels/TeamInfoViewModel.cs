using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WorldCupData.Model;

namespace WorldCupWPF.ViewModels
{
    public class TeamInfoViewModel : INotifyPropertyChanged
    {
        public string Country { get; }
        public string FifaCode { get; }
        public int GamesPlayed { get; }
        public int Wins { get; }
        public int Losses { get; }
        public int Draws { get; }
        public int GoalsFor { get; }
        public int GoalsAgainst { get; }

        public int GoalDifference => GoalsFor - GoalsAgainst;

        public TeamInfoViewModel(Team team)
        {
            Country = team.Country;
            FifaCode = team.FifaCode;
            GamesPlayed = 10;
            Wins = 10;
            Losses = 10;
            Draws =10;
            GoalsFor = 10;
            GoalsAgainst = 10;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
