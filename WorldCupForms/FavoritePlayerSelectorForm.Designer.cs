namespace WorldCupForms
{
    partial class FavoritePlayerSelectorForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flpFavoritesOne;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            flpFavoritesOne = new FlowLayoutPanel();
            btnSave = new Button();
            btnCancel = new Button();
            flpFavoritesTwo = new FlowLayoutPanel();
            flpFavoritesThree = new FlowLayoutPanel();
            flpAllPlayers = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // flpFavoritesOne
            // 
            flpFavoritesOne.BackColor = Color.RosyBrown;
            flpFavoritesOne.BorderStyle = BorderStyle.FixedSingle;
            flpFavoritesOne.Location = new Point(476, 12);
            flpFavoritesOne.Name = "flpFavoritesOne";
            flpFavoritesOne.Size = new Size(423, 191);
            flpFavoritesOne.TabIndex = 1;
            flpFavoritesOne.MouseDown += flpFavoritesOne_MouseDown;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(476, 586);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(178, 30);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save Selection";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(718, 586);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(181, 30);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // flpFavoritesTwo
            // 
            flpFavoritesTwo.BackColor = Color.RosyBrown;
            flpFavoritesTwo.BorderStyle = BorderStyle.FixedSingle;
            flpFavoritesTwo.Location = new Point(476, 209);
            flpFavoritesTwo.Name = "flpFavoritesTwo";
            flpFavoritesTwo.Size = new Size(423, 177);
            flpFavoritesTwo.TabIndex = 2;
            // 
            // flpFavoritesThree
            // 
            flpFavoritesThree.BackColor = Color.RosyBrown;
            flpFavoritesThree.BorderStyle = BorderStyle.FixedSingle;
            flpFavoritesThree.Location = new Point(476, 392);
            flpFavoritesThree.Name = "flpFavoritesThree";
            flpFavoritesThree.Size = new Size(423, 188);
            flpFavoritesThree.TabIndex = 2;
            // 
            // flpAllPlayers
            // 
            flpAllPlayers.Location = new Point(4, 12);
            flpAllPlayers.Name = "flpAllPlayers";
            flpAllPlayers.Size = new Size(466, 604);
            flpAllPlayers.TabIndex = 4;
            // 
            // FavoritePlayerSelectorForm
            // 
            ClientSize = new Size(911, 628);
            Controls.Add(flpAllPlayers);
            Controls.Add(flpFavoritesThree);
            Controls.Add(flpFavoritesTwo);
            Controls.Add(flpFavoritesOne);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Name = "FavoritePlayerSelectorForm";
            Text = "Select Your Favorite Players";
            Load += FavoritePlayerSelectorForm_Load;
            ResumeLayout(false);
        }
        private FlowLayoutPanel flpFavoritesTwo;
        private FlowLayoutPanel flpFavoritesThree;
        private FlowLayoutPanel flpAllPlayers;
    }
}