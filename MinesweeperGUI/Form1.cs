using System.Text.Json;
using System.Text;
using MineSweeperClasses;
using static MineSweeperClasses.Board;

namespace MinesweeperGUI
{
    public partial class Form1 : Form
    {
        // Fields
        private Board board;
        private Button[,] cells;
        private int size;
        private float difficulty;
        private GameStats currentGameStats;

        private const int CELL_SIZE = 100; // Fixed size for each cell
        private const int PANEL_PADDING = 20; // Space around the board

        // Image Dictionary
        private Dictionary<string, Image> cellImages = new Dictionary<string, Image>();
        private string imageFolder = "Images"; // Folder containing cell images

        // HighScores
        private const string HighScoreFile = "highscores.json";
        private List<GameStats> highScores = new List<GameStats>();

        public Form1(int size, float difficulty)
        {
            LoadCellImages();
            InitializeComponent();
            this.size = size;
            this.difficulty = difficulty;

            // Set form minimum size
            this.MinimumSize = new Size(
                size * CELL_SIZE + PANEL_PADDING * 2 + 16, // 16 for window borders
                size * CELL_SIZE + PANEL_PADDING * 2 + 39   // 39 for title bar and borders
            );

            // Set form starting size
            this.ClientSize = new Size(
                size * CELL_SIZE + PANEL_PADDING * 2,
                size * CELL_SIZE + PANEL_PADDING * 2
            );

            // Initialize game stats
            currentGameStats = new GameStats
            {
                BoardSize = size,
                Difficulty = difficulty,
                StartTime = DateTime.Now
            };

            // Set up timer
            SetupTimer();
            InitializeGame();

        }

        private void ResizeFormAfterGameStart()
        {
            int totalHeight = pnlGame.Height + 200;
            int totalWidth = Math.Max(pnlGame.Width + 40, 1000); // Keep a min width

            this.ClientSize = new Size(totalWidth, totalHeight);
            this.PerformLayout();  // Re-layout form
        }

        private void SetupTimer()
        {
            GameTimer.Start();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            UpdateTimeDisplay();
        }
        private void UpdateTimeDisplay()
        {
            var elapsed = DateTime.Now - currentGameStats.StartTime;
            lblTimerQty.Text = $"{elapsed:mm\\:ss}";
        }

        private void InitializeGame()
        {
            // Initialize the board
            board = new Board(size, difficulty);

            SetupGamePanel();
            UpdateButtonFaces();
            ResizeFormAfterGameStart();
        }

