﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCupData.Enums;

namespace WorldCupData.Service
{
    public static class FavoriteService
    {

        public static void Save(ChampionshipType type,string teamCode, List<string> playerNames)
        {
            string SharedBasePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "WorldCupData", "worldcup.sfg.io", type.ToString().ToLower()
            );

            string FilePath = PathHelper.GetFavoritesFilePath(type);


            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            string content = $"team={teamCode}\nplayers={string.Join(",", playerNames)}";
            File.WriteAllText(FilePath, content);
        }

        public static (string TeamCode, List<string> PlayerNames)? Load(ChampionshipType type)
        {
            string FilePath = PathHelper.GetFavoritesFilePath(type);

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
