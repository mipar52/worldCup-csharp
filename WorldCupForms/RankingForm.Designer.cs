namespace WorldCupForms
{
    partial class RankingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RankingForm));
            dgvPlayerRanking = new DataGridView();
            dgvMatchRanking = new DataGridView();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            printItem = new ToolStripMenuItem();
            printPreviewToolStripMenuItem = new ToolStripMenuItem();
            printDialogRankings = new PrintDialog();
            printDocumentRankings = new System.Drawing.Printing.PrintDocument();
            printPreviewDialog = new PrintPreviewDialog();
            ((System.ComponentModel.ISupportInitialize)dgvPlayerRanking).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvMatchRanking).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvPlayerRanking
            // 
            dgvPlayerRanking.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPlayerRanking.Location = new Point(12, 27);
            dgvPlayerRanking.Name = "dgvPlayerRanking";
            dgvPlayerRanking.Size = new Size(579, 505);
            dgvPlayerRanking.TabIndex = 0;
            // 
            // dgvMatchRanking
            // 
            dgvMatchRanking.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMatchRanking.Location = new Point(666, 27);
            dgvMatchRanking.Name = "dgvMatchRanking";
            dgvMatchRanking.Size = new Size(457, 505);
            dgvMatchRanking.TabIndex = 1;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1152, 24);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { printItem, printPreviewToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // printItem
            // 
            printItem.Name = "printItem";
            printItem.ShortcutKeys = Keys.Control | Keys.P;
            printItem.Size = new Size(216, 22);
            printItem.Text = "Print";
            printItem.Click += printItem_Click;
            // 
            // printPreviewToolStripMenuItem
            // 
            printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            printPreviewToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.P;
            printPreviewToolStripMenuItem.Size = new Size(216, 22);
            printPreviewToolStripMenuItem.Text = "Print preview";
            printPreviewToolStripMenuItem.Click += printPreviewToolStripMenuItem_Click;
            // 
            // printDialogRankings
            // 
            printDialogRankings.UseEXDialog = true;
            // 
            // printDocumentRankings
            // 
            printDocumentRankings.BeginPrint += printDocumentRankings_BeginPrint;
            printDocumentRankings.EndPrint += printDocumentRankings_EndPrint;
            printDocumentRankings.PrintPage += printDocumentRankings_PrintPage;
            // 
            // printPreviewDialog
            // 
            printPreviewDialog.AutoScrollMargin = new Size(0, 0);
            printPreviewDialog.AutoScrollMinSize = new Size(0, 0);
            printPreviewDialog.ClientSize = new Size(400, 300);
            printPreviewDialog.Enabled = true;
            printPreviewDialog.Icon = (Icon)resources.GetObject("printPreviewDialog.Icon");
            printPreviewDialog.Name = "printPreviewDialog";
            printPreviewDialog.Visible = false;
            // 
            // RankingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1152, 544);
            Controls.Add(dgvMatchRanking);
            Controls.Add(dgvPlayerRanking);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "RankingForm";
            Text = "RankingForm";
            Load += RankingForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPlayerRanking).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvMatchRanking).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvPlayerRanking;
        private DataGridView dgvMatchRanking;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem printItem;
        private PrintDialog printDialogRankings;
        private System.Drawing.Printing.PrintDocument printDocumentRankings;
        private ToolStripMenuItem printPreviewToolStripMenuItem;
        private PrintPreviewDialog printPreviewDialog;
    }
}