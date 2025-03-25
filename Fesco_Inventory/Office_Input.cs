using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fesco_Inventory
{
    public partial class Office_Input : Form
    {
        string officeCode;
        string officeName;
        Label label;

        bool valid_OfficeNum = false;

        public Office_Input(string officeCode, string officeName, Label label)
        {
            InitializeComponent();
            this.officeCode = officeCode;
            this.officeName = officeName;
            this.label = label;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (btn_add.Text == "Add")
            {
                if (!string.IsNullOrEmpty(textbox_officeCode.Text) && !string.IsNullOrEmpty(richTextBox_Description.Text) && textbox_officeCode.ForeColor != Color.Red)
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand command = new SqlCommand(string.Concat("select id from Office where Office_Code = ",textbox_officeCode.Text," or office_Add = '",richTextBox_Description.Text,"' and isDeleted != 1"), con);
                    object val = command.ExecuteScalar();

                    if(val == null)
                    {
                        SqlCommand cmd = new SqlCommand("Insert into Office (Office_Code,office_Add) values (@oc,@oa)", con);
                        cmd.Parameters.AddWithValue("@oc", int.Parse(textbox_officeCode.Text));
                        cmd.Parameters.AddWithValue("@oa", richTextBox_Description.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Office Details Added!", "Forbidden", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Entered Office Code or Name already exists!", "Forbidden", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }

                else
                {
                    MessageBox.Show("Empty Sections or invalid Data!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textbox_officeCode.Text = "";
                    richTextBox_Description.Text = "";
                }
            }

            else
            {
                if (!string.IsNullOrEmpty(textbox_officeCode.Text) && !string.IsNullOrEmpty(richTextBox_Description.Text) && textbox_officeCode.ForeColor != Color.Red)
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand command = new SqlCommand(string.Concat("select id from Office where Office_Code = ", textbox_officeCode.Text, " or office_Add = '", richTextBox_Description.Text, "' and isDeleted != 1 and id != (Select id from Office where office_Code = ",officeCode, " and office_Add = '",officeName,"' and isDeleted = 0)"), con);
                    object val = command.ExecuteScalar();

                    if (val == null)
                    {
                        SqlCommand cmd = new SqlCommand(string.Concat("Update Office Set Office_Code = @oc, office_Add = @oa where id = (Select id from Office where office_Code = ",officeCode," and office_Add = '",officeName,"' and isDeleted = 0)"), con);
                        cmd.Parameters.AddWithValue("@oc", int.Parse(textbox_officeCode.Text));
                        cmd.Parameters.AddWithValue("@oa", richTextBox_Description.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Office Details updated!", "Forbidden", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Entered Office Code or Name already exists!", "Forbidden", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }

                else
                {
                    MessageBox.Show("Empty Sections or invalid Data!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textbox_officeCode.Text = officeCode;
                    richTextBox_Description.Text = officeName;
                }
            }
        }

        private void Office_Input_Load(object sender, EventArgs e)
        {
            if(officeCode != null && officeName != null) { btn_add.Text = "Apply Changes"; textbox_officeCode.Text = officeCode; richTextBox_Description.Text = officeName; }
        }

        private void richTextBox_Description_Leave(object sender, EventArgs e)
        {
            RichTextBox textBox = (RichTextBox)sender;

            string originalText = textBox.Text;
            string capitalizedText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(originalText);

            textBox.Text = capitalizedText;
        }

        private bool checkValid_ItemNo()
        {
            int val = 0;
            if (textbox_officeCode.Text != "" && textbox_officeCode.Text != "XXXXXXXX")
                val = int.Parse(textbox_officeCode.Text);

            if (textbox_officeCode.Text.Length == 8 && val != 0)
                return true;
            return false;
        }

        private void textbox_officeCode_TextChanged(object sender, EventArgs e)
        {
            if ((valid_OfficeNum && textbox_officeCode.Text != "XXXXXXXX") || checkValid_ItemNo()) { textbox_officeCode.ForeColor = Color.FromArgb(125, 137, 149); }
        }

        private void textbox_officeCode_Validating(object sender, CancelEventArgs e)
        {
            if (textbox_officeCode.Text != "" && textbox_officeCode.Text != "XXXXXXXX")
            {
                int val = int.Parse(textbox_officeCode.Text);
                if (textbox_officeCode.Text.Length != 8 || val == 0)
                {
                    textbox_officeCode.ForeColor = Color.Red;
                    valid_OfficeNum = false;
                }

                else
                {
                    textbox_officeCode.ForeColor = Color.FromArgb(125, 137, 149);
                    valid_OfficeNum = true;
                }
            }
        }

        private void textbox_officeCode_Leave(object sender, EventArgs e)
        {
            if (textbox_officeCode.Text == "" || textbox_officeCode.Text == "XXXXXXXX") { textbox_officeCode.Text = ""; textbox_officeCode.ForeColor = Color.FromArgb(125, 137, 149); }

        }

        private void textbox_officeCode_Click(object sender, EventArgs e)
        {
            if (textbox_officeCode.Text == "")
            {
                textbox_officeCode.ForeColor = Color.LightGray;
                textbox_officeCode.Text = "XXXXXXXX";
            }
        }

        private void textbox_officeCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textbox_officeCode.Text == "XXXXXXXX")
            {
                textbox_officeCode.Text = ""; textbox_officeCode.ForeColor = Color.FromArgb(125, 137, 149);
            }

            Regex regex = new Regex(@"^[\d]+$");

            // Check if the pressed key matches the pattern
            if (!regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ignore the input
            }
        }
    }
}
