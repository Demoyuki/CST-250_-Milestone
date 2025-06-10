namespace MinesweeperGUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlGame = new Panel();
            lblTimerQty = new Label();
            gbxStatus = new GroupBox();
            btnPlayAgain = new Button();
            lblScore = new Label();
            lblTimer = new Label();
            GameTimer = new System.Windows.Forms.Timer(components);
            tableLayoutPanel1 = new TableLayoutPanel();
            gbxStatus.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlGame
            // 
            pnlGame.Anchor = AnchorStyles.None;
            pnlGame.Location = new Point(3, 3);
            pnlGame.Name = "pnlGame";
            pnlGame.Size = new Size(619, 742);
            pnlGame.TabIndex = 0;
            // 
            // lblTimerQty
            // 
            lblTimerQty.AutoSize = true;
            lblTimerQty.Font = new Font("Segoe UI", 14.25F);
            lblTimerQty.Location = new Point(78, 31);
            lblTimerQty.Name = "lblTimerQty";
            lblTimerQty.Size = new Size(56, 25);
            lblTimerQty.TabIndex = 0;
            lblTimerQty.Text = "00:00";
            // 
            // gbxStatus
            // 
            gbxStatus.Controls.Add(btnPlayAgain);
            gbxStatus.Controls.Add(lblScore);
            gbxStatus.Controls.Add(lblTimer);
            gbxStatus.Controls.Add(lblTimerQty);
            gbxStatus.Location = new Point(628, 3);
            gbxStatus.Name = "gbxStatus";
            gbxStatus.Size = new Size(230, 158);
            gbxStatus.TabIndex = 1;
            gbxStatus.TabStop = false;
            gbxStatus.Text = "Status:";
            // 
            // btnPlayAgain
            // 
            btnPlayAgain.Location = new Point(15, 115);
            btnPlayAgain.Name = "btnPlayAgain";
            btnPlayAgain.Size = new Size(75, 23);
            btnPlayAgain.TabIndex = 1;
            btnPlayAgain.Text = "Play Again";
            btnPlayAgain.UseVisualStyleBackColor = true;
            btnPlayAgain.Visible = false;
            btnPlayAgain.Click += btnPlayAgain_Click;
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.Font = new Font("Segoe UI", 14.25F);
            lblScore.Location = new Point(15, 73);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(78, 25);
            lblScore.TabIndex = 0;
            lblScore.Text = "Score: 0";
            // 
            // lblTimer
            // 
            lblTimer.AutoSize = true;
            lblTimer.Font = new Font("Segoe UI", 14.25F);
            lblTimer.Location = new Point(15, 31);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(57, 25);
            lblTimer.TabIndex = 0;
            lblTimer.Text = "Time:";
            // 
            // GameTimer
            // 
            GameTimer.Interval = 1000;
            GameTimer.Tick += GameTimer_Tick;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.49616F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.5038376F));
            tableLayoutPanel1.Controls.Add(pnlGame, 0, 0);
            tableLayoutPanel1.Controls.Add(gbxStatus, 1, 0);
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 48F));
            tableLayoutPanel1.Size = new Size(940, 748);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1128, 884);
            Controls.Add(tableLayoutPanel1);
            Name = "Form1";
            Text = "Form1";
            gbxStatus.ResumeLayout(false);
            gbxStatus.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlGame;
        private Label lblTimerQty;
        private GroupBox gbxStatus;
        private Button btnPlayAgain;
        private Label lblScore;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblTimer;
        public System.Windows.Forms.Timer GameTimer;
    }
}
