using Fesco_Inventory.BL;
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
    public partial class Bucket : UserControl
    {
        Panel pan;
        userBL user;
        Label label;
        public Bucket(Panel pan, userBL user, Label label)
        {
            InitializeComponent();
            this.pan = pan;
            this.user = user;
            this.label = label;
        }

        private void getUC(UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            pan.Controls.Clear();
            pan.Controls.Add(uc);
            uc.BringToFront();
        }

        void retrieve()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("select d.Id,(select ItemNo from inventory where id = d.ItemId)ItemNo , (select Item_Description from inventory where id = d.ItemId)Item_Description, (select [Value] from [Lookup] where Category = 'UNIT_OF_MEASURE' and id = (select Unit_of_Measure from inventory where id = d.ItemId)) Unit_of_Measure, d.qDisc, d.disc_On, (select EmployeeName from Employee where id = d.disc_By)disc_By, d.Updated  from  Discarded d", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Sort(dataGridView1.Columns["ColumnItem_No"], System.ComponentModel.ListSortDirection.Ascending);
            DataGridViewColumn column = dataGridView1.Columns["ColumnID"];
            column.Visible = false;
            if(!user.Isadmin)
            {
                DataGridViewColumn column1 = dataGridView1.Columns["CheckUpdated"];
                column1.Visible = false;
            }
        }

        void search()
        {
            if (comboBox_searchBy.SelectedItem.ToString() == "Item Number")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(string.Concat("select d.Id,(select ItemNo from inventory where id = d.ItemId)ItemNo , (select Item_Description from inventory where id = d.ItemId)Item_Description, (select [Value] from [Lookup] where Category = 'UNIT_OF_MEASURE' and id = (select Unit_of_Measure from inventory where id = d.ItemId)) Unit_of_Measure, d.qDisc, d.disc_On, (select EmployeeName from Employee where id = d.disc_By)disc_By, d.Updated  from  Discarded d Join Inventory i1 On i1.Id = d.ItemId where i1.ItemNo like '", textbox_search.Text,"%'"), con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }

            else if (comboBox_searchBy.SelectedItem.ToString() == "Item Description")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(string.Concat("select d.Id,(select ItemNo from inventory where id = d.ItemId)ItemNo , (select Item_Description from inventory where id = d.ItemId)Item_Description, (select [Value] from [Lookup] where Category = 'UNIT_OF_MEASURE' and id = (select Unit_of_Measure from inventory where id = d.ItemId)) Unit_of_Measure, d.qDisc, d.disc_On, (select EmployeeName from Employee where id = d.disc_By)disc_By, d.Updated  from  Discarded d Join Inventory i1 On i1.Id = d.ItemId where i1.Item_Description like '", textbox_search.Text, "%'"), con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            dataGridView1.Sort(dataGridView1.Columns["ColumnItem_No"], System.ComponentModel.ListSortDirection.Ascending);
            DataGridViewColumn column = dataGridView1.Columns["ColumnID"];
            column.Visible = false;
            if (!user.Isadmin)
            {
                DataGridViewColumn column1 = dataGridView1.Columns["CheckUpdated"];
                column1.Visible = false;
            }
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

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex != -1)
            {
                // Cancel the event to prevent any action
                dataGridView1.ClearSelection();
                dataGridView1.CurrentCell = null;
            }
            else
            {
                if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Edit")
                {
                    int id_ = (int)dataGridView1.Rows[e.RowIndex].Cells["ColumnID"].Value;
                    // Replace this with the datetime you want to compare
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand sqll = new SqlCommand(string.Concat("select disc_On from Discarded where id = ", id_), con);
                    DateTime targetDateTime = (DateTime)sqll.ExecuteScalar();

                    TimeSpan difference = DateTime.Now - targetDateTime;

                    if (difference.TotalDays <= 3)
                    {
                        Item_Editor nf = new Item_Editor(id_, 4, user, label);
                        nf.ShowDialog();
                        // The difference is greater than 3 days, so you can enter this condition
                    }
                    else
                    {
                        MessageBox.Show("Record Outdated!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // The difference is not greater than 3 days
                    }

                    
                }
                    retrieve();
            }
            
        }

        private void Bucket_Load(object sender, EventArgs e)
        {
            label.Text = "Bucket Management";
            retrieve();
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
            else if (textbox_search.Text != "Search")
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
    }
}
