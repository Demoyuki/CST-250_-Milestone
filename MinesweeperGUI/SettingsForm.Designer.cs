namespace MinesweeperGUI
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            gbxDifficulty = new GroupBox();
            btnStart = new Button();
            trackSize = new TrackBar();
            trackDifficulty = new TrackBar();
            lblDifficulty = new Label();
            lblSize = new Label();
            gbxDifficulty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackDifficulty).BeginInit();
            SuspendLayout();
            // 
            // gbxDifficulty
            // 
            gbxDifficulty.Controls.Add(btnStart);
            gbxDifficulty.Controls.Add(trackSize);
            gbxDifficulty.Controls.Add(trackDifficulty);
            gbxDifficulty.Controls.Add(lblDifficulty);
            gbxDifficulty.Controls.Add(lblSize);
            gbxDifficulty.Location = new Point(12, 12);
            gbxDifficulty.Name = "gbxDifficulty";
            gbxDifficulty.Size = new Size(240, 262);
            gbxDifficulty.TabIndex = 0;
            gbxDifficulty.TabStop = false;
            gbxDifficulty.Text = "Play Minesweeper";
            // 
            // btnStart
            // 
            btnStart.Location = new Point(82, 209);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 23);
            btnStart.TabIndex = 6;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += startButton_Click;
            // 
            // trackSize
            // 
            trackSize.Location = new Point(0, 63);
            trackSize.Minimum = 3;
            trackSize.Name = "trackSize";
            trackSize.Size = new Size(228, 45);
            trackSize.TabIndex = 5;
            trackSize.Value = 3;
            trackSize.Scroll += TrackBar_Scroll;
            // 
            // trackDifficulty
            // 
            trackDifficulty.Location = new Point(3, 158);
            trackDifficulty.Maximum = 50;
            trackDifficulty.Minimum = 1;
            trackDifficulty.Name = "trackDifficulty";
            trackDifficulty.Size = new Size(231, 45);
            trackDifficulty.TabIndex = 4;
            trackDifficulty.Value = 10;
            trackDifficulty.Scroll += TrackBar_Scroll;
            // 
            // lblDifficulty
            // 
            lblDifficulty.AutoSize = true;
            lblDifficulty.Location = new Point(6, 111);
            lblDifficulty.Name = "lblDifficulty";
            lblDifficulty.Size = new Size(186, 15);
            lblDifficulty.TabIndex = 1;
            lblDifficulty.Text = "Difficulty: Bomb percentage 100%";
            // 
            // lblSize
            // 
            lblSize.AutoSize = true;
            lblSize.Location = new Point(3, 32);
            lblSize.Name = "lblSize";
            lblSize.Size = new Size(85, 15);
            lblSize.TabIndex = 0;
            lblSize.Text = "Board Size: 1x1";
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(282, 286);
            Controls.Add(gbxDifficulty);
            Name = "SettingsForm";
            Text = "Settings";
            gbxDifficulty.ResumeLayout(false);
            gbxDifficulty.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackDifficulty).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gbxDifficulty;
        private TrackBar trackSize;
        private TrackBar trackDifficulty;
        private Label lblDifficulty;
        private Label lblSize;
        private Button btnStart;
    }
}