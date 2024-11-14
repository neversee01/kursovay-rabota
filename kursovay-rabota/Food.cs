using System;
using System.Windows;

namespace kursovay_rabota
{
    public class Food
    {
        private int gridSize;
        private Random random = new Random();

        public Point Position { get; private set; }

        public Food(int gridSize)
        {
            this.gridSize = gridSize;
            GeneratePosition();
        }

        public void GeneratePosition()
        {
            Position = new Point(random.Next(0, gridSize), random.Next(0, gridSize));
        }
    }
}
