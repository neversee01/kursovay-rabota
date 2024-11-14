using System.Collections.Generic;
using System.Windows;

namespace kursovay_rabota
{
    public class Snake
    {
        public List<Point> Body { get; private set; }
        public Direction CurrentDirection { get; set; }
        private int gridSize;

        public Snake(int initialSize, int gridSize)
        {
            this.gridSize = gridSize;
            CurrentDirection = Direction.Right;

            // Начальная позиция змейки по центру поля
            Body = new List<Point>();
            int centerX = gridSize / 2;
            int centerY = gridSize / 2;

            for (int i = 0; i < initialSize; i++)
            {
                Body.Add(new Point(centerX - i, centerY));
            }
        }

        public void Move()
        {
            Point head = Body[0];
            Point newHead = head;

            switch (CurrentDirection)
            {
                case Direction.Up: newHead.Y -= 1; break;
                case Direction.Down: newHead.Y += 1; break;
                case Direction.Left: newHead.X -= 1; break;
                case Direction.Right: newHead.X += 1; break;
            }

            // Проверка столкновения с границами
            if (newHead.X < 0 || newHead.X >= gridSize || newHead.Y < 0 || newHead.Y >= gridSize)
            {
                throw new System.Exception("Game Over");
            }

            // Проверка столкновения с телом
            if (Body.Contains(newHead))
            {
                throw new System.Exception("Game Over");
            }

            Body.Insert(0, newHead); // Новая голова
            Body.RemoveAt(Body.Count - 1); // Удаляем последний элемент
        }

        public void Grow()
        {
            Body.Add(Body[Body.Count - 1]); // Добавляем элемент к змейке
        }
    }

    public enum Direction
    {
        Up, Down, Left, Right
    }
}
