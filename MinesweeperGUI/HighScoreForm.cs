using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using MineSweeperClasses;

namespace MinesweeperGUI
{
    public partial class HighScoreForm : Form
    {
        private const string HighScoreFile = "highscores.json";
        private List<GameStats> highScores = new List<GameStats>();

        public HighScoreForm()
        {
            InitializeComponent();
            LoadHighScores();
            BindData();
        }

        // File > Save
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveHighScores();
            MessageBox.Show("Scores saved!", "Success");
        }

        // File > Load
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadHighScores();
            MessageBox.Show("Scores loaded!", "Success");
        }

        // File > Exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Load scores from JSON
        private void LoadHighScores()
        {
            if (File.Exists(HighScoreFile))
            {
                string json = File.ReadAllText(HighScoreFile);
                highScores = JsonSerializer.Deserialize<List<GameStats>>(json) ?? new List<GameStats>();
                BindData();
            }
        }

        // Sort > By Name
        private void byNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvScores.DataSource = highScores.OrderBy(s => s.Name).ToList();
        }

        // Sort > By Score
        private void byScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvScores.DataSource = highScores.OrderByDescending(s => s.Score).ToList();
        }

        // Sort > By Date
        private void byDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvScores.DataSource = highScores.OrderByDescending(s => s.EndTime).ToList();
        }

        // Save scores to JSON
        private void SaveHighScores()
        {
            string json = JsonSerializer.Serialize(highScores);
            File.WriteAllText(HighScoreFile, json);
        }

        // Bind scores to DataGridView
        private void BindData()
        {
            dgvScores.DataSource = null;
            dgvScores.DataSource = highScores
                .OrderByDescending(s => s.Score)
                .ToList();
        }
    }
}