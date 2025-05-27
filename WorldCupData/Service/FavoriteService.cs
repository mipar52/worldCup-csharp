using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupData.Service
{
    public static class FavoriteService
    {
        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "favorite.txt");

        public static void Save(string teamCode, List<string> playerNames)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            string content = $"team={teamCode}\nplayers={string.Join(",", playerNames)}";
            File.WriteAllText(FilePath, content);
        }

        public static (string TeamCode, List<string> PlayerNames)? Load()
        {
            if (!File.Exists(FilePath))
                return null;

            var lines = File.ReadAllLines(FilePath);
            string teamCode = "";
            List<string> players = new();

            foreach (var line in lines)
            {
                var parts = line.Split('=');
                if (parts.Length != 2) continue;

                switch (parts[0])
                {
                    case "team":
                        teamCode = parts[1];
                        break;
                    case "players":
                        players = parts[1].Split(',').Where(p => !string.IsNullOrWhiteSpace(p)).ToList();
                        break;
                }
            }

            return (teamCode, players);
        }
    }
}
