namespace Fesco_Inventory
{
    partial class Office_Input
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
            this.textbox_officeCode = new Guna.UI2.WinForms.Guna2TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox_Description = new System.Windows.Forms.RichTextBox();
            this.btn_add = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Palatino Linotype", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkRed;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(227, 50);
            this.label2.TabIndex = 19;
            this.label2.Text = "Office Code*";
            // 
            // textbox_officeCode
            // 
            this.textbox_officeCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textbox_officeCode.DefaultText = "";
            this.textbox_officeCode.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textbox_officeCode.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textbox_officeCode.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textbox_officeCode.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textbox_officeCode.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textbox_officeCode.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textbox_officeCode.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textbox_officeCode.Location = new System.Drawing.Point(13, 63);
            this.textbox_officeCode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textbox_officeCode.MaxLength = 8;
            this.textbox_officeCode.Name = "textbox_officeCode";
            this.textbox_officeCode.PasswordChar = '\0';
            this.textbox_officeCode.PlaceholderText = "";
            this.textbox_officeCode.SelectedText = "";
            this.textbox_officeCode.Size = new System.Drawing.Size(450, 45);
            this.textbox_officeCode.TabIndex = 20;
            this.textbox_officeCode.TextChanged += new System.EventHandler(this.textbox_officeCode_TextChanged);
            this.textbox_officeCode.Click += new System.EventHandler(this.textbox_officeCode_Click);
            this.textbox_officeCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textbox_officeCode_KeyPress);
            this.textbox_officeCode.Leave += new System.EventHandler(this.textbox_officeCode_Leave);
            this.textbox_officeCode.Validating += new System.ComponentModel.CancelEventHandler(this.textbox_officeCode_Validating);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(12, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(242, 50);
            this.label1.TabIndex = 21;
            this.label1.Text = "Office Name*";
            // 
            // richTextBox_Description
            // 
            this.richTextBox_Description.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_Description.Font = new System.Drawing.Font("Segoe UI", 16.2F);
            this.richTextBox_Description.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.richTextBox_Description.Location = new System.Drawing.Point(13, 166);
            this.richTextBox_Description.Name = "richTextBox_Description";
            this.richTextBox_Description.Size = new System.Drawing.Size(450, 148);
            this.richTextBox_Description.TabIndex = 22;
            this.richTextBox_Description.Text = "";
            this.richTextBox_Description.Leave += new System.EventHandler(this.richTextBox_Description_Leave);
            // 
            // btn_add
            // 
            this.btn_add.Animated = true;
            this.btn_add.BorderColor = System.Drawing.Color.DarkGoldenrod;
            this.btn_add.BorderRadius = 8;
            this.btn_add.BorderThickness = 3;
            this.btn_add.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_add.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_add.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_add.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_add.FillColor = System.Drawing.Color.Goldenrod;
            this.btn_add.Font = new System.Drawing.Font("Comic Sans MS", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add.ForeColor = System.Drawing.Color.DarkRed;
            this.btn_add.Location = new System.Drawing.Point(145, 324);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(319, 61);
            this.btn_add.TabIndex = 23;
            this.btn_add.Text = "Add";
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // Office_Input
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.ClientSize = new System.Drawing.Size(476, 393);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textbox_officeCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox_Description);
            this.Controls.Add(this.btn_add);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Office_Input";
            this.Text = "Fesco";
            this.Load += new System.EventHandler(this.Office_Input_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2TextBox textbox_officeCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox_Description;
        private Guna.UI2.WinForms.Guna2Button btn_add;
    }
}