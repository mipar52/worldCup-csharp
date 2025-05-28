namespace WorldCupForms
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
            btnCancel = new Button();
            btnConfirm = new Button();
            label1 = new Label();
            lbLanguage = new Label();
            grpChampionship = new GroupBox();
            rbMen = new RadioButton();
            rbWomen = new RadioButton();
            cbLanguage = new ComboBox();
            gbData = new GroupBox();
            rbApi = new RadioButton();
            rbLocal = new RadioButton();
            grpChampionship.SuspendLayout();
            gbData.SuspendLayout();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(405, 367);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(134, 43);
            btnCancel.TabIndex = 13;
            btnCancel.Text = "CANCEL";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click_1;
            // 
            // btnConfirm
            // 
            btnConfirm.Location = new Point(205, 367);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(134, 43);
            btnConfirm.TabIndex = 12;
            btnConfirm.Text = "CONFIRM";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(205, 9);
            label1.Name = "label1";
            label1.Size = new Size(334, 15);
            label1.TabIndex = 11;
            label1.Text = "Want a change? Feel free to change your app preferences here";
            // 
            // lbLanguage
            // 
            lbLanguage.AutoSize = true;
            lbLanguage.Location = new Point(205, 54);
            lbLanguage.Name = "lbLanguage";
            lbLanguage.Size = new Size(155, 15);
            lbLanguage.TabIndex = 10;
            lbLanguage.Text = "Select application language:";
            // 
            // grpChampionship
            // 
            grpChampionship.Controls.Add(rbMen);
            grpChampionship.Controls.Add(rbWomen);
            grpChampionship.Location = new Point(205, 140);
            grpChampionship.Name = "grpChampionship";
            grpChampionship.Size = new Size(340, 95);
            grpChampionship.TabIndex = 9;
            grpChampionship.TabStop = false;
            grpChampionship.Text = "World Championship Picker";
            // 
            // rbMen
            // 
            rbMen.AutoSize = true;
            rbMen.Location = new Point(6, 22);
            rbMen.Name = "rbMen";
            rbMen.Size = new Size(200, 19);
            rbMen.TabIndex = 1;
            rbMen.TabStop = true;
            rbMen.Text = "Men World Championship (2018)";
            rbMen.UseVisualStyleBackColor = true;
            // 
            // rbWomen
            // 
            rbWomen.AutoSize = true;
            rbWomen.Location = new Point(6, 56);
            rbWomen.Name = "rbWomen";
            rbWomen.Size = new Size(218, 19);
            rbWomen.TabIndex = 2;
            rbWomen.TabStop = true;
            rbWomen.Text = "Women World Championship (2019)";
            rbWomen.UseVisualStyleBackColor = true;
            // 
            // cbLanguage
            // 
            cbLanguage.FormattingEnabled = true;
            cbLanguage.Location = new Point(205, 95);
            cbLanguage.Name = "cbLanguage";
            cbLanguage.Size = new Size(340, 23);
            cbLanguage.TabIndex = 8;
            // 
            // gbData
            // 
            gbData.Controls.Add(rbApi);
            gbData.Controls.Add(rbLocal);
            gbData.Location = new Point(205, 251);
            gbData.Name = "gbData";
            gbData.Size = new Size(340, 95);
            gbData.TabIndex = 14;
            gbData.TabStop = false;
            gbData.Text = "Data fetcher";
            gbData.Enter += groupBox1_Enter;
            // 
            // rbApi
            // 
            rbApi.AutoSize = true;
            rbApi.Location = new Point(6, 22);
            rbApi.Name = "rbApi";
            rbApi.Size = new Size(161, 19);
            rbApi.TabIndex = 1;
            rbApi.TabStop = true;
            rbApi.Text = "Via API (internet required)";
            rbApi.UseVisualStyleBackColor = true;
            // 
            // rbLocal
            // 
            rbLocal.AutoSize = true;
            rbLocal.Location = new Point(6, 56);
            rbLocal.Name = "rbLocal";
            rbLocal.Size = new Size(251, 19);
            rbLocal.TabIndex = 2;
            rbLocal.TabStop = true;
            rbLocal.Text = "Locally (via local files, no internet required)";
            rbLocal.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(gbData);
            Controls.Add(btnCancel);
            Controls.Add(btnConfirm);
            Controls.Add(label1);
            Controls.Add(lbLanguage);
            Controls.Add(grpChampionship);
            Controls.Add(cbLanguage);
            Name = "SettingsForm";
            Text = "SettingsForm";
            Load += SettingsForm_Load;
            grpChampionship.ResumeLayout(false);
            grpChampionship.PerformLayout();
            gbData.ResumeLayout(false);
            gbData.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancel;
        private Button btnConfirm;
        private Label label1;
        private Label lbLanguage;
        private GroupBox grpChampionship;
        private RadioButton rbMen;
        private RadioButton rbWomen;
        private ComboBox cbLanguage;
        private GroupBox gbData;
        private RadioButton rbApi;
        private RadioButton rbLocal;
    }
}