        private void SetupGamePanel()
        {
            // Clear existing controls
            pnlGame.Controls.Clear();

            // Calculate panel size based on cell size and board dimensions
            int panelWidth = size * CELL_SIZE;
            int panelHeight = size * CELL_SIZE;

            // Set panel size and position (centered)
            pnlGame.Size = new Size(panelWidth, panelHeight);
            pnlGame.Location = new Point(
                (this.ClientSize.Width - panelWidth) / 2,
                (this.ClientSize.Height - panelHeight) / 2
            );

            // Create cells grid
            cells = new Button[size, size];

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    cells[row, col] = new Button
                    {
                        Width = CELL_SIZE,
                        Height = CELL_SIZE,
                        Top = row * CELL_SIZE,
                        Left = col * CELL_SIZE,
                        Tag = new Point(row, col),
                        Font = new Font("Arial", 12, FontStyle.Bold),
                        FlatStyle = FlatStyle.Flat,
                        Margin = new Padding(0)
                    };

                    cells[row, col].FlatAppearance.BorderSize = 1;
                    cells[row, col].MouseUp += Cell_MouseClick;
                    pnlGame.Controls.Add(cells[row, col]);
                }
            }
        }

        private int CountVisitedCells()
        {
            int count = 0;
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (board.Cells[row, col].IsVisited)
                        count++;
                }
            }
            return count;
        }

        private int GetPointsPerCell()
        {
            return (int)(10 * difficulty); // Example: 0.15 difficulty ? 1.5 ? 1 point
        }


        private void Cell_MouseClick(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            Point location = (Point)button.Tag;
            int row = location.X;
            int col = location.Y;
            Cell cell = board.Cells[row, col];

            int visitedBefore = CountVisitedCells();

            if (e.Button == MouseButtons.Left)
            {
                if (!cell.IsFlagged)
                {
                    board.FloodFill(row, col);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (!cell.IsVisited)
                {
                    cell.IsFlagged = !cell.IsFlagged;
                }
            }

            int visitedAfter = CountVisitedCells();
            int newlyVisited = visitedAfter - visitedBefore;

            if (newlyVisited > 0)
            {
                currentGameStats.Score += newlyVisited * GetPointsPerCell();
            }

            UpdateButtonFaces();
            lblScore.Text = $"Score: {currentGameStats.Score}";

            switch (board.DetermineGameState())
            {
                case GameStatus.Won:
                    EndGame(true);
                    break;
                case GameStatus.Lost:
                    EndGame(false);
                    break;
            }
        }

        private void LoadCellImages()
        {
            try
            {
                // Load all required images
                cellImages["hidden"] = Image.FromFile(Path.Combine(imageFolder, "cell_hidden.png"));
                cellImages["revealed"] = Image.FromFile(Path.Combine(imageFolder, "cell_revealed.png"));
                cellImages["flag"] = Image.FromFile(Path.Combine(imageFolder, "cell_flag.png"));
                cellImages["mine"] = Image.FromFile(Path.Combine(imageFolder, "cell_mine.png"));
                cellImages["mine_red"] = Image.FromFile(Path.Combine(imageFolder, "cell_mine_red.png"));

                // Load number images
                for (int i = 1; i <= 8; i++)
                {
                    cellImages[$"number_{i}"] = Image.FromFile(Path.Combine(imageFolder, $"cell_{i}.png"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading images: {ex.Message}");
                // Fall back to text if images fail to load
                UseTextFallback();
            }
        }

        private void UseTextFallback()
        {
            // Clear images dictionary to force text display
            cellImages.Clear();
        }

        private void UpdateButtonFaces()
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    Button button = cells[row, col];
                    Cell cell = board.Cells[row, col];

                    // Reset button properties
                    button.Text = "";
                    button.Image = null;
                    button.BackgroundImage = null;
                    button.BackColor = SystemColors.Control;

                    if (cellImages.Count > 0) // Use images if available
                    {
                        if (cell.IsVisited)
                        {
                            if (cell.IsBomb)
                            {
                                button.BackgroundImage = cell.IsRevealedByHint
                                    ? cellImages["mine"]
                                    : cellImages["mine_red"];
                            }
                            else if (cell.NumberOfBombNeighbors > 0)
                            {
                                button.BackgroundImage = cellImages[$"number_{cell.NumberOfBombNeighbors}"];
                            }
                            else
                            {
                                button.BackgroundImage = cellImages["revealed"];
                            }
                        }
                        else if (cell.IsFlagged)
                        {
                            button.BackgroundImage = cellImages["flag"];
                        }
                        else
                        {
                            button.BackgroundImage = cellImages["hidden"];
                        }

                        // Ensure image displays properly
                        button.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else // Fallback to text if images failed to load
                    {
                        if (cell.IsVisited)
                        {
                            if (cell.IsBomb)
                            {
                                button.Text = "B";
                                button.BackColor = cell.IsRevealedByHint ? Color.LightGray : Color.Red;
                            }
                            else if (cell.NumberOfBombNeighbors > 0)
                            {
                                button.Text = cell.NumberOfBombNeighbors.ToString();
                                button.ForeColor = GetNumberColor(cell.NumberOfBombNeighbors);
                                button.BackColor = Color.LightGray;
                            }
                            else
                            {
                                button.BackColor = Color.LightGray;
                            }
                        }
                        else if (cell.IsFlagged)
                        {
                            button.Text = "F";
                            button.BackColor = Color.LightYellow;
                        }
                    }
                }
            }
        }

        private void EndGame(bool isWinner)
        {
            GameTimer.Stop();
            currentGameStats.EndTime = DateTime.Now;
            currentGameStats.IsWinner = isWinner;

            if (isWinner)
            {
                // Get player name
                string playerName = Microsoft.VisualBasic.Interaction.InputBox(
                    "You won! Enter your name:",
                    "High Score",
                    "Player"
                );

                if (!string.IsNullOrEmpty(playerName))
                {
                    currentGameStats.Name = playerName;
                    SaveHighScore(currentGameStats);
                    OpenHighScoresForm(); // Show high scores after saving
                }
            }
            else
            {
                MessageBox.Show("Game Over! Try again.", "Defeat");
            }

            btnPlayAgain.Visible = true;
        }
        private void OpenHighScoresForm()
        {
            // Load existing scores (if any)
            if (File.Exists(HighScoreFile))
            {
                HighScoreForm highScoreForm = new HighScoreForm();
                highScoreForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No high scores found!", "Information");
            }
        }
        private void SaveHighScore(GameStats stats)
        {
            // Load existing scores
            if (File.Exists(HighScoreFile))
            {
                string json = File.ReadAllText(HighScoreFile);
                highScores = JsonSerializer.Deserialize<List<GameStats>>(json) ?? new List<GameStats>();
            }

            // Add new score
            stats.Id = highScores.Count + 1;
            highScores.Add(stats);

            // Save to file
            string updatedJson = JsonSerializer.Serialize(highScores);
            File.WriteAllText(HighScoreFile, updatedJson);
        }

        private Color GetNumberColor(int number)
        {
            switch (number)
            {
                case 1: return Color.Blue;
                case 2: return Color.Green;
                case 3: return Color.Red;
                case 4: return Color.DarkBlue;
                case 5: return Color.DarkRed;
                case 6: return Color.Teal;
                case 7: return Color.Black;
                case 8: return Color.Gray;
                default: return Color.Black;
            }
        }

        private void btnPlayAgain_Click(object sender, EventArgs e)
        {
            // Reset state
            GameTimer.Stop();
            btnPlayAgain.Visible = false;
            currentGameStats = new GameStats
            {
                BoardSize = size,
                Difficulty = difficulty,
                StartTime = DateTime.Now
            };

            SetupTimer();
            InitializeGame();
            lblScore.Text = $"Score: 0";
        }

        private void btnScores_Click(object sender, EventArgs e)
        {
            OpenHighScoresForm();
        }
    }
}
