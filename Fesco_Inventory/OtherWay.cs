using Fesco_Inventory.BL;
using Fesco_Inventory.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fesco_Inventory
{
    public partial class OtherWay : Form
    {
        Panel pan;
        userBL user;
        Label label;

        string otp = "";

        public OtherWay(Panel pan, Label label)
        {
            InitializeComponent();
            this.pan = pan;
            this.label = label;
        }

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

        public void send_OTP(bool msg)
        {
            try
            {
                //panel3.SendToBack();

                string sender_email = "fescohardwarelabmanagement@outlook.com";
                string sender_Pass = "fesco?hlm2023";
                set_OTP();
                string textOtp = "Your OTP is = " + otp.ToString();
                MailMessage mm = new MailMessage();
                SmtpClient sc = new SmtpClient("smtp-mail.outlook.com");
                mm.From = new MailAddress(sender_email, "Fesco Hardware Lab Management");
                mm.To.Add(user.Email);
                mm.Subject = "User Verification OTP!";
                mm.Body = textOtp;
                sc.Port = 587;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new NetworkCredential(sender_email, sender_Pass);
                sc.EnableSsl = true;
                sc.Send(mm);
                if (msg)
                    MessageBox.Show("OTP sent to your Email Address!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("New OTP sent to your Email Address!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending Email: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void OtherWay_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            textbox_user.Focus();
        }

        private void getUC_Pan(UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            pan.Controls.Clear();
            pan.Controls.Add(uc);
            uc.BringToFront();
        }

        private void getUC_Panel(UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            panel2.Controls.Clear();
            panel2.Controls.Add(uc);
            uc.BringToFront();
        }

        private void textbox_otp_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Regex regex = new Regex(@"^[\d]+$");

            // Check if the pressed key matches the pattern
            if (!regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void btn_verify_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textbox_email.Text) && !string.IsNullOrEmpty(textbox_user.Text))
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(string.Concat("Select id from Employee where email = '", textbox_email.Text, "' and Username = '", textbox_user.Text, "'"), con);
                object val = cmd.ExecuteScalar();

                if (val != null)
                {
                    MessageBox.Show("Username and Email verified!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    user = userDL.getUserFromEmployeeTable((int)val);
                    panel2.Visible = true;
                    waiting uc = new waiting(pan,user,panel2,this, label);
                    getUC_Panel(uc);
                    //OTP nf = new OTP(pan,user);
                    //nf.StartPosition = FormStartPosition.CenterParent;
                    //nf.ShowDialog();
                    //this.Close();
                }

                else
                {
                    MessageBox.Show("Username or Email not found!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    textbox_user.Text = "";
                    textbox_email.Text = "";
                }
            }
            else
                MessageBox.Show("Both fields are required!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
    }
}
