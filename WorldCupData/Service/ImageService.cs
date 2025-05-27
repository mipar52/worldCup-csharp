using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace WorldCupData.Service
{
    public static class ImageService
    {
        private static readonly string PlaceholderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "Images", "placeholder.jpg");

        public static string GetPlaceholderImagePath()
        {
            // Adjust based on your project structure
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine("Files", "Images", "placeholderTwo.jpg");

            string fullPath = Path.Combine(baseDir, relativePath);

            if (File.Exists(fullPath))
            {
                return fullPath;
            }

            throw new FileNotFoundException("Placeholder image not found at: " + fullPath);
        }
    }
}

