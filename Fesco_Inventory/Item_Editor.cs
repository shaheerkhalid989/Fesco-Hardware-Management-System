using Fesco_Inventory.BL;
using Fesco_Inventory.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fesco_Inventory
{
    public partial class Item_Editor : Form
    {
        int id;
        int usage;
        userBL user;
        Label label;

        InventoryBL item;
        int Item_quantity;
        bool quantity_firstClick = true;
        public Item_Editor(int id, int usage, userBL user, Label label)
        {
            InitializeComponent();
            this.id = id;
            this.usage = usage;
            this.user = user;
            this.label = label;
        }

        public void getData()
        {
            if (usage == 4)
            {
                var conn = Configuration.getInstance().getConnection();
                SqlCommand comd = new SqlCommand(string.Concat("select Id from Inventory where Id = (select ItemId from Discarded where Id = ", id,")"), conn);
                item = InventoryDL.getItemFromInventoryTable((int)comd.ExecuteScalar());

                SqlCommand cmd = new SqlCommand(string.Concat("select Quantity from Inventory where Id = (select ItemId from Discarded where Id = ", id, ")"), conn);
                Item_quantity = (int)cmd.ExecuteScalar();
            }

            else
                item = InventoryDL.getItemFromInventoryTable(this.id);

            textbox_itemNo.Text = item.ItemNo.ToString();
            richTextBox_Description.Text = item.ItemDesc;
            var con = Configuration.getInstance().getConnection();
            SqlCommand commd = new SqlCommand(string.Concat("Select [Value] from [Lookup] where Category = 'UNIT_OF_MEASURE' and id = ", item.Unit_of_Measure.ToString()), con);
            textbox_unit.Text = (string)commd.ExecuteScalar();

            if (usage == 3)
                btn_add.Text = "Discard";

            else if (usage == 4)
                btn_add.Text = "Apply Changes";
        }

        private void AddStock_Load(object sender, EventArgs e)
        {
            getData();
        }

        private void textbox_quantity_KeyPress(object sender, KeyPressEventArgs e)
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
            int quantity = 0;
            if(textbox_quantity.Text != "")
                quantity = Convert.ToInt32(textbox_quantity.Text);
            
            if(!string.IsNullOrWhiteSpace(textbox_quantity.Text) && quantity != 0)
            {
                if (usage == 1)
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand commd = new SqlCommand(string.Concat("update Inventory set Quantity = @Quantity, Out_of_Stock = @Out_of_Stock where id = ", item.Id.ToString()), con);
                    commd.Parameters.AddWithValue("@Quantity", quantity);
                    commd.Parameters.AddWithValue("@Out_of_Stock", 0);
                    commd.ExecuteNonQuery();

                    SqlCommand cd = new SqlCommand(string.Concat("Insert into AddedStock (Id,added_stock,Date_added) values (", item.Id, ",", quantity, ",@date)"), con);
                    cd.Parameters.AddWithValue("@date", DateTime.Now);
                    cd.ExecuteNonQuery();

                    MessageBox.Show("Stock Added!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }

                else if (usage == 2)
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand commd = new SqlCommand(string.Concat("update Inventory set Quantity = @Quantity where id = ", item.Id.ToString()), con);
                    commd.Parameters.AddWithValue("@Quantity", quantity + item.Quantity);
                    commd.ExecuteNonQuery();

                    SqlCommand cd = new SqlCommand(string.Concat("Insert into AddedStock (Id,added_stock,Date_added) values (",item.Id,",", quantity, ",@date)"), con);
                    cd.Parameters.AddWithValue("@date", DateTime.Now);
                    cd.ExecuteNonQuery();

                    MessageBox.Show("Stock Added!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }

                else if (usage == 3)
                {


                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand(string.Concat("Select id from Discarded where id = ",item.Id),con);
                    object val = cmd.ExecuteScalar();
                    if (quantity <= item.Quantity)
                    {
                        SqlCommand commd = new SqlCommand(string.Concat("Insert into Discarded (ItemId, qDisc, disc_On, disc_By) values (@Id, @Discarded_Quantity,'",DateTime.Now,"',",user.ID,")"), con);
                        commd.Parameters.AddWithValue("@Id", item.Id);
                        commd.Parameters.AddWithValue("@Discarded_Quantity", quantity);
                        commd.ExecuteNonQuery();

                        SqlCommand co = new SqlCommand(string.Concat("update Inventory set Quantity = @Quantity, Out_of_Stock = @Out_of_Stock where id = ", item.Id.ToString()), con);
                        co.Parameters.AddWithValue("@Quantity", item.Quantity - quantity);
                        if (item.Quantity == quantity)
                            co.Parameters.AddWithValue("@Out_of_Stock", 1);
                        else
                            co.Parameters.AddWithValue("@Out_of_Stock", 0);
                        co.ExecuteNonQuery();

                        SqlCommand cd = new SqlCommand(string.Concat("Insert into Remaining (Id,rQuan,Date_rQuan) values (", item.Id, ",", item.Quantity - (quantity), ",@date)"), con);
                        cd.Parameters.AddWithValue("@date", DateTime.Now);
                        cd.ExecuteNonQuery();

                        MessageBox.Show("Item Quantity Discarded! \n\nYou can update this record within 3 days from Bucket Section!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Quantity isn't available!", "Forbiden", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        textbox_quantity.Text = "";
                    }
                }

                else if(usage == 4)
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmdd = new SqlCommand(string.Concat("Select qDisc from Discarded where id = ", id), con);
                    int dq = (int)cmdd.ExecuteScalar();

                    if(quantity <= dq)
                    {
                        SqlCommand comd = new SqlCommand(string.Concat("update Discarded set qDisc = @Discarded_Quantity, Updated = 1 where id = ", id), con);
                        comd.Parameters.AddWithValue("@Discarded_Quantity", quantity);
                        comd.ExecuteNonQuery();

                        SqlCommand co = new SqlCommand(string.Concat("update Inventory set Quantity = @Quantity, Out_of_Stock = @Out_of_Stock where id = (select ItemId from Discarded where id = ",id,")"), con);
                        co.Parameters.AddWithValue("@Quantity", item.Quantity + (dq - quantity));
                        if(item.Quantity + (dq - quantity) == 0)
                            co.Parameters.AddWithValue("@Out_of_Stock", 1);
                        else
                            co.Parameters.AddWithValue("@Out_of_Stock", 0);
                        co.ExecuteNonQuery();

                        SqlCommand cd = new SqlCommand(string.Concat("Insert into Remaining (Id,rQuan,Date_rQuan) values ((select ItemId from Discarded where id = ", id, "),", item.Quantity + (dq - quantity),", @date)"), con);
                        cd.Parameters.AddWithValue("@date", DateTime.Now);
                        cd.ExecuteNonQuery();

                        MessageBox.Show("Changes Applied!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }

                    else
                    {
                        if (item.Quantity - (quantity - dq) >= 0)
                        {
                            SqlCommand comd = new SqlCommand(string.Concat("update Discarded set qDisc = @Discarded_Quantity, Updated = 1 where id = ", id), con);
                            comd.Parameters.AddWithValue("@Discarded_Quantity", quantity);
                            comd.ExecuteNonQuery();

                            SqlCommand co = new SqlCommand(string.Concat("update Inventory set Quantity = @Quantity, Out_of_Stock = @Out_of_Stock where id = (select ItemId from Discarded where id = ", id, ")"), con);
                            co.Parameters.AddWithValue("@Quantity", item.Quantity - (quantity - dq));
                            if (item.Quantity + (dq - quantity) == 0)
                                co.Parameters.AddWithValue("@Out_of_Stock", 1);
                            else
                                co.Parameters.AddWithValue("@Out_of_Stock", 0);
                            co.ExecuteNonQuery();

                            SqlCommand cd = new SqlCommand(string.Concat("Insert into Remaining (Id,rQuan,Date_rQuan) values ((select ItemId from Discarded where id = ", id, "),", item.Quantity - (quantity - dq), ", @date)"), con);
                            cd.Parameters.AddWithValue("@date", DateTime.Now);
                            cd.ExecuteNonQuery();

                            MessageBox.Show("Changes Applied!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.Close();
                        }
                        else
                            MessageBox.Show("Quantity isn't available!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                else if(usage == 5)
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmdd = new SqlCommand(string.Concat("Select Discarded_Quantity from Discarded where id = ", item.Id), con);
                    int dq = (int)cmdd.ExecuteScalar();

                    if (quantity <= dq)
                    {
                        SqlCommand comd = new SqlCommand(string.Concat("update Discarded set Discarded_Quantity = @Discarded_Quantity, Sold_Quantity = @Sold_Quantity where id = ", item.Id), con);
                        comd.Parameters.AddWithValue("@Discarded_Quantity", dq-quantity);
                        comd.Parameters.AddWithValue("@Sold_Quantity", quantity);
                        comd.ExecuteNonQuery();

                        MessageBox.Show("Item Quantity added in sold section!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }

                    else
                    {
                        MessageBox.Show("Entered Quantity isn't available in Bucket!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        textbox_quantity.Text = "";
                    }
                }
            }

            else { MessageBox.Show("Neither left empty nor enter invalid data!", "Forbiden", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
        }

        private void textbox_quantity_Click(object sender, EventArgs e)
        {
            if(usage == 4 && quantity_firstClick) { 
                quantity_firstClick = false;
                MessageBox.Show("Quantity Avaialabe in Stock = "+Item_quantity.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
