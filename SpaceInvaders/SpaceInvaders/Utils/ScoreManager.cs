using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpaceInvaders.Utils;
    public static class ScoreManager
    {
    private static readonly string LocalFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    private static readonly string ScoresFilePath = Path.Combine(LocalFolderPath, "SpaceInvaders", "scores.json");

    static ScoreManager()
    {
        string directory = Path.GetDirectoryName(ScoresFilePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        if (!File.Exists(ScoresFilePath))
        {
            File.WriteAllText(ScoresFilePath, "[]"); 
        }
    }

    public static void SaveScore(int score)
    {
        List<int> scores = LoadScores();

        if (!scores.Contains(score))
        {
            scores.Add(score);
            scores.Sort((a, b) => b.CompareTo(a)); 

            if (scores.Count > 10)
            {
                scores = scores.GetRange(0, 10);
            }

            string json = JsonSerializer.Serialize(scores);
            File.WriteAllText(ScoresFilePath, json);
        }
    }

    public static List<int> LoadScores()
    {
        try
        {
            if (!File.Exists(ScoresFilePath))
            {
                return new List<int>();
            }

            string json = File.ReadAllText(ScoresFilePath);
            List<int> scores = JsonSerializer.Deserialize<List<int>>(json) ?? new List<int>();

            scores.Sort((a, b) => b.CompareTo(a));

            return scores;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al cargar las puntuaciones: {ex.Message}");
            return new List<int>();
        }
    }
}

 

