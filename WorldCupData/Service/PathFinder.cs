using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCupData.Enums;

namespace WorldCupData.Service
{
    public static class PathHelper
    {
        private static string FindWorldCupDataBasePath()
        {
            string dir = AppContext.BaseDirectory;

            while (dir != null)
            {
                string candidate = Path.Combine(dir, "WorldCupData", "Files");
                if (Directory.Exists(candidate))
                    return Path.Combine(dir, "WorldCupData", "Files");

                var fullName = Directory.GetParent(dir)?.FullName;
                if (fullName == null)
                    break;

                dir = fullName;
            }

            throw new DirectoryNotFoundException("Could not find WorldCupData/Files folder.");
        }

        public static string GetImageFolderPath(ChampionshipType type)
        {
            string basePath = FindWorldCupDataBasePath();
            return Path.Combine(basePath, "worldcup.sfg.io", type.ToString().ToLower(), "Images");
        }

        public static string GetFavoritesFilePath(ChampionshipType type)
        {
            string basePath = FindWorldCupDataBasePath();
            return Path.Combine(basePath, "worldcup.sfg.io", type.ToString().ToLower(), "favorite.txt");
        }

        public static string GetImageMappingFilePath(ChampionshipType type)
        {
            string basePath = FindWorldCupDataBasePath();
            return Path.Combine(basePath, "worldcup.sfg.io", type.ToString().ToLower(), "Images", "players_images.txt");
        }
        public static string GetSettingsPath()
        {
            string basePath = FindWorldCupDataBasePath();
            return Path.Combine(basePath, "Settings", "settings.txt");
        }
    }


}
