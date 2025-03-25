using Fesco_Inventory.BL;
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
    public partial class AddItem : UserControl
    {
        Panel pan;
        userBL user;
        InventoryBL item_1;
        Label label;
        int usage;

        bool validItemNo = false;

        public AddItem(Panel pan, userBL user, InventoryBL item_1, int usage, Label label)
        {
            InitializeComponent();
            this.pan = pan;
            this.user = user;
            this.item_1 = item_1;
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

        private bool checkValid_ItemNo()
        {
            int val = 0;
            if(textbox_itemNo.Text != "" && textbox_itemNo.Text != "XXXXXXXX")
               val = int.Parse(textbox_itemNo.Text);

            if (textbox_itemNo.Text.Length == 8 && val != 0)
                return true;
            return false;
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            InventryItem_AdminSup uc = new InventryItem_AdminSup(pan, user, label);
            getUC(uc);
        }

        private void textbox_itemNo_Validating(object sender, CancelEventArgs e)
        {
            if (textbox_itemNo.Text != "" && textbox_itemNo.Text != "XXXXXXXX")
            {
                int  val = int.Parse(textbox_itemNo.Text);
                if (textbox_itemNo.Text.Length != 8 || val == 0)
                {
                    textbox_itemNo.ForeColor = Color.Red;
                    validItemNo = false;
                }

                else
                {
                    textbox_itemNo.ForeColor = Color.FromArgb(125, 137, 149);
                    validItemNo = true;
                }
            }
        }

        private void textbox_itemNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(textbox_itemNo.Text == "XXXXXXXX")
            {
                textbox_itemNo.Text = ""; textbox_itemNo.ForeColor = Color.FromArgb(125, 137, 149);
            }

            Regex regex = new Regex(@"^[\d]+$");

            // Check if the pressed key matches the pattern
            if (!regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void textbox_itemNo_TextChanged(object sender, EventArgs e)
        {
            if((validItemNo && textbox_itemNo.Text != "XXXXXXXX") || checkValid_ItemNo()) { textbox_itemNo.ForeColor = Color.FromArgb(125, 137, 149); }
        }

        private void textbox_itemNo_Leave(object sender, EventArgs e)
        {
            if (textbox_itemNo.Text == "" || textbox_itemNo.Text == "XXXXXXXX") { textbox_itemNo.Text = ""; textbox_itemNo.ForeColor = Color.FromArgb(125, 137, 149); }
        }

        private void textbox_itemNo_Click(object sender, EventArgs e)
        {
            if(textbox_itemNo.Text == "")
            {
                textbox_itemNo.ForeColor = Color.LightGray;
                textbox_itemNo.Text = "XXXXXXXX";
            }
        }

        private void textbox_Quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex regex = new Regex(@"^[\d]+$");

            // Check if the pressed key matches the pattern
            if (!regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textbox_itemNo.Text) && !string.IsNullOrEmpty(textbox_desc.Text) && !string.IsNullOrEmpty(textbox_Quantity.Text) && comboBox_unit.SelectedItem != null && textbox_itemNo.ForeColor != Color.Red)
            {
                if (usage == 1)
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand co = new SqlCommand(string.Concat("Select id from Inventory where ItemNo = ", textbox_itemNo.Text, " or Item_Description = '", textbox_desc.Text, "'"), con);
                    object val = co.ExecuteScalar();

                    if (val == null)
                    {
                        int item_no = int.Parse(textbox_itemNo.Text);
                        int quantity = int.Parse(textbox_Quantity.Text);
                        SqlCommand commd = new SqlCommand(string.Concat("Select id from [Lookup] where Category = 'UNIT_OF_MEASURE' and [Value] = '", comboBox_unit.SelectedItem, "'"), con);
                        int unit = (int)commd.ExecuteScalar();
                        SqlCommand cmd = new SqlCommand("Insert into Inventory (ItemNo,Item_Description,Quantity,Unit_of_Measure,Added_On,Added_By,Out_of_Stock) values (@ItemNo,@Item_Description,@Quantity,@Unit_of_Measure,@Added_On,@Added_By,@Out_of_Stock)", con);
                        cmd.Parameters.AddWithValue("@ItemNo", item_no);
                        cmd.Parameters.AddWithValue("@Item_Description", textbox_desc.Text);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@Unit_of_Measure", unit);
                        cmd.Parameters.AddWithValue("@Added_On", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Added_By", user.ID);
                        if (quantity < 1)
                            cmd.Parameters.AddWithValue("@Out_of_Stock", 1);
                        else
                            cmd.Parameters.AddWithValue("@Out_of_Stock", 0);
                        cmd.ExecuteNonQuery();

                        SqlCommand cd = new SqlCommand(string.Concat("Insert into Remaining (Id,rQuan,Date_rQuan) values ((select max(Id) from Inventory),",quantity,",@date)"), con);
                        cd.Parameters.AddWithValue("@date", DateTime.Now);
                        cd.ExecuteNonQuery();

                        MessageBox.Show("Item Added!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textbox_itemNo.Text = null;
                        textbox_Quantity.Text = null;
                        textbox_desc.Text = null;
                        comboBox_unit.SelectedItem = null;
                    }

                    else
                        MessageBox.Show("Item Already Added!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //con.Close();
                }

                else if (usage == 2)
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand co = new SqlCommand(string.Concat("Select id from Inventory where (ItemNo = ", textbox_itemNo.Text, " or Item_Description = '", textbox_desc.Text, "') and id != ",item_1.Id), con);
                    object val = co.ExecuteScalar();

                    if (val == null)
                    {
                        int item_no = int.Parse(textbox_itemNo.Text);
                        int quantity = int.Parse(textbox_Quantity.Text);
                        SqlCommand commd = new SqlCommand(string.Concat("Select id from [Lookup] where Category = 'UNIT_OF_MEASURE' and [Value] = '", comboBox_unit.SelectedItem, "'"), con);
                        int unit = (int)commd.ExecuteScalar();

                        SqlCommand cmd = new SqlCommand(string.Concat("Update Inventory Set ItemNo = @ItemNo, Item_Description = @Item_Description, Quantity = @Quantity, Unit_of_Measure = @Unit_of_Measure, Out_of_Stock = @Out_of_Stock where id = ", item_1.Id.ToString()), con);
                        cmd.Parameters.AddWithValue("@ItemNo", item_no);
                        cmd.Parameters.AddWithValue("@Item_Description", textbox_desc.Text);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@Unit_of_Measure", unit);
                        if (quantity < 1)
                            cmd.Parameters.AddWithValue("@Out_of_Stock", 1);
                        else
                            cmd.Parameters.AddWithValue("@Out_of_Stock", 0);
                        cmd.ExecuteNonQuery();

                        SqlCommand cd = new SqlCommand(string.Concat("Insert into Remaining (Id,rQuan,Date_rQuan) values (", item_1.Id, ",", quantity, ",@date)"), con);
                        cd.Parameters.AddWithValue("@date", DateTime.Now);
                        cd.ExecuteNonQuery();

                        MessageBox.Show("Item Updated!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textbox_itemNo.Text = null;
                        textbox_Quantity.Text = null;
                        textbox_desc.Text = null;
                        comboBox_unit.SelectedItem = null;

                        InventryItem_AdminSup uc = new InventryItem_AdminSup(pan, user, label);
                        getUC(uc);
                    }

                    else
                    {
                        MessageBox.Show("Item# OR Description conflict found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textbox_itemNo.Text = item_1.ItemNo.ToString();
                        textbox_desc.Text = item_1.ItemDesc;
                        textbox_Quantity.Text = item_1.Quantity.ToString();
                        SqlCommand commd = new SqlCommand(string.Concat("Select [Value] from [Lookup] where Category = 'UNIT_OF_MEASURE' and id = ", item_1.Unit_of_Measure.ToString()), con);
                        comboBox_unit.Text = (string)commd.ExecuteScalar();
                        btn_add.Text = "Apply Changes";
                    }
                }
            }

            else
            {
                MessageBox.Show("Empty Sections or invalid Data!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textbox_desc_Leave(object sender, EventArgs e)
        {
            //Guna2TextBox textBox = (Guna2TextBox)sender;

            //string originalText = textBox.Text;
            //string capitalizedText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(originalText);

            //textBox.Text = capitalizedText;
        }

        private void AddItem_Load(object sender, EventArgs e)
        {
            if(usage == 2)
            {
                textbox_itemNo.Text = item_1.ItemNo.ToString();
                textbox_desc.Text = item_1.ItemDesc;
                textbox_Quantity.Text= item_1.Quantity.ToString();
                var con = Configuration.getInstance().getConnection();
                SqlCommand commd = new SqlCommand(string.Concat("Select [Value] from [Lookup] where Category = 'UNIT_OF_MEASURE' and id = ", item_1.Unit_of_Measure.ToString()), con);
                comboBox_unit.Text = (string)commd.ExecuteScalar();
                btn_add.Text = "Apply Changes";
            }
        }
    }
}
