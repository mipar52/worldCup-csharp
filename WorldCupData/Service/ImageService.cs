using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using WorldCupData.Enums;

namespace WorldCupData.Service
{
    public static class ImageService
    {
        private static readonly string PlaceholderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "Images", "placeholder.jpg");
        //  private static readonly string ImageFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "Images", "Players");
        //  private static readonly string MapFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "players_images.txt");

        private static Dictionary<string, string> _cache;

        public static string GetPlaceholderImagePath(ChampionshipType type)
        {
            // Adjust based on your project structure

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine("Files","worldcup.sfg.io", type.ToString().ToLower(),"Images", "placeholderTwo.jpg");

            string fullPath = Path.Combine(baseDir, relativePath);

            if (File.Exists(fullPath))
            {
                return fullPath;
            }

            throw new FileNotFoundException("Placeholder image not found at: " + fullPath);
        }

        public static void SavePlayerImage(ChampionshipType type, string playerName, string sourceFilePath)
        {


            string ImageFolder = PathHelper.GetImageFolderPath(type);
            string MapFile = PathHelper.GetImageMappingFilePath(type);

            Directory.CreateDirectory(ImageFolder);
            Directory.CreateDirectory(Path.GetDirectoryName(MapFile)!);

            string extension = Path.GetExtension(sourceFilePath);
            string targetPath = Path.Combine(ImageFolder, $"{SanitizeFileName(playerName)}{extension}");

            File.Copy(sourceFilePath, targetPath, true);

            var mappings = LoadImageMappings(type);
            mappings[playerName] = targetPath;

            File.WriteAllLines(MapFile, mappings.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        }

        public static string? GetPlayerImagePath(ChampionshipType type,string playerName)
        {
            var mappings = LoadImageMappings(type);
            return mappings.TryGetValue(playerName, out var path) ? path : null;
        }

        private static Dictionary<string, string> LoadImageMappings(ChampionshipType type)
        {
            string ImageFolder = PathHelper.GetImageFolderPath(type);
            string MapFile = PathHelper.GetImageMappingFilePath(type);
            
            if (_cache != null) return _cache;

            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (File.Exists(MapFile))
            {
                foreach (var line in File.ReadAllLines(MapFile))
                {
                    var parts = line.Split('=', 2);
                    if (parts.Length == 2)
                        map[parts[0]] = parts[1];
                }
            }

            _cache = map;
            return map;
        }

        private static string SanitizeFileName(string name)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');
            return name;
        }
    }


}


