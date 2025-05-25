using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WorldCupData.Service
{
    public class FileService
    {
        public T LoadJson<T>(string path)
        {
            if (!File.Exists(path))
                return default;

            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json);
        }

        public void SaveJson<T>(string path, T data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }

        public string LoadText(string path)
        {
            return File.Exists(path) ? File.ReadAllText(path) : null;
        }

        public void SaveText(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}
