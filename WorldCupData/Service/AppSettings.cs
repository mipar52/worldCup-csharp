using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCupData.Enums;

namespace WorldCupData.Service
{
    public static class AppSettings
    {
        public static string Language { get; set; } = "en";
        public static ChampionshipType Championship { get; set; }

        public static DataSourceMode DataSourceMode { get; set; }

        // for WPF app only
        public static string DisplayMode { get; set; }

    }
}
