using WorldCupData.Enums;
using System;
using System.IO;
namespace WorldCupData.Service
{
    public class SettingsService
    {
        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "settings.txt");
        public bool WasLoaded { get; private set; } = false;

        public void Save()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            File.WriteAllText(FilePath, $"language={AppSettings.Language}\nchampionship={AppSettings.Championship}\ndataMode={AppSettings.DataSourceMode}\ndisplayMode={AppSettings.DisplayMode}");
        }

        public void Load()
        {
            if (!File.Exists(FilePath))
            {
                WasLoaded = false;
                return;
            }

            var lines = File.ReadAllLines(FilePath);
            

            foreach (var line in lines)
            {
                var parts = line.Split('=');
                if (parts.Length != 2) continue;

                switch (parts[0])
                {
                    case "language":
                        AppSettings.Language = parts[1];
                        break;
                    case "championship":
                        if (Enum.TryParse(parts[1], out ChampionshipType champ))
                            AppSettings.Championship = champ;
                        break;
                    case "dataMode":
                        if (Enum.TryParse(parts[1], out DataSourceMode mode))
                            AppSettings.DataSourceMode = mode;
                        break;
                    case "displayMode":
                        AppSettings.DisplayMode = parts[1];
                        break;
                }
            }
            WasLoaded = true;
        }

        public void Reset()
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
    }
}
