using Fesco_Inventory.BL;
using MailKit.Security;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace Fesco_Inventory
{
    public partial class Otp_UC : UserControl
    {

        Panel pan;
        userBL user;
        Panel panel;
        Label label;

        Form f;
        public Otp_UC(Panel pan, userBL user,Panel panel, Form f, Label label)
        {
            InitializeComponent();
            this.panel = panel;
            this.user = user;
            this.pan = pan;
            this.f = f;
            this.label = label;
        }

        string otp = "";

        private int remainingTime = 60;

        public void set_OTP()
        {
            Random rand = new Random();
            int total = 0;
            string recap = "";

            do
            {
                int chr = rand.Next(48, 58); // Generates random number between 48 ('0') and 57 ('9')
                recap = recap + (char)chr;
                total++;
                if (total >= 6) // Set the desired length of the OTP (here, 6 digits)
                {
                    break;
                }
            }
            while (true);

            otp = recap;
            //MessageBox.Show(otp);
        }

        private void getUC_Panel(UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            panel.Controls.Clear();
            panel.Controls.Add(uc);
            uc.BringToFront();
        }

        public void send_OTP(bool msg)
        {
            try
            {
                set_OTP();
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Fesco Harware Lab Management", "fescohlm2323@outlook.com")); // Replace with your name and email
                message.To.Add(new MailboxAddress("", user.Email)); // Recipient's email address
                message.Subject = "User Verification OTP!";
                string body = "Your OTP for Fesco 'Hardware Lab Management System' User Verfication is: " + otp + ".\n\nTo keep your account secure, please do not share it with anyone.\n\nThankyou!\nRegard\nFesco Hardware Lab Management.";
                message.Body = new TextPart("plain") { Text = body };

                // SMTP Client setup
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true; // Ignore SSL certificate errors
                    client.Connect("smtp-mail.outlook.com", 587, SecureSocketOptions.StartTls); // Replace with your SMTP server address and port

                    // Replace 'your_email@example.com' and 'your_password' with your SMTP server login credentials
                    client.Authenticate("fescohlm2323@outlook.com", "feshardlm#23");

                    client.Send(message);
                    client.Disconnect(true);
                }

                if (msg)
                    MessageBox.Show("OTP sent to your Email Address!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("New OTP sent to your Email Address!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                remainingTime = 60;
                timer1 = new Timer();
                timer1.Interval = 1000; // 1 second
                timer1.Tick += timer1_Tick;
                timer1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending Email: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //textbox_otp.Text = ex.Message;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            remainingTime--;
            label4.Text = "Remaining time : " + remainingTime.ToString() + "s";

            // Check if the timer reached zero
            if (remainingTime == 0)
            {
                timer1.Stop();
                linkLabel_otherWay.Enabled = true;
                linkLabel_otherWay.Visible = true;
                textbox_otp.Enabled = false;
                btn_verifyOtp.Enabled = false;
            }
        }

        private void textbox_otp_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex regex = new Regex(@"^[\d]+$");

            // Check if the pressed key matches the pattern
            if (!regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void btn_verifyOtp_Click(object sender, EventArgs e)
        {
            if (otp == textbox_otp.Text)
            {
                MessageBox.Show("OTP verified!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EditUserPass nf = new EditUserPass(pan, user, 4, label);
                nf.StartPosition = FormStartPosition.CenterParent;
                nf.ShowDialog();
                f.Close();
            }

            else
            {
                MessageBox.Show("OTP verification failed!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                timer1.Stop();
                MessageBox.Show("Sending New OTP to your Email Address!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                waiting uc = new waiting(pan, user,panel,f, label);
                getUC_Panel(uc);
            }
        }

        private void linkLabel_otherWay_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Sending New OTP to your Email Address!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            waiting uc = new waiting(pan, user, panel, f, label);
            getUC_Panel(uc);
        }

        private void Otp_UC_Load(object sender, EventArgs e)
        {
            linkLabel_otherWay.Enabled = false;
            linkLabel_otherWay.Visible = false;
            send_OTP(true);
        }
    }
}
