namespace Fesco_Inventory
{
    partial class Otp_UC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkLabel_otherWay = new System.Windows.Forms.LinkLabel();
            this.btn_verifyOtp = new Guna.UI2.WinForms.Guna2Button();
            this.textbox_otp = new Guna.UI2.WinForms.Guna2TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Bisque;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.linkLabel_otherWay);
            this.panel1.Controls.Add(this.btn_verifyOtp);
            this.panel1.Controls.Add(this.textbox_otp);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(432, 278);
            this.panel1.TabIndex = 79;
            // 
            // linkLabel_otherWay
            // 
            this.linkLabel_otherWay.ActiveLinkColor = System.Drawing.Color.RoyalBlue;
            this.linkLabel_otherWay.AutoSize = true;
            this.linkLabel_otherWay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabel_otherWay.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel_otherWay.LinkColor = System.Drawing.Color.DarkRed;
            this.linkLabel_otherWay.Location = new System.Drawing.Point(6, 240);
            this.linkLabel_otherWay.Name = "linkLabel_otherWay";
            this.linkLabel_otherWay.Size = new System.Drawing.Size(125, 28);
            this.linkLabel_otherWay.TabIndex = 80;
            this.linkLabel_otherWay.TabStop = true;
            this.linkLabel_otherWay.Text = "Resend OTP";
            this.linkLabel_otherWay.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_otherWay_LinkClicked);
            // 
            // btn_verifyOtp
            // 
            this.btn_verifyOtp.Animated = true;
            this.btn_verifyOtp.BorderColor = System.Drawing.Color.DarkGoldenrod;
            this.btn_verifyOtp.BorderRadius = 8;
            this.btn_verifyOtp.BorderThickness = 2;
            this.btn_verifyOtp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_verifyOtp.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_verifyOtp.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_verifyOtp.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_verifyOtp.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_verifyOtp.FillColor = System.Drawing.Color.Goldenrod;
            this.btn_verifyOtp.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_verifyOtp.ForeColor = System.Drawing.Color.DarkRed;
            this.btn_verifyOtp.Location = new System.Drawing.Point(200, 168);
            this.btn_verifyOtp.Name = "btn_verifyOtp";
            this.btn_verifyOtp.Size = new System.Drawing.Size(202, 51);
            this.btn_verifyOtp.TabIndex = 79;
            this.btn_verifyOtp.Text = "Verify OTP";
            this.btn_verifyOtp.Click += new System.EventHandler(this.btn_verifyOtp_Click);
            // 
            // textbox_otp
            // 
            this.textbox_otp.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textbox_otp.DefaultText = "";
            this.textbox_otp.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textbox_otp.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textbox_otp.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textbox_otp.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textbox_otp.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textbox_otp.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textbox_otp.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textbox_otp.Location = new System.Drawing.Point(200, 115);
            this.textbox_otp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textbox_otp.MaxLength = 6;
            this.textbox_otp.Name = "textbox_otp";
            this.textbox_otp.PasswordChar = '\0';
            this.textbox_otp.PlaceholderText = "";
            this.textbox_otp.SelectedText = "";
            this.textbox_otp.Size = new System.Drawing.Size(202, 45);
            this.textbox_otp.TabIndex = 76;
            this.textbox_otp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textbox_otp_KeyPress);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 19.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(15, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 44);
            this.label1.TabIndex = 74;
            this.label1.Text = "Enter OTP ";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Palatino Linotype", 19.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkRed;
            this.label4.Location = new System.Drawing.Point(52, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(323, 44);
            this.label4.TabIndex = 73;
            this.label4.Text = "Remaining Time : 60s";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Otp_UC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.Controls.Add(this.panel1);
            this.Name = "Otp_UC";
            this.Size = new System.Drawing.Size(432, 278);
            this.Load += new System.EventHandler(this.Otp_UC_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel linkLabel_otherWay;
        private Guna.UI2.WinForms.Guna2Button btn_verifyOtp;
        private Guna.UI2.WinForms.Guna2TextBox textbox_otp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer1;
    }
}
