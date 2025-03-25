namespace Fesco_Inventory
{
    partial class ReportType
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
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_reportType = new System.Windows.Forms.ComboBox();
            this.btn_add = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Palatino Linotype", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkRed;
            this.label2.Location = new System.Drawing.Point(2, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(214, 45);
            this.label2.TabIndex = 2;
            this.label2.Text = "Report Type*";
            // 
            // comboBox_reportType
            // 
            this.comboBox_reportType.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBox_reportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_reportType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox_reportType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_reportType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.comboBox_reportType.FormattingEnabled = true;
            this.comboBox_reportType.Items.AddRange(new object[] {
            "Departmental Allocations Report (Overall)",
            "Departmental Allocations Report (Single Department)",
            "Departmental Allocations Report (Single Item)",
            "Departmental Repairs Report (Single Department)",
            "Discarded Items Report (Overall)",
            "Discarded Items Report (Single Item)"});
            this.comboBox_reportType.Location = new System.Drawing.Point(9, 53);
            this.comboBox_reportType.Name = "comboBox_reportType";
            this.comboBox_reportType.Size = new System.Drawing.Size(583, 36);
            this.comboBox_reportType.TabIndex = 10;
            // 
            // btn_add
            // 
            this.btn_add.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_add.Animated = true;
            this.btn_add.BorderColor = System.Drawing.Color.DarkGoldenrod;
            this.btn_add.BorderRadius = 8;
            this.btn_add.BorderThickness = 3;
            this.btn_add.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_add.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_add.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_add.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_add.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_add.FillColor = System.Drawing.Color.Goldenrod;
            this.btn_add.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add.ForeColor = System.Drawing.Color.DarkRed;
            this.btn_add.Location = new System.Drawing.Point(366, 95);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(226, 58);
            this.btn_add.TabIndex = 19;
            this.btn_add.Text = "Get Report";
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // ReportType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.ClientSize = new System.Drawing.Size(601, 164);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.comboBox_reportType);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ReportType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fesco";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_reportType;
        private Guna.UI2.WinForms.Guna2Button btn_add;
    }
}