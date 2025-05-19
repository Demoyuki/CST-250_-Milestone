using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperClasses
{
    public class Board
    {
        public int Size { get; set; }
        public float Difficulty { get; set; }
        public Cell[,] Cells { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Board(int size, float difficulty)
        {
            Size = size;
            Difficulty = difficulty;
            Cells = new Cell[size, size];
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Cells[i, j] = new Cell { Row = i, Column = j };
                }
            }
            SetupBombs();
            SetupRewards();
            CalculateNumberOfBombNeighbors();
            StartTime = DateTime.Now;
        }

        private void SetupRewards()
        {
            Random random = new Random();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    // Place rewards on 5% of non-bomb cells
                    if (!Cells[i, j].IsBomb && random.NextDouble() < 0.05)
                    {
                        Cells[i, j].HasSpecialReward = true;
                    }
                }
            }
        }

        public void UseHintReward()
        {
            List<Cell> bombs = new List<Cell>();
            foreach (var cell in Cells)
            {
                if (cell.IsBomb && !cell.IsVisited)
                {
                    bombs.Add(cell);
                }
            }

            if (bombs.Count > 0)
            {
                Random random = new Random();
                Cell revealedBomb = bombs[random.Next(bombs.Count)];
                revealedBomb.IsVisited = true; // Reveal the bomb
                Console.WriteLine($"Hint: Bomb revealed at ({revealedBomb.Row}, {revealedBomb.Column})");
            }
            else
            {
                Console.WriteLine("No unrevealed bombs left!");
            }
        }

        private void SetupBombs()
        {
            Random random = new Random();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (random.NextDouble() < Difficulty)
                    {
                        Cells[i, j].IsBomb = true;
                    }
                }
            }
        }

        private void CalculateNumberOfBombNeighbors()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (!Cells[i, j].IsBomb)
                    {
                        Cells[i, j].NumberOfBombNeighbors = GetNumberOfBombNeighbors(i, j);
                    }
                }
            }
        }

        private int GetNumberOfBombNeighbors(int row, int col)
        {
            int count = 0;
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < Size && j >= 0 && j < Size && Cells[i, j].IsBomb)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}
