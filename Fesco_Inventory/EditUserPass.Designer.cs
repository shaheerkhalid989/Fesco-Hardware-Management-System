namespace Fesco_Inventory
{
    partial class EditUserPass
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
            this.label19 = new System.Windows.Forms.Label();
            this.textbox_user = new Guna.UI2.WinForms.Guna2TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textbox_pass = new Guna.UI2.WinForms.Guna2TextBox();
            this.btn_applyChanges = new Guna.UI2.WinForms.Guna2Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textbox_reenterPass = new Guna.UI2.WinForms.Guna2TextBox();
            this.btn_hide = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // label19
            // 
            this.label19.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Palatino Linotype", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.DarkRed;
            this.label19.Location = new System.Drawing.Point(5, 6);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(203, 50);
            this.label19.TabIndex = 55;
            this.label19.Text = "Username*";
            // 
            // textbox_user
            // 
            this.textbox_user.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textbox_user.DefaultText = "";
            this.textbox_user.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textbox_user.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textbox_user.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textbox_user.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textbox_user.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textbox_user.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textbox_user.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textbox_user.Location = new System.Drawing.Point(12, 61);
            this.textbox_user.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textbox_user.Name = "textbox_user";
            this.textbox_user.PasswordChar = '\0';
            this.textbox_user.PlaceholderText = "";
            this.textbox_user.SelectedText = "";
            this.textbox_user.Size = new System.Drawing.Size(447, 45);
            this.textbox_user.TabIndex = 56;
            this.textbox_user.TextChanged += new System.EventHandler(this.textbox_user_TextChanged);
            this.textbox_user.Click += new System.EventHandler(this.textbox_user_Click);
            this.textbox_user.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textbox_user_KeyPress);
            this.textbox_user.Validating += new System.ComponentModel.CancelEventHandler(this.textbox_user_Validating);
            // 
            // label20
            // 
            this.label20.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Palatino Linotype", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.DarkRed;
            this.label20.Location = new System.Drawing.Point(5, 111);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(282, 50);
            this.label20.TabIndex = 57;
            this.label20.Text = "New Password*";
            // 
            // textbox_pass
            // 
            this.textbox_pass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textbox_pass.DefaultText = "";
            this.textbox_pass.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textbox_pass.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textbox_pass.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textbox_pass.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textbox_pass.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textbox_pass.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textbox_pass.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textbox_pass.Location = new System.Drawing.Point(12, 166);
            this.textbox_pass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textbox_pass.Name = "textbox_pass";
            this.textbox_pass.PasswordChar = '\0';
            this.textbox_pass.PlaceholderText = "";
            this.textbox_pass.SelectedText = "";
            this.textbox_pass.Size = new System.Drawing.Size(447, 45);
            this.textbox_pass.TabIndex = 58;
            this.textbox_pass.TextChanged += new System.EventHandler(this.textbox_pass_TextChanged);
            this.textbox_pass.Click += new System.EventHandler(this.textbox_pass_Click);
            this.textbox_pass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textbox_pass_KeyPress);
            this.textbox_pass.Validating += new System.ComponentModel.CancelEventHandler(this.textbox_pass_Validating);
            // 
            // btn_applyChanges
            // 
            this.btn_applyChanges.Animated = true;
            this.btn_applyChanges.BorderColor = System.Drawing.Color.DarkGoldenrod;
            this.btn_applyChanges.BorderRadius = 8;
            this.btn_applyChanges.BorderThickness = 3;
            this.btn_applyChanges.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_applyChanges.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_applyChanges.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_applyChanges.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_applyChanges.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_applyChanges.FillColor = System.Drawing.Color.Goldenrod;
            this.btn_applyChanges.Font = new System.Drawing.Font("Comic Sans MS", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_applyChanges.ForeColor = System.Drawing.Color.DarkRed;
            this.btn_applyChanges.Location = new System.Drawing.Point(203, 326);
            this.btn_applyChanges.Name = "btn_applyChanges";
            this.btn_applyChanges.Size = new System.Drawing.Size(299, 61);
            this.btn_applyChanges.TabIndex = 59;
            this.btn_applyChanges.Text = "Apply Changes";
            this.btn_applyChanges.Click += new System.EventHandler(this.btn_applyChanges_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(6, 217);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(346, 50);
            this.label1.TabIndex = 60;
            this.label1.Text = "Re-Enter Password*";
            // 
            // textbox_reenterPass
            // 
            this.textbox_reenterPass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textbox_reenterPass.DefaultText = "";
            this.textbox_reenterPass.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textbox_reenterPass.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textbox_reenterPass.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textbox_reenterPass.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textbox_reenterPass.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textbox_reenterPass.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textbox_reenterPass.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textbox_reenterPass.Location = new System.Drawing.Point(13, 272);
            this.textbox_reenterPass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textbox_reenterPass.Name = "textbox_reenterPass";
            this.textbox_reenterPass.PasswordChar = '\0';
            this.textbox_reenterPass.PlaceholderText = "";
            this.textbox_reenterPass.SelectedText = "";
            this.textbox_reenterPass.Size = new System.Drawing.Size(447, 45);
            this.textbox_reenterPass.TabIndex = 61;
            // 
            // btn_hide
            // 
            this.btn_hide.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_hide.Animated = true;
            this.btn_hide.AutoRoundedCorners = true;
            this.btn_hide.BorderRadius = 23;
            this.btn_hide.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_hide.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_hide.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_hide.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_hide.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_hide.FillColor = System.Drawing.Color.Transparent;
            this.btn_hide.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_hide.ForeColor = System.Drawing.Color.White;
            this.btn_hide.Image = global::Fesco_Inventory.Properties.Resources.show;
            this.btn_hide.ImageSize = new System.Drawing.Size(30, 30);
            this.btn_hide.Location = new System.Drawing.Point(464, 270);
            this.btn_hide.Name = "btn_hide";
            this.btn_hide.Size = new System.Drawing.Size(48, 48);
            this.btn_hide.TabIndex = 62;
            this.btn_hide.Click += new System.EventHandler(this.btn_hide_Click);
            // 
            // EditUserPass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.ClientSize = new System.Drawing.Size(514, 395);
            this.Controls.Add(this.btn_hide);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textbox_reenterPass);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.textbox_user);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.textbox_pass);
            this.Controls.Add(this.btn_applyChanges);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EditUserPass";
            this.Text = "Fesco";
            this.Load += new System.EventHandler(this.EditUserPass_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label19;
        private Guna.UI2.WinForms.Guna2TextBox textbox_user;
        private System.Windows.Forms.Label label20;
        private Guna.UI2.WinForms.Guna2TextBox textbox_pass;
        private Guna.UI2.WinForms.Guna2Button btn_applyChanges;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox textbox_reenterPass;
        private Guna.UI2.WinForms.Guna2Button btn_hide;
    }
}