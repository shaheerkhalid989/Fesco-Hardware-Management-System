using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fesco_Inventory.BL;
using Fesco_Inventory.DL;

namespace Fesco_Inventory
{
    public partial class InventryItem_AdminSup : UserControl
    {
        Panel pan;
        userBL user;
        string isOperator = "";
        Label label;
        public InventryItem_AdminSup(Panel pan,userBL user, Label label)
        {
            InitializeComponent();
            this.pan = pan; 
            this.user = user;
            this.label = label;
        }

        string getRole()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmdd = new SqlCommand(string.Concat("Select [Value] from Lookup where Id = ", user.Role, " and Category = 'ROLE'"), con);
            string role = (string)cmdd.ExecuteScalar();
            return role;
        }

        void retrieve()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("select i.ItemNo,i.Item_Description,Quantity,(select l.Value from [Lookup] l where l.Id = i.Unit_of_Measure and l.Category = 'UNIT_OF_MEASURE')Unit_of_Measure, convert(date,Added_ON)Added_On,(Select e.EmployeeName from employee e where e.id = i.Added_By) Added_By from Inventory i where i.Out_of_Stock = 0 and i.isDeleted = 0", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            if (isOperator == "Operator")
            {
                DataGridViewColumn column = dataGridView1.Columns["ColDiscard"];
                column.Visible = false;

                DataGridViewColumn column_ = dataGridView1.Columns["ColumnAddedBy"];
                column_.Visible = false;

                DataGridViewColumn column_D = dataGridView1.Columns["Del"];
                column_D.Visible = false;

                DataGridViewColumn column_E = dataGridView1.Columns["EditCol"];
                column_E.Visible = false;
            }
            dataGridView1.Sort(dataGridView1.Columns["ColumnItem_No"], System.ComponentModel.ListSortDirection.Ascending);
        }

        void search()
        {
            if (comboBox_searchBy.Text == "Item Number")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(string.Concat("select i.ItemNo,i.Item_Description,Quantity,(select l.Value from [Lookup] l where l.Id = i.Unit_of_Measure and l.Category = 'UNIT_OF_MEASURE')Unit_of_Measure, convert(date,Added_ON)Added_On,(Select e.EmployeeName from employee e where e.id = i.Added_By) Added_By from Inventory i where i.Out_of_Stock = 0 and i.isDeleted = 0 and i.ItemNo like '", textbox_search.Text, "%'"), con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                if (isOperator == "Operator")
                {
                    DataGridViewColumn column = dataGridView1.Columns["ColDiscard"];
                    column.Visible = false;

                    DataGridViewColumn column_ = dataGridView1.Columns["ColumnAddedBy"];
                    column_.Visible = false;

                    DataGridViewColumn column_D = dataGridView1.Columns["Del"];
                    column_D.Visible = false;

                    DataGridViewColumn column_E = dataGridView1.Columns["EditCol"];
                    column_E.Visible = false;
                }
            }

            else if (comboBox_searchBy.Text == "Item Description")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(string.Concat("select i.ItemNo,i.Item_Description,Quantity,(select l.Value from [Lookup] l where l.Id = i.Unit_of_Measure and l.Category = 'UNIT_OF_MEASURE')Unit_of_Measure, convert(date,Added_ON)Added_On,(Select e.EmployeeName from employee e where e.id = i.Added_By) Added_By from Inventory i where i.Out_of_Stock = 0 and i.isDeleted = 0 and i.Item_Description like '", textbox_search.Text, "%'"), con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                if (isOperator == "Operator")
                {
                    DataGridViewColumn column = dataGridView1.Columns["ColDiscard"];
                    column.Visible = false;

                    DataGridViewColumn column_ = dataGridView1.Columns["ColumnAddedBy"];
                    column_.Visible = false;

                    DataGridViewColumn column_D = dataGridView1.Columns["Del"];
                    column_D.Visible = false;

                    DataGridViewColumn column_E = dataGridView1.Columns["EditCol"];
                    column_E.Visible = false;
                }
            }
            dataGridView1.Sort(dataGridView1.Columns["ColumnItem_No"], System.ComponentModel.ListSortDirection.Ascending);
        }

        private void InventryItem_AdminSup_Load(object sender, EventArgs e)
        {
            label.Text = "Inventory Management";
            comboBox_searchBy.Text = "Search By";
            isOperator = getRole();

            if(!user.Isadmin)
            {
                btn_reports.Enabled = false;
                btn_reports.Visible = false;
            }

            retrieve();
        }

        private void getUC(UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            pan.Controls.Clear();
            pan.Controls.Add(uc);
            uc.BringToFront();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            if (user.Isadmin)
            {
                AdminOperations uc = new AdminOperations(pan, user, label);
                getUC(uc);
            }

            else
            {
                UsualOperations uc = new UsualOperations(pan, user, label);
                getUC(uc);
            }
        }

        private void textbox_search_TextChanged(object sender, EventArgs e)
        {
            if (textbox_search.Text != "Search")
                search();
        }

