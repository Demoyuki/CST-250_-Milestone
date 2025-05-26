using System.Drawing;
using MineSweeperClasses;
using static MineSweeperClasses.Board;

namespace MineSweeperConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                RunGame();
            } while (PlayAgain());
        }

        static void RunGame()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Minesweeper!");
            Board board = new Board(5, 0.1f); // Small board for testing
            bool gameOver = false;
            PrintAnswers(board);

            while (!gameOver)
            {
                try
                {
                    PrintBoard(board);
                    Console.WriteLine("Enter row, column, and action (1=Visit, 2=Flag, 3=Use Reward):");

                    if (!int.TryParse(Console.ReadLine(), out int row) ||
                        !int.TryParse(Console.ReadLine(), out int col) ||
                        !int.TryParse(Console.ReadLine(), out int action))
                    {
                        Console.WriteLine("Invalid input. Please enter valid integers.");
                        continue;
                    }

                    if (!IsInBounds(board, row, col))
                    {
                        Console.WriteLine("Coordinates out of range.");
                        continue;
                    }

                    Cell cell = board.Cells[row, col];

                    switch (action)
                    {
                        case 1:
                            HandleVisit(board, row, col, cell);
                            break;
                        case 2:
                            HandleFlag(cell);
                            break;
                        case 3:
                            board.UseHintReward();
                            break;
                        default:
                            Console.WriteLine("Invalid action. Choose 1, 2, or 3.");
                            break;
                    }

                    switch (board.DetermineGameState())
                    {
                        case GameStatus.Won:
                            PrintBoard(board);
                            Console.Beep(1000, 200);
                            Console.Beep(1200, 200);
                            Console.Beep(1400, 300);
                            Console.WriteLine("You won!");
                            gameOver = true;
                            break;
                        case GameStatus.Lost:
                            PrintBoard(board);
                            Console.Beep(400, 300);
                            Console.Beep(300, 300);
                            Console.Beep(200, 500);
                            Console.WriteLine("You hit a bomb! Game over.");
                            gameOver = true;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }
        }

        static bool PlayAgain()
        {
            Console.WriteLine("Would you like to play again? (y/yes to restart)");
            string input = Console.ReadLine().Trim().ToLower();
            return input == "y" || input == "yes";
        }

        static void HandleVisit(Board board, int row, int col, Cell cell)
        {
            if (!cell.IsVisited && !cell.IsFlagged)
            {
                board.RevealCellAndNeighbors(row, col);
                if (cell.HasSpecialReward)
                {
                    Console.WriteLine("You found a reward! You can use it in a future turn.");
                    board.RemainingRewards++;
                    cell.HasSpecialReward = false;
                }
            }
            else
            {
                Console.WriteLine("Cell already visited or flagged.");
            }
        }

        static void HandleFlag(Cell cell)
        {
            if (!cell.IsVisited)
                cell.IsFlagged = !cell.IsFlagged;
            else
                Console.WriteLine("You cannot flag a visited cell.");
        }


        static bool IsInBounds(Board board, int row, int col)
        {
            return row >= 0 && row < board.Size && col >= 0 && col < board.Size;
        }

        static void PrintBoard(Board board)
        {
            PrintHeader(board.Size);
            for (int i = 0; i < board.Size; i++)
            {
                Console.Write($"{i} |");
                for (int j = 0; j < board.Size; j++)
                {
                    Cell cell = board.Cells[i, j];
                    Console.Write(" ");
                    (char symbol, ConsoleColor color) = GetCellDisplay(cell);
                    Console.ForegroundColor = color;
                    Console.Write(symbol);
                    Console.ResetColor();
                    Console.Write(" |");
                }
                Console.WriteLine();
                PrintDivider(board.Size);
            }
        }

        static void PrintAnswers(Board board)
        {
            PrintHeader(board.Size);
            for (int i = 0; i < board.Size; i++)
            {
                Console.Write($"{i} |");
                for (int j = 0; j < board.Size; j++)
                {
                    Cell cell = board.Cells[i, j];
                    Console.Write(" ");
                    (char symbol, ConsoleColor color) = GetAnswerDisplay(cell);
                    Console.ForegroundColor = color;
                    Console.Write(symbol);
                    Console.ResetColor();
                    Console.Write(" |");
                }
                Console.WriteLine();
                PrintDivider(board.Size);
            }
        }

        static void PrintHeader(int size)
        {
            Console.Write("   ");
            for (int col = 0; col < size; col++) Console.Write($" {col}  ");
            Console.WriteLine();
            PrintDivider(size);
        }

        static void PrintDivider(int size)
        {
            Console.Write("  +");
            for (int col = 0; col < size; col++) Console.Write("---+");
            Console.WriteLine();
        }

        static (char symbol, ConsoleColor color) GetAnswerDisplay(Cell cell)
        {
            if (cell.IsBomb) return ('B', ConsoleColor.Red);
            if (cell.HasSpecialReward) return ('R', ConsoleColor.Yellow);
            return GetNeighborDisplay(cell);
        }

        static (char symbol, ConsoleColor color) GetCellDisplay(Cell cell)
        {
            if (cell.IsRevealedByHint) return ('B', ConsoleColor.DarkRed);
            if (cell.IsFlagged) return ('F', ConsoleColor.Cyan);
            if (!cell.IsVisited) return ('?', ConsoleColor.White);
            if (cell.IsBomb) return ('B', ConsoleColor.Red);
            if (cell.HasSpecialReward) return ('R', ConsoleColor.Yellow);
            return GetNeighborDisplay(cell);
        }

        static (char symbol, ConsoleColor color) GetNeighborDisplay(Cell cell)
        {
            if (cell.NumberOfBombNeighbors > 0)
            {
                ConsoleColor color = cell.NumberOfBombNeighbors switch
                {
                    1 => ConsoleColor.Blue,
                    2 => ConsoleColor.Green,
                    3 => ConsoleColor.Red,
                    4 => ConsoleColor.DarkBlue,
                    5 => ConsoleColor.DarkRed,
                    6 => ConsoleColor.Magenta,
                    7 => ConsoleColor.DarkMagenta,
                    8 => ConsoleColor.Gray,
                    _ => ConsoleColor.White
                };
                return (cell.NumberOfBombNeighbors.ToString()[0], color);
            }
            return ('.', ConsoleColor.White);
        }
    }
}
