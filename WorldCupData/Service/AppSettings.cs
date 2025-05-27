using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCupData.Enums;

namespace WorldCupData.Service
{
    public class AppSettings
    {
        public string Language { get; set; } = "en";
        public ChampionshipType Championship { get; set; } = ChampionshipType.Men;

        public DataSourceMode DataSourceMode { get; set; } = DataSourceMode.Api;
    }
}
