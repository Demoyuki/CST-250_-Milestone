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
            }
            while (PlayAgain());
        }

        static void RunGame()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Minesweeper!");
            Board board = new Board(5, 0.2f); // Small board for testing
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

                    if (row < 0 || row >= board.Size || col < 0 || col >= board.Size)
                    {
                        Console.WriteLine("Coordinates out of range.");
                        continue;
                    }

                    Cell cell = board.Cells[row, col];

                    if (action == 1) // Visit
                    {
                        if (!cell.IsVisited && !cell.IsFlagged)
                        {
                            RevealCellAndNeighbors(board, row, col);
                            if (cell.HasSpecialReward)
                            {
                                Console.WriteLine("You found a reward! Use it next turn.");
                                board.UseHintReward();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Cell already visited or flagged.");
                        }
                    }
                    else if (action == 2) // Flag
                    {
                        if (!cell.IsVisited)
                            cell.IsFlagged = !cell.IsFlagged; // Toggle flag
                        else
                            Console.WriteLine("You cannot flag a visited cell.");
                    }
                    else if (action == 3) // Use Reward
                    {
                        board.UseHintReward();
                    }
                    else
                    {
                        Console.WriteLine("Invalid action. Choose 1, 2, or 3.");
                    }

                    GameStatus status = board.DetermineGameState();
                    if (status == GameStatus.Won)
                    {
                        PrintBoard(board);
                        Console.WriteLine("You won!");
                        gameOver = true;
                    }
                    else if (status == GameStatus.Lost)
                    {
                        PrintBoard(board);
                        Console.WriteLine("You hit a bomb! Game over.");
                        gameOver = true;
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

        static void RevealCellAndNeighbors(Board board, int row, int col)
        {
            if (row < 0 || row >= board.Size || col < 0 || col >= board.Size) return;

            Cell cell = board.Cells[row, col];

            if (cell.IsVisited || cell.IsFlagged) return;

            cell.IsVisited = true;

            if (cell.NumberOfBombNeighbors == 0 && !cell.IsBomb)
            {
                for (int i = row - 1; i <= row + 1; i++)
                {
                    for (int j = col - 1; j <= col + 1; j++)
                    {
                        if (i == row && j == col) continue;
                        RevealCellAndNeighbors(board, i, j);
                    }
                }
            }
        }
        static void PrintBoard(Board board)
        {
            Console.Write("   ");
            for (int col = 0; col < board.Size; col++) Console.Write($" {col}  ");
            Console.WriteLine("\n  +" + string.Join("", Enumerable.Repeat("---+", board.Size)));

            for (int i = 0; i < board.Size; i++)
            {
                Console.Write($"{i} |");
                for (int j = 0; j < board.Size; j++)
                {
                    Cell cell = board.Cells[i, j];
                    Console.Write(" ");
                    if (cell.IsFlagged)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("F"); // Flagged cells
                    }
                    else if (!cell.IsVisited)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("?"); // Hidden cells
                    }
                    else if (cell.IsBomb)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("B"); // Revealed bombs
                    }
                    else if (cell.NumberOfBombNeighbors > 0)
                    {
                        // Color-coded numbers (same as Milestone 1)
                        Console.Write(cell.NumberOfBombNeighbors);
                    }
                    else
                    {
                        Console.Write("."); // Empty revealed cell
                    }
                    Console.ResetColor();
                    Console.Write(" |");
                }
                Console.WriteLine("\n  +" + string.Join("", Enumerable.Repeat("---+", board.Size)));
            }
        }

        static void PrintAnswers(Board board)
        {
            // Print column numbers (header)
            Console.Write("   "); // Padding for row numbers
            for (int col = 0; col < board.Size; col++)
            {
                if (col > 9)
                {
                    Console.Write($" {col} "); // Column numbers
                }
                else
                {
                    Console.Write($" {col}  "); // Column numbers
                }
            }
            Console.WriteLine();

            // Print top border
            Console.Write("  +");
            for (int col = 0; col < board.Size; col++)
            {
                Console.Write("---+"); // Divider line
            }
            Console.WriteLine();

            for (int i = 0; i < board.Size; i++)
            {
                // Print row number
                Console.Write($"{i} |"); // Left border

                for (int j = 0; j < board.Size; j++)
                {
                    Cell cell = board.Cells[i, j];
                    Console.Write(" ");

                    // Set colors based on cell content
                    if (cell.IsBomb)
                    {
                        Console.ForegroundColor = ConsoleColor.Red; // Bombs = Bright Red
                        Console.Write("B");
                    }
                    else if (cell.HasSpecialReward)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow; // Rewards = Yellow
                        Console.Write("R");
                    }
                    else if (cell.NumberOfBombNeighbors > 0)
                    {
                        // Color-code numbers 1-8
                        switch (cell.NumberOfBombNeighbors)
                        {
                            case 1: Console.ForegroundColor = ConsoleColor.Blue; break;
                            case 2: Console.ForegroundColor = ConsoleColor.Green; break;
                            case 3: Console.ForegroundColor = ConsoleColor.Red; break;
                            case 4: Console.ForegroundColor = ConsoleColor.DarkBlue; break;
                            case 5: Console.ForegroundColor = ConsoleColor.DarkRed; break;
                            case 6: Console.ForegroundColor = ConsoleColor.Magenta; break;
                            case 7: Console.ForegroundColor = ConsoleColor.DarkMagenta; break;
                            case 8: Console.ForegroundColor = ConsoleColor.Gray; break;
                        }
                        Console.Write(cell.NumberOfBombNeighbors);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White; // Default for empty cells
                        Console.Write(".");
                    }

                    Console.ResetColor(); // Reset to default after each cell
                    Console.Write(" |"); // Right border
                }

                Console.WriteLine();

                // Print divider line between rows
                Console.Write("  +");
                for (int col = 0; col < board.Size; col++)
                {
                    Console.Write("---+");
                }
                Console.WriteLine();
            }
        }

    }
}