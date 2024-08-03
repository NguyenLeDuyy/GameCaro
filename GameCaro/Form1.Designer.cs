namespace GameCaro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pnlChessBoard = new Panel();
            panel2 = new Panel();
            main_picture = new PictureBox();
            panel3 = new Panel();
            label1 = new Label();
            pctbSign = new PictureBox();
            btnLAN = new Button();
            txbIP = new TextBox();
            prcbCoolDown = new ProgressBar();
            txbPlayerName = new TextBox();
            tmCoolDown = new System.Windows.Forms.Timer(components);
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)main_picture).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pctbSign).BeginInit();
            SuspendLayout();
            // 
            // pnlChessBoard
            // 
            pnlChessBoard.BackColor = SystemColors.Control;
            pnlChessBoard.Location = new Point(11, 229);
            pnlChessBoard.Margin = new Padding(4, 4, 4, 4);
            pnlChessBoard.Name = "pnlChessBoard";
            pnlChessBoard.Size = new Size(1016, 651);
            pnlChessBoard.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(main_picture);
            panel2.Location = new Point(15, 10);
            panel2.Margin = new Padding(4, 4, 4, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(1408, 199);
            panel2.TabIndex = 1;
            // 
            // main_picture
            // 
            main_picture.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            main_picture.BackgroundImage = Properties.Resources.Caro_Chess_5_in_a_line;
            main_picture.Location = new Point(-4, 0);
            main_picture.Margin = new Padding(4, 4, 4, 4);
            main_picture.Name = "main_picture";
            main_picture.Size = new Size(1411, 195);
            main_picture.TabIndex = 0;
            main_picture.TabStop = false;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel3.Controls.Add(label1);
            panel3.Controls.Add(pctbSign);
            panel3.Controls.Add(btnLAN);
            panel3.Controls.Add(txbIP);
            panel3.Controls.Add(prcbCoolDown);
            panel3.Controls.Add(txbPlayerName);
            panel3.Location = new Point(1035, 229);
            panel3.Margin = new Padding(4, 4, 4, 4);
            panel3.Name = "panel3";
            panel3.Size = new Size(388, 651);
            panel3.TabIndex = 2;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Algerian", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(21, 579);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(323, 40);
            label1.TabIndex = 5;
            label1.Text = "5 in a line to win";
            // 
            // pctbSign
            // 
            pctbSign.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pctbSign.BackColor = SystemColors.Control;
            pctbSign.BackgroundImageLayout = ImageLayout.Stretch;
            pctbSign.Location = new Point(9, 218);
            pctbSign.Margin = new Padding(4, 4, 4, 4);
            pctbSign.Name = "pctbSign";
            pctbSign.Size = new Size(375, 328);
            pctbSign.SizeMode = PictureBoxSizeMode.StretchImage;
            pctbSign.TabIndex = 4;
            pctbSign.TabStop = false;
            // 
            // btnLAN
            // 
            btnLAN.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnLAN.Location = new Point(6, 174);
            btnLAN.Margin = new Padding(4, 4, 4, 4);
            btnLAN.Name = "btnLAN";
            btnLAN.Size = new Size(380, 36);
            btnLAN.TabIndex = 3;
            btnLAN.Text = "Connect";
            btnLAN.UseVisualStyleBackColor = true;
            // 
            // txbIP
            // 
            txbIP.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txbIP.Location = new Point(9, 114);
            txbIP.Margin = new Padding(4, 4, 4, 4);
            txbIP.Name = "txbIP";
            txbIP.Size = new Size(376, 31);
            txbIP.TabIndex = 2;
            // 
            // prcbCoolDown
            // 
            prcbCoolDown.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            prcbCoolDown.Location = new Point(9, 56);
            prcbCoolDown.Margin = new Padding(4, 4, 4, 4);
            prcbCoolDown.Name = "prcbCoolDown";
            prcbCoolDown.Size = new Size(375, 36);
            prcbCoolDown.TabIndex = 1;
            // 
            // txbPlayerName
            // 
            txbPlayerName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txbPlayerName.Location = new Point(6, 1);
            txbPlayerName.Margin = new Padding(4, 4, 4, 4);
            txbPlayerName.Name = "txbPlayerName";
            txbPlayerName.ReadOnly = true;
            txbPlayerName.Size = new Size(376, 31);
            txbPlayerName.TabIndex = 0;
            // 
            // tmCoolDown
            // 
            tmCoolDown.Tick += tmCoolDown_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1438, 895);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(pnlChessBoard);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 4, 4, 4);
            Name = "Form1";
            Text = "Game Caro";
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)main_picture).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pctbSign).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlChessBoard;
        private Panel panel2;
        private Panel panel3;
        private PictureBox main_picture;
        private ProgressBar prcbCoolDown;
        private TextBox txbPlayerName;
        private Button btnLAN;
        private TextBox txbIP;
        private Label label1;
        private PictureBox pctbSign;
        private System.Windows.Forms.Timer tmCoolDown;
    }
}
