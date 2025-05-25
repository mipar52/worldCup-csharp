using WorldCupData.Enums;
using System;
using System.IO;
namespace WorldCupData.Service
{
    public class SettingsService
    {
        private const string SettingsFile = "settings.txt";

        public void SaveSettings(string language, ChampionshipType championship)
        {
            File.WriteAllText(SettingsFile, $"{language}|{championship}");
        }

        public (string Language, ChampionshipType Championship) LoadSettings()
        {
            if (!File.Exists(SettingsFile))
                return ("en", ChampionshipType.Men);

            var parts = File.ReadAllText(SettingsFile).Split('|');
            if (parts.Length != 2)
                return ("en", ChampionshipType.Men);

            try
            {
                var language = parts[0];
                var championship = (ChampionshipType)Enum.Parse(typeof(ChampionshipType), parts[1], true);
                return (language, championship);
            }
            catch
            {
                return ("en", ChampionshipType.Men);
            }
        }
    }
}
