namespace WorldCupForms
{
    partial class PlayerCardControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pbPlayer = new PictureBox();
            lbName = new Label();
            lbPosition = new Label();
            lbNumber = new Label();
            lbPlayerName = new Label();
            lbPlayerNumber = new Label();
            lbPlayerPosition = new Label();
            lbPlayerCaptain = new Label();
            contextMenu = new ContextMenuStrip(components);
            promoteToFavoriteToolStripMenuItem = new ToolStripMenuItem();
            demoteToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)pbPlayer).BeginInit();
            contextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // pbPlayer
            // 
            pbPlayer.Location = new Point(189, 3);
            pbPlayer.Name = "pbPlayer";
            pbPlayer.Size = new Size(143, 170);
            pbPlayer.TabIndex = 0;
            pbPlayer.TabStop = false;
            // 
            // lbName
            // 
            lbName.AutoSize = true;
            lbName.Location = new Point(3, 17);
            lbName.Name = "lbName";
            lbName.Size = new Size(75, 15);
            lbName.TabIndex = 1;
            lbName.Text = "Player name:";
            // 
            // lbPosition
            // 
            lbPosition.AutoSize = true;
            lbPosition.Location = new Point(4, 65);
            lbPosition.Name = "lbPosition";
            lbPosition.Size = new Size(88, 15);
            lbPosition.TabIndex = 2;
            lbPosition.Text = "Player position:";
            // 
            // lbNumber
            // 
            lbNumber.AutoSize = true;
            lbNumber.Location = new Point(4, 44);
            lbNumber.Name = "lbNumber";
            lbNumber.Size = new Size(87, 15);
            lbNumber.TabIndex = 3;
            lbNumber.Text = "Player number:";
            // 
            // lbPlayerName
            // 
            lbPlayerName.AutoSize = true;
            lbPlayerName.Location = new Point(97, 17);
            lbPlayerName.Name = "lbPlayerName";
            lbPlayerName.Size = new Size(23, 15);
            lbPlayerName.TabIndex = 5;
            lbPlayerName.Text = "bla";
            // 
            // lbPlayerNumber
            // 
            lbPlayerNumber.AutoSize = true;
            lbPlayerNumber.Location = new Point(97, 44);
            lbPlayerNumber.Name = "lbPlayerNumber";
            lbPlayerNumber.Size = new Size(23, 15);
            lbPlayerNumber.TabIndex = 6;
            lbPlayerNumber.Text = "bla";
            // 
            // lbPlayerPosition
            // 
            lbPlayerPosition.AutoSize = true;
            lbPlayerPosition.Location = new Point(97, 65);
            lbPlayerPosition.Name = "lbPlayerPosition";
            lbPlayerPosition.Size = new Size(23, 15);
            lbPlayerPosition.TabIndex = 7;
            lbPlayerPosition.Text = "bla";
            // 
            // lbPlayerCaptain
            // 
            lbPlayerCaptain.AutoSize = true;
            lbPlayerCaptain.Location = new Point(4, 91);
            lbPlayerCaptain.Name = "lbPlayerCaptain";
            lbPlayerCaptain.Size = new Size(23, 15);
            lbPlayerCaptain.TabIndex = 8;
            lbPlayerCaptain.Text = "bla";
            // 
            // contextMenu
            // 
            contextMenu.Items.AddRange(new ToolStripItem[] { promoteToFavoriteToolStripMenuItem, demoteToolStripMenuItem });
            contextMenu.Name = "contextMenu";
            contextMenu.Size = new Size(178, 48);
            contextMenu.Click += contextMenu_Click;
            // 
            // promoteToFavoriteToolStripMenuItem
            // 
            promoteToFavoriteToolStripMenuItem.Name = "promoteToFavoriteToolStripMenuItem";
            promoteToFavoriteToolStripMenuItem.Size = new Size(177, 22);
            promoteToFavoriteToolStripMenuItem.Text = "Promote to favorite";
            promoteToFavoriteToolStripMenuItem.Click += promoteToFavoriteToolStripMenuItem_Click;
            // 
            // demoteToolStripMenuItem
            // 
            demoteToolStripMenuItem.Name = "demoteToolStripMenuItem";
            demoteToolStripMenuItem.Size = new Size(177, 22);
            demoteToolStripMenuItem.Text = "Demote";
            demoteToolStripMenuItem.Click += demoteToolStripMenuItem_Click;
            // 
            // PlayerCardControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lbPlayerCaptain);
            Controls.Add(lbPlayerPosition);
            Controls.Add(lbPlayerNumber);
            Controls.Add(lbPlayerName);
            Controls.Add(lbNumber);
            Controls.Add(lbPosition);
            Controls.Add(lbName);
            Controls.Add(pbPlayer);
            Name = "PlayerCardControl";
            Size = new Size(335, 179);
            Load += PlayerCardControl_Load;
            MouseDown += PlayerCardControl_MouseDown;
            ((System.ComponentModel.ISupportInitialize)pbPlayer).EndInit();
            contextMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbPlayer;
        private Label lbName;
        private Label lbPosition;
        private Label lbNumber;
        private Label lbPlayerName;
        private Label lbPlayerNumber;
        private Label lbPlayerPosition;
        private Label lbPlayerCaptain;
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem promoteToFavoriteToolStripMenuItem;
        private ToolStripMenuItem demoteToolStripMenuItem;
    }
}
