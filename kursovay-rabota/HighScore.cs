using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace kursovay_rabota
{
    public class HighScoreEntry
    {
        public string Name { get; set; }
        public int Score { get; set; }
    }

    public class HighScore
    {
        private const string FilePath = "highscores.txt";
        public List<HighScoreEntry> Scores { get; private set; }

        public HighScore()
        {
            Scores = new List<HighScoreEntry>();
            LoadScores();
        }

        // Добавление нового рекорда
        public void AddScore(string name, int score)
        {
            Scores.Add(new HighScoreEntry { Name = name, Score = score });
            Scores = Scores.OrderByDescending(s => s.Score).Take(10).ToList();
            SaveScores();
        }

        // Очистка рекордов
        public void ClearScores()
        {
            Scores.Clear();
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath); // Удаление файла с рекордами
            }
        }

        private void LoadScores()
        {
            if (File.Exists(FilePath))
            {
                var lines = File.ReadAllLines(FilePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                    {
                        Scores.Add(new HighScoreEntry { Name = parts[0], Score = score });
                    }
                }
                Scores = Scores.OrderByDescending(s => s.Score).Take(10).ToList();
            }
        }

        private void SaveScores()
        {
            var lines = Scores.Select(s => $"{s.Name},{s.Score}");
            File.WriteAllLines(FilePath, lines);
        }
    }
}
