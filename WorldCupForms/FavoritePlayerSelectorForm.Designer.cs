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
            playerListView = new ListView();
            SuspendLayout();
            // 
            // flpFavoritesOne
            // 
            flpFavoritesOne.BackColor = Color.RosyBrown;
            flpFavoritesOne.BorderStyle = BorderStyle.FixedSingle;
            flpFavoritesOne.Location = new Point(330, 12);
            flpFavoritesOne.Name = "flpFavoritesOne";
            flpFavoritesOne.Size = new Size(349, 146);
            flpFavoritesOne.TabIndex = 1;
            flpFavoritesOne.MouseDown += flpFavoritesOne_MouseDown;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(330, 478);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(145, 30);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save Selection";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(534, 478);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(145, 30);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // flpFavoritesTwo
            // 
            flpFavoritesTwo.BackColor = Color.RosyBrown;
            flpFavoritesTwo.BorderStyle = BorderStyle.FixedSingle;
            flpFavoritesTwo.Location = new Point(330, 164);
            flpFavoritesTwo.Name = "flpFavoritesTwo";
            flpFavoritesTwo.Size = new Size(349, 146);
            flpFavoritesTwo.TabIndex = 2;
            // 
            // flpFavoritesThree
            // 
            flpFavoritesThree.BackColor = Color.RosyBrown;
            flpFavoritesThree.BorderStyle = BorderStyle.FixedSingle;
            flpFavoritesThree.Location = new Point(330, 316);
            flpFavoritesThree.Name = "flpFavoritesThree";
            flpFavoritesThree.Size = new Size(349, 146);
            flpFavoritesThree.TabIndex = 2;
            // 
            // playerListView
            // 
            playerListView.Location = new Point(11, 12);
            playerListView.Name = "playerListView";
            playerListView.Size = new Size(313, 496);
            playerListView.TabIndex = 4;
            playerListView.UseCompatibleStateImageBehavior = false;
            playerListView.MouseDown += playerListView_MouseDown;
            // 
            // FavoritePlayerSelectorForm
            // 
            ClientSize = new Size(691, 520);
            Controls.Add(playerListView);
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
        private ListView playerListView;
    }
}