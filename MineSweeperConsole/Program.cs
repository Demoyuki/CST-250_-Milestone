using MineSweeperClasses;

namespace MineSweeperConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, welcome to Minesweeper");
            Board board = new Board(10, 0.1f);
            Console.WriteLine("Here is the answer key for the first board");
            PrintAnswers(board);

            board = new Board(20, 0.2f);
            Console.WriteLine("Here is the answer key for the second board");
            // Test the "Hint" reward
            Console.WriteLine("Using hint reward...");
            board.UseHintReward();
            PrintAnswers(board); // The revealed bomb will now show as visited (e.g., "B" becomes visible)
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