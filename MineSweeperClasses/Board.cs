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
        public enum GameStatus { InProgress, Won, Lost }
        public int RemainingRewards { get; set; } = 0;
        public bool ShowAnimation { get; set; } = true;

        public Board(int size, float difficulty)
        {
            Size = size;
            Difficulty = difficulty;
            Cells = new Cell[size, size];
            InitializeBoard();
        }
        public GameStatus DetermineGameState()
        {
            bool allNonBombsVisited = true;
            bool bombTriggered = false;

            foreach (var cell in Cells)
            {
                if (cell.IsBomb && cell.IsVisited)
                {
                    bombTriggered = true; // Player clicked a bomb
                }
                else if (!cell.IsBomb && !cell.IsVisited && !cell.IsFlagged)
                {
                    allNonBombsVisited = false; // Game continues
                }
            }

            if (bombTriggered) return GameStatus.Lost;
            return allNonBombsVisited ? GameStatus.Won : GameStatus.InProgress;
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
            if (RemainingRewards <= 0)
            {
                Console.WriteLine("You don't have any rewards to use.");
                return;
            }

            List<Cell> bombs = new List<Cell>();
            foreach (var cell in Cells)
            {
                if (cell.IsBomb && !cell.IsVisited && !cell.IsRevealedByHint)
                {
                    bombs.Add(cell);
                }
            }

            if (bombs.Count > 0)
            {
                Random random = new Random();
                Cell revealedBomb = bombs[random.Next(bombs.Count)];
                revealedBomb.IsRevealedByHint = true; // Don't mark it as visited!
                Console.WriteLine($"Hint: Bomb revealed at ({revealedBomb.Row}, {revealedBomb.Column})");
                RemainingRewards--;
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
        //RevealCellAndNeighbors
        public void FloodFill(int row, int col, Action<Board> onUpdate = null)
        {
            if (row < 0 || row >= Size || col < 0 || col >= Size)
                return;

            Cell cell = Cells[row, col];
            if (cell.IsVisited || cell.IsFlagged)
                return;

            cell.IsVisited = true;

            // Check for reward and collect it
            if (cell.HasSpecialReward)
            {
                Console.WriteLine($"You found a reward at ({row}, {col})!");
                RemainingRewards++;
                cell.HasSpecialReward = false;
            }

            // Trigger update callback if provided
            if (onUpdate != null)
            {
                onUpdate(this);
                if (ShowAnimation) Thread.Sleep(100);
            }

            // Stop if the cell has a bomb or has bomb neighbors
            if (cell.IsBomb || cell.NumberOfBombNeighbors > 0)
                return;

            // Recursively reveal neighbors
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i == row && j == col)
                        continue;

                    FloodFill(i, j, onUpdate);
                }
            }
        }
    }
}
