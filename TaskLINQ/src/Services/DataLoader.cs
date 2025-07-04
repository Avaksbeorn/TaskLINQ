using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using LINQQueriesProject.Models;

namespace LINQQueriesProject.Services
{
    public static class DataLoader
    {
        public static T LoadDataFromFile<T>(string fileName)
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Файл не найден: {filePath}");
                    return default;
                }

                string jsonData = File.ReadAllText(filePath);

                if (string.IsNullOrWhiteSpace(jsonData))
                {
                    Console.WriteLine($"Файл пустой или содержит только пробелы: {filePath}");
                    return default;
                }

                try
                {
                    return JsonSerializer.Deserialize<T>(jsonData);
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Ошибка десериализации файла {filePath}: {ex.Message}");
                    Console.WriteLine($"Содержание JSON: {jsonData}");
                    return default;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла {fileName}: {ex.Message}");
                return default;
            }
        }
    }
}
