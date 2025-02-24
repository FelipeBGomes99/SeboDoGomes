using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SeboDoGomesV2.Service
{
    internal class FileLoader
    {
        public static string GetFilePath(string fileName)
        {
            if (!Directory.Exists("Data"))
            {
                Directory.CreateDirectory("Data");
            }

            string filePath = Path.Combine("Data", fileName);

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }

            return filePath;
        }

        public static List<T> LoadData<T>(string fileName)
        {
            try
            {
                var options = new JsonSerializerOptions { IncludeFields = true, PropertyNameCaseInsensitive = true };
                string json = File.ReadAllText(fileName);
                return JsonSerializer.Deserialize<List<T>>(json, options) ?? new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar arquivo {fileName}: {ex.Message}");
                return new List<T>();
            }
        }

        public static void SerializeList<T> (string filePath, List<T> target)
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(target, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
