using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using MikroSDN.Models;

namespace MikroSDN.Services
{
    public static class SessionManager
    {
        // Define o caminho para: C:\Users\Nome\AppData\Local\MikroSDN\router_sessions.json
        private static string GetFilePath()
        {
            // 1. Começamos onde o .exe está (ex: bin/Debug/net8.0-windows)
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo directory = new DirectoryInfo(currentDir);

            // 2. Subimos na hierarquia até encontrar a pasta que contém o ficheiro de projeto (.csproj)
            // Isso garante que funcionará em qualquer PC que clone o repositório
            while (directory != null && !directory.GetFiles("*.csproj").Any())
            {
                directory = directory.Parent;
            }

            // 3. Se por algum motivo não encontrar a raiz (ex: app já publicada), 
            // usamos a pasta do executável como fallback
            string rootPath = directory != null ? directory.FullName : AppDomain.CurrentDomain.BaseDirectory;

            // 4. Criamos a pasta AppData na raiz do projeto
            string folderPath = Path.Combine(rootPath, "AppData");
            MessageBox.Show("Caminho do ficheiro: " + folderPath);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return Path.Combine(folderPath, "router_sessions.json");
        }
        
        public static void SaveSessions(List<RouterDevice> sessions)
        {
            try
            {
                string filePath = GetFilePath();
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(sessions, options);

                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao guardar na AppData: " + ex.Message);
            }
        }

        public static List<RouterDevice> LoadSessions()
        {
            try
            {
                string filePath = GetFilePath();

                if (!File.Exists(filePath)) return new List<RouterDevice>();

                string jsonString = File.ReadAllText(filePath);
                if (string.IsNullOrWhiteSpace(jsonString)) return new List<RouterDevice>();

                return JsonSerializer.Deserialize<List<RouterDevice>>(jsonString) ?? new List<RouterDevice>();
            }
            catch
            {
                return new List<RouterDevice>();
            }
        }
    }
}
