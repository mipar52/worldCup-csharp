﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupData.Model
{
    public class Match
    {
        public string Venue { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string Time { get; set; }
        public string FifaId { get; set; }
        public Weather Weather { get; set; }
        public string Attendance { get; set; }
        public List<string> Officials { get; set; }
        public string StageName { get; set; }
        public string HomeTeamCountry { get; set; }
        public string AwayTeamCountry { get; set; }
        public DateTime Datetime { get; set; }
        public string Winner { get; set; }
        public string WinnerCode { get; set; }
        public TeamScore HomeTeam { get; set; }
        public TeamScore AwayTeam { get; set; }
        public List<TeamEvent> HomeTeamEvents { get; set; }
        public List<TeamEvent> AwayTeamEvents { get; set; }
        public TeamStats HomeTeamStatistics { get; set; }
        public TeamStats AwayTeamStatistics { get; set; }
    }

}
