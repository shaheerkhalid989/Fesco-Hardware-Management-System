using Fesco_Inventory.BL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Fesco_Inventory
{
    public partial class EditUser : Form
    {
        userBL user;
        Label label;

        bool tb_user_isfirstClick = true;
        bool tb_pass_isfirstClick = true;

        bool valid_user = false;
        bool valid_pass = false;
        bool valid_cnic = false;
        bool valid_email;
        bool valid_phone = false;

        public EditUser(userBL user, Label label)
        {
            InitializeComponent();
            this.user = user;
            this.label = label;
        }

        bool checkCnic(string cnic_)
        {
            char[] letters = cnic_.ToCharArray();

            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            int countDash = 0;

            foreach (char letter in letters)
            {
                if (letter == '-')
                    countDash++;
                else if (countDash == 0 && char.IsDigit(letter))
                    count1++;
                else if (countDash == 1 && char.IsDigit(letter))
                    count2++;
                else if (countDash == 2 && char.IsDigit(letter))
                    count3++;
            }

            if (count1 == 5 && count2 == 7 && count3 == 1 && countDash == 2)
                return true;
            return false;
        }

        void UserData()
        {
            textbox_name.Text = user.Name;
            textbox_designation.Text = user.Designation;
            if (user.Email == null)
                textbox_email.Text = "Null";
            else
                textbox_email.Text = user.Email;
            textbox_pass.Text = user.Password;
            if (user.Phone == null)
                textbox_phone.Text = "Null";
            else
                textbox_phone.Text = user.Phone;
            textbox_user.Text = user.Username;
            if (user.Cnic == null)
                textbox_cnic.Text = "Null";
            else
                textbox_cnic.Text = user.Cnic;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand(string.Concat("Select [Value] from Lookup where Id = ", user.Gender, " and Category = 'Gender'"), con);
            string gender = (string)cmd.ExecuteScalar();
            comboBox_gender.SelectedItem = gender;

            SqlCommand cmdd = new SqlCommand(string.Concat("Select [Value] from Lookup where Id = ", user.Role, " and Category = 'ROLE'"), con);
            string role = (string)cmdd.ExecuteScalar();
            comboBox_role.SelectedItem = role;
        }

        private bool validCnicCheck()
        {
            Regex regex = new Regex(@"^\d+-+\d+-+\d+$");
            if (regex.IsMatch(textbox_cnic.Text) && textbox_cnic.Text.Length == 15 && checkCnic(textbox_cnic.Text)) return true;
            return false;
        }

        private bool validemailcheck()
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            if (regex.IsMatch(textbox_email.Text) && textbox_email.Text != "abc123@example.xyz") return true;
            return false;
        }

        private bool validPhoneCheck()
        {
            Regex regex = new Regex(@"^03+\d+$");
            if (regex.IsMatch(textbox_phone.Text) && textbox_phone.Text.Length == 11) return true;
            return false;
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

        private void EditUser_Load(object sender, EventArgs e)
        {
            textbox_pass.PasswordChar = '*';
            btn_hide.Image = Properties.Resources.show;
            UserData();
            if (comboBox_role.Text == "Supervisor")
            {
                checkBox_isAdmin.Enabled = true;
                checkBox_isAdmin.Visible = true;
                if(user.Isadmin)
                    checkBox_isAdmin.Checked = true;
            }
            else
            {
                checkBox_isAdmin.Enabled = false;
                checkBox_isAdmin.Visible = false;
            }
        }

        private void textbox_cnic_Click(object sender, EventArgs e)
        {
            if (textbox_cnic.Text == "" || textbox_cnic.Text == "Null")
            {
                textbox_cnic.ForeColor = Color.LightGray;
                textbox_cnic.Text = "XXXXX-XXXXXXX-X";
            }
        }

        private void textbox_cnic_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textbox_cnic.Text == "XXXXX-XXXXXXX-X")
            {
                textbox_cnic.Text = "";
                textbox_cnic.ForeColor = Color.FromArgb(125, 137, 149);
            }

            Regex regex = new Regex(@"^[\d-]+$");

            // Check if the pressed key matches the pattern
            if (!regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void textbox_cnic_Validating(object sender, CancelEventArgs e)
        {
            if (textbox_cnic.Text != "" && textbox_cnic.Text != "Null" && textbox_cnic.Text != "XXXXX-XXXXXXX-X")
            {
                Regex regex = new Regex(@"^\d+-+\d+-+\d+$");

                // Check if the textbox text matches the pattern
                if (!regex.IsMatch(textbox_cnic.Text) || textbox_cnic.Text.Length != 15 || !checkCnic(textbox_cnic.Text))
                {
                    textbox_cnic.ForeColor = Color.Red;
                    valid_cnic = false;
                }

                else
                {
                    textbox_cnic.ForeColor = Color.FromArgb(125, 137, 149);
                    valid_cnic = true;
                }
            }
        }

        private void textbox_phone_Click(object sender, EventArgs e)
        {
            if (textbox_phone.Text == "" || textbox_phone.Text == "Null")
            {
                textbox_phone.ForeColor = Color.LightGray;
                textbox_phone.Text = "03XXXXXXXXX";
            }
        }

        private void textbox_phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textbox_phone.Text == "03XXXXXXXXX")
            {
                textbox_phone.Text = ""; textbox_cnic.ForeColor = Color.FromArgb(125, 137, 149);
            }

            Regex regex = new Regex(@"^[\d]+$");

            // Check if the pressed key matches the pattern
            if (!regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void textbox_phone_Validating(object sender, CancelEventArgs e)
        {
            if (textbox_phone.Text != "" && textbox_phone.Text != "Null" && textbox_phone.Text != "03XXXXXXXXX")
            {
                Regex regex = new Regex(@"^03+\d+$");

                if (!regex.IsMatch(textbox_phone.Text) || textbox_phone.Text.Length != 11)
                {
                    textbox_phone.ForeColor = Color.Red;
                    valid_phone = false;
                }

                else
                {
                    textbox_phone.ForeColor = Color.FromArgb(125, 137, 149);
                    valid_phone = true;
                }
            }
        }

        private void textbox_email_Click(object sender, EventArgs e)
        {
            if (textbox_email.Text == "" || textbox_email.Text == "Null")
            {
                textbox_email.Text = "abc123@example.xyz"; textbox_email.ForeColor = Color.LightGray;
            }
        }

        private void textbox_email_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textbox_email.Text == "abc123@example.xyz")
            {
                textbox_email.Text = "";
                textbox_cnic.ForeColor = Color.FromArgb(125, 137, 149);
            }

            Regex regex = new Regex(@"^[\da-z@.]+$");

            // Check if the pressed key matches the pattern
            if (!regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void textbox_email_Validating(object sender, CancelEventArgs e)
        {
            if (textbox_email.Text != "" && textbox_email.Text != "Null" && textbox_email.Text != "abc123@example.xyz")
            {

                Regex regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

                // Check if the textbox text matches the pattern
                if (!regex.IsMatch(textbox_email.Text))
                {
                    textbox_email.ForeColor = Color.Red;
                    valid_email = false;
                }

                else
                {
                    textbox_email.ForeColor = Color.FromArgb(125, 137, 149);
                    valid_email = true;
                }
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

                // Check if the text contains any special characters
                //MessageBox.Show(regex.IsMatch(textbox_pass.Text).ToString());

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

        private void textbox_name_Leave(object sender, EventArgs e)
        {
            Guna2TextBox textBox = (Guna2TextBox)sender;

            string originalText = textBox.Text;
            string capitalizedText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(originalText);

            textBox.Text = capitalizedText;
        }

        private void textbox_user_TextChanged(object sender, EventArgs e)
        {
            if (valid_user || validUserCheck()) { textbox_user.ForeColor = Color.FromArgb(125, 137, 149); }
        }

        private void textbox_phone_TextChanged(object sender, EventArgs e)
        {
            if ((valid_phone && textbox_phone.Text != "03XXXXXXXXX") || validPhoneCheck()) { textbox_phone.ForeColor = Color.FromArgb(125, 137, 149); }
        }

        private void textbox_email_TextChanged(object sender, EventArgs e)
        {
            if ((valid_email && textbox_email.Text != "abc123@example.xyz") || validemailcheck()) { textbox_email.ForeColor = Color.FromArgb(125, 137, 149); }
        }

        private void textbox_pass_TextChanged(object sender, EventArgs e)
        {
            if (valid_pass || validPassCheck()) { textbox_pass.ForeColor = Color.FromArgb(125, 137, 149); }
        }

        private void textbox_cnic_TextChanged(object sender, EventArgs e)
        {
            if ((valid_cnic && textbox_cnic.Text != "XXXXX-XXXXXXX-X") || validCnicCheck()) { textbox_cnic.ForeColor = Color.FromArgb(125, 137, 149); }
        }

        private void textbox_cnic_Leave(object sender, EventArgs e)
        {
            if (textbox_cnic.Text == "" || textbox_cnic.Text == "XXXXX-XXXXXXX-X") { textbox_cnic.Text = "Null"; textbox_cnic.ForeColor = Color.FromArgb(125, 137, 149); }
        }

        private void textbox_email_Leave(object sender, EventArgs e)
        {
            if (textbox_email.Text == "abc123@example.xyz" || textbox_email.Text == "") { textbox_email.Text = "Null"; textbox_cnic.ForeColor = Color.FromArgb(125, 137, 149); }
            }

        private void textbox_phone_Leave(object sender, EventArgs e)
        {
            if (textbox_phone.Text == "03XXXXXXXXX" || textbox_phone.Text == "") { textbox_phone.Text = "Null"; textbox_cnic.ForeColor = Color.FromArgb(125, 137, 149); }
        }

        private void btn_hide_Click(object sender, EventArgs e)
        {
            if (textbox_pass.PasswordChar == '\0')
            {
                textbox_pass.PasswordChar = '*';
                btn_hide.Image = Properties.Resources.show;
            }

            else if (textbox_pass.PasswordChar == '*')
            {
                textbox_pass.PasswordChar = '\0';
                btn_hide.Image = Properties.Resources.hide;
            }
        }

        private void btn_applyChanges_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textbox_name.Text) && !string.IsNullOrWhiteSpace(textbox_designation.Text) && !string.IsNullOrWhiteSpace(textbox_pass.Text) && !string.IsNullOrWhiteSpace(textbox_user.Text) && !string.IsNullOrWhiteSpace(textbox_email.Text))
            {
                if (textbox_cnic.ForeColor != Color.Red && textbox_email.ForeColor != Color.Red && textbox_pass.ForeColor != Color.Red && textbox_phone.ForeColor != Color.Red && textbox_user.ForeColor != Color.Red)
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand(string.Concat("Select id from Employee where Username = '", textbox_user.Text, "' and id != ", user.ID), con);
                    object value = cmd.ExecuteScalar();

                    SqlCommand cmdd = new SqlCommand(string.Concat("Select id from Employee where email = '", textbox_email.Text, "' and id != ", user.ID), con);
                    object val_ = cmdd.ExecuteScalar();

                    if (value == null)
                    {
                        if (val_ == null)
                        {
                            SqlCommand c = new SqlCommand(string.Concat("Select id from Previous_Password where pp = '", textbox_pass.Text, "' and id = ", user.ID), con);
                            object val = c.ExecuteScalar();

                            if (val == null || textbox_pass.Text == user.Password)
                            {
                                SqlCommand comd = new SqlCommand(string.Concat("Select id from [Lookup] where Category = 'GENDER' and [Value] = '", comboBox_gender.SelectedItem, "'"), con);
                                int gender = (int)comd.ExecuteScalar();

                                SqlCommand commd = new SqlCommand(string.Concat("Select id from [Lookup] where Category = 'ROLE' and [Value] = '", comboBox_role.SelectedItem, "'"), con);
                                int role = (int)commd.ExecuteScalar();

                                SqlCommand cmnnd = new SqlCommand("Update Employee set FL = @FL, EmployeeName = @EmployeeName, Designation = @Designation, Gender = @Gender, Username = @Username, UserPassword = @UserPassword, UserRole = @UserRole, email = @email, Phone = @Phone, Cnic = @Cnic, isAdmin = @isAdmin WHERE Id = @Id", con);
                                cmnnd.Parameters.AddWithValue("@Id", user.ID);
                                cmnnd.Parameters.AddWithValue("@EmployeeName", textbox_name.Text);
                                cmnnd.Parameters.AddWithValue("@Designation", textbox_designation.Text);
                                cmnnd.Parameters.AddWithValue("@Gender", gender);
                                cmnnd.Parameters.AddWithValue("@UserRole", role);
                                cmnnd.Parameters.AddWithValue("@FL", 1);
                                if (textbox_phone.Text == "Null")
                                    cmnnd.Parameters.AddWithValue("@Phone", DBNull.Value);
                                else
                                    cmnnd.Parameters.AddWithValue("@Phone", textbox_phone.Text);
                                if (textbox_email.Text == "Null")
                                    cmnnd.Parameters.AddWithValue("@email", DBNull.Value);
                                else
                                    cmnnd.Parameters.AddWithValue("@email", textbox_email.Text);
                                cmnnd.Parameters.AddWithValue("@Username", textbox_user.Text);
                                cmnnd.Parameters.AddWithValue("@UserPassword", textbox_pass.Text);
                                if (textbox_cnic.Text == "Null")
                                    cmnnd.Parameters.AddWithValue("@Cnic", DBNull.Value);
                                else
                                    cmnnd.Parameters.AddWithValue("@Cnic", textbox_cnic.Text);
                                if(checkBox_isAdmin.Checked)
                                    cmnnd.Parameters.AddWithValue("@isAdmin", 1);
                                else
                                    cmnnd.Parameters.AddWithValue("@isAdmin", 0);
                                cmnnd.ExecuteNonQuery();

                                SqlCommand pp = new SqlCommand(string.Concat("Insert into Previous_Password (Id,pp) values (", user.ID, ", '", textbox_pass.Text, "')"), con);
                                pp.ExecuteNonQuery();

                                MessageBox.Show("User details updated Successfully!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }

                            else
                            {
                                MessageBox.Show("Password Previously used!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                textbox_pass.Text = "";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Email already exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textbox_email.Text = user.Email;
                        }
                    }

                    else
                    {
                        MessageBox.Show("Username already taken!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textbox_user.Text = user.Username;
                    }
                }
                else
                    MessageBox.Show("Some of the data is invalid ..... Please enter valid data!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

            else
            {
                MessageBox.Show("Required Sections should neither be empty nor contain white spaces!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void comboBox_role_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox_role.Text == "Supervisor")
            {
                checkBox_isAdmin.Enabled = true;
                checkBox_isAdmin.Visible = true;
            }
            else
            {
                checkBox_isAdmin.Enabled = false;
                checkBox_isAdmin.Visible = false;
            }
        }
    }
}
