using Fesco_Inventory.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fesco_Inventory
{
    public partial class EditUserPass : Form
    {
        userBL user;
        int usage;
        Panel pan;
        Label label;

        bool tb_user_isfirstClick = true;
        bool tb_pass_isfirstClick = true;

        bool valid_user = false;
        bool valid_pass = false;

        public EditUserPass(Panel pan, userBL user, int usage, Label label)
        {
            InitializeComponent();
            this.user = user;
            this.usage = usage;
            this.pan = pan;
            this.label = label;
        }

        private void getUC(UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            pan.Controls.Clear();
            pan.Controls.Add(uc);
            uc.BringToFront();
        }

        private bool validUserCheck()
        {
            if (textbox_user.Text.Length < 8) return false;
            return true;
        }

        private bool validPassCheck()
        {
            Regex regex = new Regex(@"[~`!@#$%^&*()+=\[\]{};:'""\\|,.<>/?]");
            if (!regex.IsMatch(textbox_pass.Text) || textbox_pass.Text.Length < 8) { return false; }
            return true;
        }

        private void EditUserPass_Load(object sender, EventArgs e)
        {
            if (usage == 1)
            {
                textbox_user.Text = user.Username;

                textbox_pass.PasswordChar = '*';
                textbox_reenterPass.PasswordChar = '*';
                btn_hide.Image = Properties.Resources.show;
            }

            if (usage == 2 || usage == 3 || usage == 4)
            {
                textbox_user.Text = user.Username;
                textbox_user.Enabled = false;

                textbox_pass.PasswordChar = '*';
                textbox_reenterPass.PasswordChar = '*';
                btn_hide.Image = Properties.Resources.show;
            }
        }

        private void btn_applyChanges_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(textbox_pass.Text) && !string.IsNullOrWhiteSpace(textbox_user.Text) && !string.IsNullOrWhiteSpace(textbox_reenterPass.Text) && textbox_user.ForeColor != Color.Red && textbox_pass.ForeColor != Color.Red)
            {
                if (textbox_pass.Text == textbox_reenterPass.Text)
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand(string.Concat("Select id from Employee where Username = '", textbox_user.Text, "' and id != ", user.ID), con);
                    object value = cmd.ExecuteScalar();

                    if (value == null)
                    {
                        SqlCommand c = new SqlCommand(string.Concat("Select id from Previous_Password where pp = '", textbox_pass.Text, "' and id = ", user.ID), con);
                        object val = c.ExecuteScalar();

                        if (val == null)
                        {

                            SqlCommand cmnnd = new SqlCommand("Update Employee set Username = @Username, UserPassword = @UserPassword WHERE Id = @Id", con);
                            cmnnd.Parameters.AddWithValue("@Id", user.ID);
                            cmnnd.Parameters.AddWithValue("@Username", textbox_user.Text);
                            cmnnd.Parameters.AddWithValue("@UserPassword", textbox_pass.Text);
                            cmnnd.ExecuteNonQuery();
                            user.Username = textbox_user.Text;
                            user.Password = textbox_pass.Text;


                            SqlCommand pp = new SqlCommand(string.Concat("Insert into Previous_Password (Id,pp) values (", user.ID, ", '", textbox_pass.Text, "')"), con);
                            pp.ExecuteNonQuery();

                            MessageBox.Show("Username and Password updated!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            if (usage == 2)
                            {
                                SQuestion uc = new SQuestion(pan, user, 2, label);
                                getUC(uc);
                            }

                            else if (usage == 3)
                            {
                                login uc = new login(pan, null, null, false, label);
                                getUC(uc);
                            }

                            else if(usage == 4)
                            {
                                SqlCommand sbc = new SqlCommand(string.Concat("Update Employee set FL = 0 WHERE Id = ",user.ID), con);
                                sbc.ExecuteNonQuery();
                                login uc = new login(pan, null, null, false, label);
                                getUC(uc);
                            }
                            this.Close();
                        }

                        else
                        {
                            MessageBox.Show("Password Previously used!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textbox_pass.Text = "";
                            textbox_reenterPass.Text = "";
                        }
                    }


                    else
                    {
                        MessageBox.Show("Username already taken!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textbox_user.Text = user.Username;
                    }
                }

                else
                    MessageBox.Show("Both Passwords do not match!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                MessageBox.Show("Neither left empty sections nor enter invalid data!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textbox_user_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex regex = new Regex(@"^[\da-z]+$");

            // Check if the pressed key matches the pattern
            if (!regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void textbox_user_Click(object sender, EventArgs e)
        {
            if (tb_user_isfirstClick)
            {
                MessageBox.Show("Username should contain only small alphabets and numerical digits.", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tb_user_isfirstClick = false;
            }
        }

        private void textbox_pass_Click(object sender, EventArgs e)
        {
            if (tb_pass_isfirstClick)
            {
                MessageBox.Show("Password must have atleast 8 characters and atleast one special characters!", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tb_pass_isfirstClick = false;
            }
        }

        private void textbox_pass_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex regex = new Regex(@"^[\da-z~`!@#$%^&*()+=\[\]{};:'""\\|,.<>/?]+$");

            // Check if the pressed key matches the pattern
            if (!regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void textbox_pass_Validating(object sender, CancelEventArgs e)
        {
            if (textbox_pass.Text != "")
            {
                Regex regex = new Regex(@"[~`!@#$%^&*()+=\[\]{};:'""\\|,.<>/?]");

                if (!regex.IsMatch(textbox_pass.Text) || textbox_pass.Text.Length < 8)
                {
                    textbox_pass.ForeColor = Color.Red;
                    valid_pass = false;
                }

                else
                {
                    textbox_pass.ForeColor = Color.FromArgb(125, 137, 149);
                    valid_pass = true;
                }
            }
        }

        private void textbox_user_Validating(object sender, CancelEventArgs e)
        {
            if (textbox_user.Text != "")
            {
                if (textbox_user.Text.Length < 8)
                {
                    textbox_user.ForeColor = Color.Red;
                    valid_user = false;
                }

                else
                {
                    valid_user = true;
                    textbox_user.ForeColor = Color.FromArgb(125, 137, 149);
                }
            }
        }

        private void btn_hide_Click(object sender, EventArgs e)
        {
            if (textbox_pass.PasswordChar == '\0')
            {
                textbox_pass.PasswordChar = '*';
                textbox_reenterPass.PasswordChar = '*';
                btn_hide.Image = Properties.Resources.show;
            }

            else if (textbox_pass.PasswordChar == '*')
            {
                textbox_pass.PasswordChar = '\0';
                textbox_reenterPass.PasswordChar = '\0';
                btn_hide.Image = Properties.Resources.hide;
            }
        }

        private void textbox_pass_TextChanged(object sender, EventArgs e)
        {
            if (valid_pass || validPassCheck()) { textbox_pass.ForeColor = Color.FromArgb(125, 137, 149); }
        }

        private void textbox_user_TextChanged(object sender, EventArgs e)
        {
            if (valid_user || validUserCheck()) { textbox_user.ForeColor = Color.FromArgb(125, 137, 149); }
        }
    }
}
