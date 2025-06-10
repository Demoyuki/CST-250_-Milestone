using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperGUI
{
    public partial class SettingsForm : Form
    {
        public int SelectedSize { get; private set; } = 5; // Default size
        public float SelectedDifficulty { get; private set; } = 0.15f; // Default difficul

        public SettingsForm()
        {
            InitializeComponent();

            // Set default values
            trackSize.Value = SelectedSize;
            trackDifficulty.Value = (int)(SelectedDifficulty * 100);
            UpdateLabels();
        }
        private void startButton_Click(object sender, System.EventArgs e)
        {
            SelectedSize = (int)trackSize.Value;
            SelectedDifficulty = (float)trackDifficulty.Value / 100;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void UpdateLabels()
        {
            lblSize.Text = $"Board Size: {trackSize.Value}x{trackSize.Value}";
            lblDifficulty.Text = $"Difficulty: Bomb percentage {(float)trackDifficulty.Value / 100:P0}";
        }

        private void TrackBar_Scroll(object sender, System.EventArgs e)
        {
            UpdateLabels();
        }
    }
}