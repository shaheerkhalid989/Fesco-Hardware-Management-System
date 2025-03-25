using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fesco_Inventory.BL;
using Fesco_Inventory.DL;
using System.Threading;
using System.Diagnostics.Eventing.Reader;

namespace Fesco_Inventory
{
    public partial class login : UserControl
    {
        Panel pan;
        string user;
        string pass;
        bool check;
        Label label;

        public login(Panel pan, string user, string pass, bool check, Label label)
        {
            InitializeComponent();
            this.pan = pan;
            this.user = user;
            this.pass = pass;
            this.check = check;
            this.label = label;
        }

        private void login_Load(object sender, EventArgs e)
        {
            checkBox1.CheckedChanged -= this.checkBox1_CheckedChanged;

            textbox_Pass.Text = pass;
            textbox_user.Text = user;
            checkBox1.Checked = check;

            checkBox1.CheckedChanged += this.checkBox1_CheckedChanged;

            textbox_Pass.PasswordChar = '*';
            btn_hide.Image = Properties.Resources.show;
        }

        private void btn_hide_Click(object sender, EventArgs e)
        {
            if (textbox_Pass.PasswordChar == '\0')
            {
                textbox_Pass.PasswordChar = '*';
                btn_hide.Image = Properties.Resources.show;
            }

            else if (textbox_Pass.PasswordChar == '*')
            {
                textbox_Pass.PasswordChar = '\0';
                btn_hide.Image = Properties.Resources.hide;
            }
        }

        private void getUC(UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            pan.Controls.Clear();
            pan.Controls.Add(uc);
            uc.BringToFront();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {


                if (textbox_Pass.Text != "" && textbox_user.Text != "")
                {
                    if (checkBox1.Checked)
                    {
                        var con = Configuration.getInstance().getConnection();
                        SqlCommand cmd = new SqlCommand(string.Concat("Select id from Employee where Username = '", textbox_user.Text, "' and UserPassword = '", textbox_Pass.Text, "'"), con);
                        object value = cmd.ExecuteScalar();

                        if (value != null)
                        {
                            userBL user = userDL.getUserFromEmployeeTable((int)value);

                            if (user.Isdeleted == true)
                            {
                                MessageBox.Show("User Deleted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                textbox_user.Text = "";
                                textbox_Pass.Text = "";
                            }

                            else
                            {       
                                SqlCommand Role_ = new SqlCommand(string.Concat("select [Value] from [Lookup] where id = ", user.Role.ToString(), " and Category = 'ROLE'"), con);
                                object RoleVal = Role_.ExecuteScalar();
                                string RoleValue = (string)RoleVal;

                                if (user.FirstLogin == true && user.SecQuestion != 0)
                                {
                                    EditUserPass newform = new EditUserPass(pan, user, 4, label);
                                    newform.ShowDialog();
                                }

                                else if (user.FirstLogin == true && user.SecQuestion == 0)
                                {
                                    EditUserPass newform = new EditUserPass(pan, user, 2, label);
                                    newform.ShowDialog();
                                }

                                else
                                {
                                    Loading uc = new Loading(pan, user, RoleValue, label);
                                    getUC(uc);
                                }
                            }
                        }


                        else
                        {
                            MessageBox.Show("Username and Pasword not found!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            textbox_user.Text = "";
                            textbox_Pass.Text = "";
                        }
                    }

                    else
                    {
                        MessageBox.Show("Please Verify reCaptcha!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }

                else
                {
                    MessageBox.Show("Enter both your username and password!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            catch { MessageBox.Show("Restart application after few minutes!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }

        private void textbox_user_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex regex = new Regex(@"^[a-z\d]+$");

            // Check if the pressed key matches the pattern
            if (!regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void textbox_Pass_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex regex = new Regex(@"^[a-z\d@#%!^&*]+$");

            // Check if the pressed key matches the pattern
            if (!regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                reCaptha uc = new reCaptha(pan, textbox_user.Text, textbox_Pass.Text, 1,null,null, label);
                getUC(uc);
            }
        }

        private void linkLabel_forget_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SQuestion sq = new SQuestion(pan, null, 1, label);
            getUC(sq);
        }

        private void textbox_user_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                textbox_Pass.Focus();
            }
        }
    }
}
