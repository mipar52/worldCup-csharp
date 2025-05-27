using WorldCupData.Enums;
using System;
using System.IO;
namespace WorldCupData.Service
{
    public class SettingsService
    {
        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "settings.txt");

        public void Save(AppSettings settings)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            File.WriteAllText(FilePath, $"language={settings.Language}\nchampionship={settings.Championship}\ndataMode={settings.DataSourceMode}");
        }

        public AppSettings Load()
        {
            if (!File.Exists(FilePath))
                return null;

            var lines = File.ReadAllLines(FilePath);
            var settings = new AppSettings();

            foreach (var line in lines)
            {
                var parts = line.Split('=');
                if (parts.Length != 2) continue;

                switch (parts[0])
                {
                    case "language":
                        settings.Language = parts[1];
                        break;
                    case "championship":
                        if (Enum.TryParse(parts[1], out ChampionshipType champ))
                            settings.Championship = champ;
                        break;
                    case "dataMode":
                        if (Enum.TryParse(parts[1], out DataSourceMode mode))
                            settings.DataSourceMode = mode;
                        break;
                }
            }

            return settings;
        }
    }
}
