using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WorldCupData.Service
{
    public class FileService
    {
        public T LoadJson<T>(string relativePath, JsonSerializerSettings settings)
        {
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            Debug.WriteLine($"CLASS LIB DEBUG: Loading JSON from: {fullPath}");

            if (!File.Exists(fullPath))
                throw new FileNotFoundException("JSON file not found:", fullPath);

            string json = LoadText(fullPath)
                          ?? throw new InvalidOperationException("Failed to load JSON content from file.");
            return JsonConvert.DeserializeObject<T>(json, settings)
                   ?? throw new InvalidOperationException("Deserialization returned null.");
        }

        public string? LoadText(string path)
        {
            return File.Exists(path) ? File.ReadAllText(path) : null;
        }
    }
}
