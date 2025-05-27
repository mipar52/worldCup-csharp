namespace WorldCupForms
{
    partial class StartupForm
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
            cbLanguage = new ComboBox();
            rbMen = new RadioButton();
            rbWomen = new RadioButton();
            grpChampionship = new GroupBox();
            lbLanguage = new Label();
            label1 = new Label();
            btnConfirm = new Button();
            btnCancel = new Button();
            groupBox1 = new GroupBox();
            rbApi = new RadioButton();
            rbLocal = new RadioButton();
            grpChampionship.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // cbLanguage
            // 
            cbLanguage.FormattingEnabled = true;
            cbLanguage.Location = new Point(182, 91);
            cbLanguage.Name = "cbLanguage";
            cbLanguage.Size = new Size(340, 23);
            cbLanguage.TabIndex = 0;
            cbLanguage.SelectedIndexChanged += cbLanguage_SelectedIndexChanged;
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
            // grpChampionship
            // 
            grpChampionship.Controls.Add(rbMen);
            grpChampionship.Controls.Add(rbWomen);
            grpChampionship.Location = new Point(182, 136);
            grpChampionship.Name = "grpChampionship";
            grpChampionship.Size = new Size(340, 95);
            grpChampionship.TabIndex = 3;
            grpChampionship.TabStop = false;
            grpChampionship.Text = "World Championship Picker";
            // 
            // lbLanguage
            // 
            lbLanguage.AutoSize = true;
            lbLanguage.Location = new Point(182, 58);
            lbLanguage.Name = "lbLanguage";
            lbLanguage.Size = new Size(155, 15);
            lbLanguage.TabIndex = 4;
            lbLanguage.Text = "Select application language:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(188, 18);
            label1.Name = "label1";
            label1.Size = new Size(350, 15);
            label1.TabIndex = 5;
            label1.Text = "WELCOME TO THE WORLD CHAMPIONSHIP STATS APPLICATION";
            // 
            // btnConfirm
            // 
            btnConfirm.Location = new Point(182, 357);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(134, 43);
            btnConfirm.TabIndex = 6;
            btnConfirm.Text = "CONFIRM";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(388, 357);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(134, 43);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "CANCEL";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rbApi);
            groupBox1.Controls.Add(rbLocal);
            groupBox1.Location = new Point(182, 238);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(340, 95);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Data fetcher";
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
            rbApi.CheckedChanged += radioButton1_CheckedChanged;
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
            // StartupForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            Controls.Add(btnCancel);
            Controls.Add(btnConfirm);
            Controls.Add(label1);
            Controls.Add(lbLanguage);
            Controls.Add(grpChampionship);
            Controls.Add(cbLanguage);
            Name = "StartupForm";
            Text = "StartupForm";
            Load += StartupForm_Load;
            grpChampionship.ResumeLayout(false);
            grpChampionship.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cbLanguage;
        private RadioButton rbMen;
        private RadioButton rbWomen;
        private GroupBox grpChampionship;
        private Label lbLanguage;
        private Label label1;
        private Button btnConfirm;
        private Button btnCancel;
        private GroupBox groupBox1;
        private RadioButton rbApi;
        private RadioButton rbLocal;
    }
}