        private void textbox_search_Click(object sender, EventArgs e)
        {
            if ((comboBox_searchBy.Text == "Item Number" || comboBox_searchBy.Text == "Item Description") && textbox_search.Text == "Search")
            {
                textbox_search.Enabled = true;
                textbox_search.Text = "";
            }

            else if(textbox_search.Text != "Search")
            {

            }

            else
            {
                textbox_search.Enabled = false;
                MessageBox.Show("Select from Given Items!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textbox_search_Leave(object sender, EventArgs e)
        {
            if (textbox_search.Text == "")
                textbox_search.Text = "Search";
        }

        private void comboBox_searchBy_SelectedValueChanged(object sender, EventArgs e)
        {
            textbox_search.Text = "Search";
            textbox_search.Enabled = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex != -1)
            {
                // Cancel the event to prevent any action
                dataGridView1.ClearSelection();
                dataGridView1.CurrentCell = null;
            }
            else
            {

                if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Delete")
                {
                    DialogResult confirm = MessageBox.Show("Are you sure, You want to delete this Item?", "Confirmation Box", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm == DialogResult.Yes)
                    {
                        int itemNo = (int)dataGridView1.Rows[e.RowIndex].Cells["ColumnItem_No"].Value;
                        string itemDesc = (string)dataGridView1.Rows[e.RowIndex].Cells["ColumnDesc"].Value;

                        var con = Configuration.getInstance().getConnection();

                        SqlCommand co = new SqlCommand("select count(*) FROM Inventory where isDeleted = 0", con);
                        int count = (int)co.ExecuteScalar();

                        SqlCommand cmd = new SqlCommand(string.Concat("Update Inventory set isDeleted = @isDeleted where [Item No] = ", itemNo.ToString(), " and [Item Description] = '", itemDesc, "'"), con);
                        cmd.Parameters.AddWithValue("@isDeleted", 1);
                        cmd.ExecuteNonQuery();

                        SqlCommand c = new SqlCommand("select count(*) FROM Inventory where isDeleted = 0", con);
                        int cunt = (int)c.ExecuteScalar();

                        if (count > cunt)
                            MessageBox.Show("Item deleted Successfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                else if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Increase Stock")
                {
                    int itemNo_ = (int)dataGridView1.Rows[e.RowIndex].Cells["ColumnItem_No"].Value;
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand commd = new SqlCommand(string.Concat("Select id from Inventory where ItemNo = ", itemNo_.ToString()), con);
                    int id_ = (int)commd.ExecuteScalar();
                    Item_Editor nf = new Item_Editor(id_, 2, user, label);
                    nf.ShowDialog();
                }

                else if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Edit")
                {
                    if (user.Isadmin)
                    {
                        int itemNo_ = (int)dataGridView1.Rows[e.RowIndex].Cells["ColumnItem_No"].Value;
                        var con = Configuration.getInstance().getConnection();
                        SqlCommand commd = new SqlCommand(string.Concat("Select id from Inventory where ItemNo = ", itemNo_.ToString()),con);
                        int id_ = (int)commd.ExecuteScalar();
                        InventoryBL item_1 = InventoryDL.getItemFromInventoryTable(id_);
                        AddItem uc = new AddItem(pan, user, item_1, 2, label);
                        getUC(uc);
                    }

                    else
                    {

                        int itemNo_ = (int)dataGridView1.Rows[e.RowIndex].Cells["ColumnItem_No"].Value;
                        var con = Configuration.getInstance().getConnection();
                        SqlCommand commd = new SqlCommand(string.Concat("Select id from Inventory where ItemNo = ", itemNo_.ToString()), con);
                        int id_ = (int)commd.ExecuteScalar();
                        InventoryBL item_1 = InventoryDL.getItemFromInventoryTable(id_);

                        DateTime today = DateTime.Now;
                        TimeSpan differenec = item_1.Added_On.AddDays(3) - today;
                        if (differenec.Days >= 0)
                        {
                            AddItem uc = new AddItem(pan, user, item_1, 2, label);
                            getUC(uc);
                        }

                        else
                        {
                            MessageBox.Show("Added Item is editable within 3 days ..... Visit admin for further Assistance!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

                else if(dataGridView1.Columns[e.ColumnIndex].HeaderText == "Discard")
                {
                    int itemNo_ = (int)dataGridView1.Rows[e.RowIndex].Cells["ColumnItem_No"].Value;
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand commd = new SqlCommand(string.Concat("Select id from Inventory where ItemNo = ", itemNo_.ToString()), con);
                    int id_ = (int)commd.ExecuteScalar();
                    Item_Editor nf = new Item_Editor(id_, 3, user, label);
                    nf.ShowDialog();
                }
                retrieve();
            }
            
        }

        private void btn_addItem_Click(object sender, EventArgs e)
        {
            AddItem uc = new AddItem(pan, user,null,1, label);
            getUC(uc);
        }

        private void btn_OutofStock_Click(object sender, EventArgs e)
        {
            OutofStock uc = new OutofStock(pan, user, label);
            getUC(uc);
        }

        private void dataGridView1_SizeChanged(object sender, EventArgs e)
        {
            if (isOperator == "Operator")
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void btn_reports_Click(object sender, EventArgs e)
        {

            ReportType uc = new ReportType(pan,user, label);
            uc.ShowDialog();
        }
    }
}
