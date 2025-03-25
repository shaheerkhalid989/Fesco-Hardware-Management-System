using Fesco_Inventory.BL;
using Fesco_Inventory.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fesco_Inventory
{
    public partial class SQuestion : UserControl
    {
        userBL user;
        int usage;
        Panel pan;
        string checkUser = "";
        Label label;

        public SQuestion(Panel pan, userBL user, int usage, Label label)
        {
            InitializeComponent();
            this.pan = pan;
            this.user = user;
            this.usage = usage;
            this.label = label;
        }

        private void getUC(UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            pan.Controls.Clear();
            pan.Controls.Add(uc);
            uc.BringToFront();
        }

        private void SQuestion_Load(object sender, EventArgs e)
        {
            textbox_ans.PasswordChar = '*';
            btn_hide.Image = Properties.Resources.show;

            comboBox_SQ.Items.Clear();
            comboBox_SQ.Items.Add("What is your childhood nick name?");
            comboBox_SQ.Items.Add("What is your favorite movie name?");
            comboBox_SQ.Items.Add("What is your favorite memorable date?");
            comboBox_SQ.Items.Add("Which is your favorite football team?");
            comboBox_SQ.Items.Add("Who is your favorite cricket player?");
            comboBox_SQ.Items.Add("What was your favorite school teacher name?");

            if (usage == 1)
            {
                comboBox_SQ.SelectedText = "Choose Security Question!";
                btn_add.Text = "Verify";
            }

            else if (usage == 2)
            {
                btn_checkUser.Enabled = false;
                btn_checkUser.Visible = false;
                textbox_user.Text = user.Username;
                textbox_user.Enabled = false;
                comboBox_SQ.SelectedText = "Choose Security Question!";
                btn_back.Enabled = false;
                btn_back.Visible = false;
                linkLabel_otherWay.Enabled = false;
                linkLabel_otherWay.Visible = false;
            }
        }

        private void btn_checkUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textbox_user.Text))
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand(string.Concat("Select id from Employee where Username = '", textbox_user.Text, "'"), con);
                    object value = cmd.ExecuteScalar();

                    if (value != null)
                    {
                        user = userDL.getUserFromEmployeeTable((int)value);

                        if (user.Isdeleted == true)
                        {
                            MessageBox.Show("User Deleted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textbox_user.Text = "";
                        }

                        else
                        {
                            if (user.SecQuestion == 0)
                            {
                                MessageBox.Show("Security Question Not Set ...... Visit Admin further Assistance!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                textbox_user.Text = null;
                            }

                            else
                            {
                                checkUser = textbox_user.Text;
                                textbox_user.Enabled = false;
                                SqlCommand cmdd = new SqlCommand(string.Concat("Select [Value] from Lookup where Category = 'SECURITY_QUESTION' and id = ", user.SecQuestion.ToString()), con);
                                string sq = (string)cmdd.ExecuteScalar();
                                comboBox_SQ.SelectedItem = sq;
                                comboBox_SQ.Enabled = false;
                            }
                        }
                    }

                    else
                    {
                        MessageBox.Show("Username not found!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        textbox_user.Text = "";
                    }

                    //con.Close();
                }
                else
                {
                    MessageBox.Show("Enter Yor Username!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void btn_hide_Click(object sender, EventArgs e)
        {
            if (textbox_ans.PasswordChar == '\0')
            {
                textbox_ans.PasswordChar = '*';
                btn_hide.Image = Properties.Resources.show;
            }

            else if (textbox_ans.PasswordChar == '*')
            {
                textbox_ans.PasswordChar = '\0';
                btn_hide.Image = Properties.Resources.hide;
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            //var con = Configuration.getInstance().getConnection();
            try
            {
                if (btn_add.Text == "Verify")
                {
                    if (checkUser != "")
                    {
                        bool isItemInList = false;
                        comboBox_SQ.Enabled = true;
                        foreach (var item in comboBox_SQ.Items)
                        {
                            if (comboBox_SQ.SelectedItem != null)
                            {
                                if (item.ToString() == comboBox_SQ.SelectedItem.ToString())
                                {
                                    // The selected item is from the items in the ComboBox
                                    isItemInList = true;
                                    break;
                                }
                            }
                            continue;
                        }
                        comboBox_SQ.Enabled = false;

                        if (isItemInList)
                        {
                            if (!string.IsNullOrWhiteSpace(textbox_ans.Text))
                            {
                                if (textbox_ans.Text.ToString() == user.Sqa)
                                { 
                                    MessageBox.Show("Verification Successful!", "Verification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    EditUserPass nf = new EditUserPass(pan, user, 3, label);
                                    nf.ShowDialog();
                                }
                                else
                                    MessageBox.Show("Verification failed!", "Verification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                                MessageBox.Show("Enter valid answer!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                            MessageBox.Show("Select from given Questions!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                        MessageBox.Show("First enter your username and get it checked!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }

                else if (btn_add.Text == "Add")
                {
                    bool isItemInList = false;
                    //MessageBox.Show(item);
                    foreach (var item in comboBox_SQ.Items)
                    {
                        if (comboBox_SQ.SelectedItem != null)
                        {
                            if (item.ToString() == comboBox_SQ.SelectedItem.ToString())
                            {
                                // The selected item is from the items in the ComboBox
                                isItemInList = true;
                                break;
                            }
                        }
                        continue;
                    }

                    if (isItemInList)
                    {
                        if (!string.IsNullOrWhiteSpace(textbox_ans.Text))
                        {
                            var con = Configuration.getInstance().getConnection();
                            SqlCommand command = new SqlCommand(string.Concat("Update Employee set SQ = (select id from [Lookup] where Category = 'SECURITY_QUESTION' and [Value] = '",comboBox_SQ.SelectedItem.ToString(), "'), SQA = @SQA, FL = 0 where id = @id"), con);
                            command.Parameters.AddWithValue("@id", user.ID);
                            command.Parameters.AddWithValue("@SQA", textbox_ans.Text);
                            command.ExecuteNonQuery();
                            MessageBox.Show("Security Question Set for your profile!", "Information",MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            login uc = new login(pan, null, null, false, label);
                            getUC(uc);
                        }
                        else
                            MessageBox.Show("Enter valid answer!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Select from given Questions!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ee) { MessageBox.Show(ee.Message); }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            login uc = new login(pan, null, null, false, label);
            getUC(uc);
        }

        private void linkLabel_otherWay_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OtherWay nf = new OtherWay(pan, label);
            nf.ShowDialog();
        }
    }
}
