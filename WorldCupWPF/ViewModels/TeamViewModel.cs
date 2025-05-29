using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCupData.Model;

namespace WorldCupWPF.ViewModels
{
    public class TeamViewModel
    {
        public Team Team { get; set; }

        public string DisplayName => $"{Team.Country} ({Team.FifaCode})";

        public TeamViewModel(Team team)
        {
            Team = team;
        }
    }

}
