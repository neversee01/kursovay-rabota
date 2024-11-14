using System.Windows;

namespace kursovay_rabota
{
    public class Game
    {
        public Snake Snake { get; private set; }
        public Food Food { get; private set; }
        private int gridSize;

        public Game(int gridSize, int initialSnakeSize)
        {
            this.gridSize = gridSize;
            Snake = new Snake(initialSnakeSize, gridSize);
            Food = new Food(gridSize);
        }

        public void Update()
        {
            Snake.Move();

            // Проверяем, съела ли змейка еду
            if (Snake.Body[0] == Food.Position)
            {
                Snake.Grow();
                Food.GeneratePosition();
            }
        }
    }
}
