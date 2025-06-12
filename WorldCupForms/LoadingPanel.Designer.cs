namespace CustomControls
{
    partial class LoadingPanel
    {
        private ProgressBar progressBar;
        private Label lblMessage;

        private void InitializeComponent()
        {
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(20, 20);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(260, 20);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.MarqueeAnimationSpeed = 30;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(20, 50);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(110, 15);
            this.lblMessage.Text = "Loading, please wait...";
            // 
            // LoadingPanel
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblMessage);
            this.Name = "LoadingPanel";
            this.Size = new System.Drawing.Size(300, 80);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
