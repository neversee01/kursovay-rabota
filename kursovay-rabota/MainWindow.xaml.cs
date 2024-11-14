using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace kursovay_rabota
{
    public partial class MainWindow : Window
    {
        private Game game;
        private DispatcherTimer timer;
        private int gridSize = 30;
        private int cellSize = 15;
        private int score;
        private HighScore highScore;
        private bool isGameRunning;

        public MainWindow()
        {
            InitializeComponent();
            highScore = new HighScore();
            UpdateHighScoreList();
            StartNewGame();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(150);
            timer.Tick += Timer_Tick;
            isGameRunning = false;

            // Подписываемся на событие PreviewKeyDown для обработки клавиш направления
            this.PreviewKeyDown += MainWindow_PreviewKeyDown;
        }

        private void StartNewGame()
        {
            int initialSnakeSize = 1;
            game = new Game(gridSize, initialSnakeSize);
            score = 0;
            UpdateHighScoreList();
            Draw();
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Обработка клавиш направления
            if (!isGameRunning) return;

            switch (e.Key)
            {
                case Key.Up:
                    if (game.Snake.CurrentDirection != Direction.Down)
                        game.Snake.CurrentDirection = Direction.Up;
                    e.Handled = true; // Останавливаем дальнейшую обработку события
                    break;
                case Key.Down:
                    if (game.Snake.CurrentDirection != Direction.Up)
                        game.Snake.CurrentDirection = Direction.Down;
                    e.Handled = true;
                    break;
                case Key.Left:
                    if (game.Snake.CurrentDirection != Direction.Right)
                        game.Snake.CurrentDirection = Direction.Left;
                    e.Handled = true;
                    break;
                case Key.Right:
                    if (game.Snake.CurrentDirection != Direction.Left)
                        game.Snake.CurrentDirection = Direction.Right;
                    e.Handled = true;
                    break;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!isGameRunning) return;

            try
            {
                game.Update();
                score++;
                Draw();
            }
            catch (Exception)
            {
                StopGame();
                var playerName = NameInput.Text;

                if (string.IsNullOrWhiteSpace(playerName) || playerName == "Enter Name")
                {
                    playerName = "Anonymous";
                }

                highScore.AddScore(playerName, score);
                UpdateHighScoreList();

                MessageBox.Show($"Game Over! Score: {score}. Starting a new game...");
                StartNewGame();
            }
        }

        private void Draw()
        {
            GameCanvas.Children.Clear();

            foreach (Point point in game.Snake.Body)
            {
                Rectangle snakePart = new Rectangle
                {
                    Width = cellSize,
                    Height = cellSize,
                    Fill = Brushes.Green
                };
                Canvas.SetLeft(snakePart, point.X * cellSize);
                Canvas.SetTop(snakePart, point.Y * cellSize);
                GameCanvas.Children.Add(snakePart);
            }

            Ellipse food = new Ellipse
            {
                Width = cellSize,
                Height = cellSize,
                Fill = Brushes.Red
            };
            Canvas.SetLeft(food, game.Food.Position.X * cellSize);
            Canvas.SetTop(food, game.Food.Position.Y * cellSize);
            GameCanvas.Children.Add(food);
        }

        private void UpdateHighScoreList()
        {
            HighScoreList.Items.Clear();
            foreach (var entry in highScore.Scores)
            {
                HighScoreList.Items.Add($"{entry.Name}: {entry.Score}");
            }
        }

        private void ShowHighScoresWindow(object sender, RoutedEventArgs e)
        {
            HighScoresWindow highScoresWindow = new HighScoresWindow(highScore);
            highScoresWindow.ShowDialog();
            FocusWindow(); // Вернуть фокус на главное окно после нажатия
        }

        private void StartGame(object sender, RoutedEventArgs e)
        {
            StartGame();
            FocusWindow(); // Вернуть фокус на главное окно
        }

        private void StartGame()
        {
            if (!isGameRunning)
            {
                isGameRunning = true;
                timer.Start();
            }
        }

        private void StopGame(object sender, RoutedEventArgs e)
        {
            StopGame();
            FocusWindow(); // Вернуть фокус на главное окно
        }

        private void StopGame()
        {
            if (isGameRunning)
            {
                isGameRunning = false;
                timer.Stop();
            }
        }

        private void RestartGame(object sender, RoutedEventArgs e)
        {
            StopGame();
            StartNewGame();
            FocusWindow(); // Вернуть фокус на главное окно
        }

        private void ClearHighScores(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите обнулить список рекордов?", "Подтверждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                highScore.ClearScores();
                UpdateHighScoreList(); // Обновляем отображение списка
            }
        }


        private void FocusWindow()
        {
            this.Focus(); // Устанавливаем фокус на окно
        }
    }
}
