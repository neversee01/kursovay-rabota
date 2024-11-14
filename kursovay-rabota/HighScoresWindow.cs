using System.Windows;
using System.Windows.Controls;

namespace kursovay_rabota
{
    public class HighScoresWindow : Window
    {
        private ListBox fullHighScoreList;

        public HighScoresWindow(HighScore highScore)
        {
            this.Title = "High Scores";
            this.Width = 300;
            this.Height = 400;
            SetupUI();
            LoadHighScores(highScore);
        }

        private void SetupUI()
        {
            StackPanel stackPanel = new StackPanel { Margin = new Thickness(10) };
            TextBlock title = new TextBlock
            {
                Text = "Таблица лучших результатов",
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 10)
            };
            stackPanel.Children.Add(title);

            fullHighScoreList = new ListBox { Width = 250, Height = 300 };
            stackPanel.Children.Add(fullHighScoreList);

            this.Content = stackPanel;
        }

        private void LoadHighScores(HighScore highScore)
        {
            fullHighScoreList.Items.Clear();
            foreach (var entry in highScore.Scores)
            {
                fullHighScoreList.Items.Add($"{entry.Name}: {entry.Score}");
            }
        }
    }
}
