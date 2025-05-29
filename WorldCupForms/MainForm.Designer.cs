namespace WorldCupForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cbFavoriteTeam;
        private System.Windows.Forms.Button btnChooseFavoritePlayers;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            cbFavoriteTeam = new ComboBox();
            btnChooseFavoritePlayers = new Button();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            changeApplicationSettingsToolStripMenuItem = new ToolStripMenuItem();
            resetSettingsToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem1 = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            flpFavoritePlayers = new FlowLayoutPanel();
            lbCurrentTeam = new Label();
            lbFavoritePlayers = new Label();
            btnRanking = new Button();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // cbFavoriteTeam
            // 
            cbFavoriteTeam.FormattingEnabled = true;
            cbFavoriteTeam.Location = new Point(449, 114);
            cbFavoriteTeam.Name = "cbFavoriteTeam";
            cbFavoriteTeam.Size = new Size(362, 23);
            cbFavoriteTeam.TabIndex = 0;
            // 
            // btnChooseFavoritePlayers
            // 
            btnChooseFavoritePlayers.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
            btnChooseFavoritePlayers.Location = new Point(509, 374);
            btnChooseFavoritePlayers.Name = "btnChooseFavoritePlayers";
            btnChooseFavoritePlayers.Size = new Size(252, 39);
            btnChooseFavoritePlayers.TabIndex = 2;
            btnChooseFavoritePlayers.Text = "Choose Favorite Players";
            btnChooseFavoritePlayers.UseVisualStyleBackColor = true;
            btnChooseFavoritePlayers.Click += btnChooseFavoritePlayers_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1282, 24);
            menuStrip1.TabIndex = 3;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { settingsToolStripMenuItem, exitToolStripMenuItem1 });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { changeApplicationSettingsToolStripMenuItem, resetSettingsToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(116, 22);
            settingsToolStripMenuItem.Text = "Settings";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // changeApplicationSettingsToolStripMenuItem
            // 
            changeApplicationSettingsToolStripMenuItem.Name = "changeApplicationSettingsToolStripMenuItem";
            changeApplicationSettingsToolStripMenuItem.Size = new Size(221, 22);
            changeApplicationSettingsToolStripMenuItem.Text = "Change application settings";
            changeApplicationSettingsToolStripMenuItem.Click += changeApplicationSettingsToolStripMenuItem_Click;
            // 
            // resetSettingsToolStripMenuItem
            // 
            resetSettingsToolStripMenuItem.Name = "resetSettingsToolStripMenuItem";
            resetSettingsToolStripMenuItem.Size = new Size(221, 22);
            resetSettingsToolStripMenuItem.Text = "Reset settings";
            resetSettingsToolStripMenuItem.Click += resetSettingsToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem1
            // 
            exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            exitToolStripMenuItem1.Size = new Size(116, 22);
            exitToolStripMenuItem1.Text = "Exit";
            exitToolStripMenuItem1.Click += exitToolStripMenuItem1_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(107, 22);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // flpFavoritePlayers
            // 
            flpFavoritePlayers.BorderStyle = BorderStyle.FixedSingle;
            flpFavoritePlayers.Location = new Point(20, 143);
            flpFavoritePlayers.Name = "flpFavoritePlayers";
            flpFavoritePlayers.Size = new Size(1250, 225);
            flpFavoritePlayers.TabIndex = 1;
            // 
            // lbCurrentTeam
            // 
            lbCurrentTeam.AutoSize = true;
            lbCurrentTeam.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
            lbCurrentTeam.Location = new Point(531, 76);
            lbCurrentTeam.Name = "lbCurrentTeam";
            lbCurrentTeam.Size = new Size(209, 25);
            lbCurrentTeam.TabIndex = 4;
            lbCurrentTeam.Text = "Current favorite team:";
            // 
            // lbFavoritePlayers
            // 
            lbFavoritePlayers.AutoSize = true;
            lbFavoritePlayers.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
            lbFavoritePlayers.Location = new Point(20, 109);
            lbFavoritePlayers.Name = "lbFavoritePlayers";
            lbFavoritePlayers.Size = new Size(156, 25);
            lbFavoritePlayers.TabIndex = 5;
            lbFavoritePlayers.Text = "Favorite players:";
            // 
            // btnRanking
            // 
            btnRanking.Location = new Point(1151, 41);
            btnRanking.Name = "btnRanking";
            btnRanking.Size = new Size(119, 39);
            btnRanking.TabIndex = 6;
            btnRanking.Text = "Get rankings";
            btnRanking.UseVisualStyleBackColor = true;
            btnRanking.Click += btnRanking_Click;
            // 
            // MainForm
            // 
            ClientSize = new Size(1282, 588);
            Controls.Add(btnRanking);
            Controls.Add(lbFavoritePlayers);
            Controls.Add(lbCurrentTeam);
            Controls.Add(cbFavoriteTeam);
            Controls.Add(flpFavoritePlayers);
            Controls.Add(btnChooseFavoritePlayers);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "World Cup Manager";
            Load += MainForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        private FlowLayoutPanel flpFavoritePlayers;
        private Label lbCurrentTeam;
        private Label lbFavoritePlayers;
        private Button btnRanking;
        private ToolStripMenuItem resetSettingsToolStripMenuItem;
        private ToolStripMenuItem changeApplicationSettingsToolStripMenuItem;
    }
}