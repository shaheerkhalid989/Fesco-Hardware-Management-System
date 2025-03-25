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
    public partial class OutofStock : UserControl
    {
        userBL user;
        Panel pan;
        Label label;
        public OutofStock(Panel pan, userBL user, Label label)
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
            SqlCommand cmd = new SqlCommand("select i.ItemNo,i.Item_Description,(select l.Value from [Lookup] l where l.Id = i.Unit_of_Measure and l.Category = 'UNIT_OF_MEASURE')Unit_of_Measure from Inventory i where i.Out_of_Stock = 1 and i.isDeleted = 0", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void search()
        {
            if (comboBox_searchBy.Text == "Item Number")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(string.Concat("select i.ItemNo,i.Item_Description,(select l.Value from [Lookup] l where l.Id = i.Unit_of_Measure and l.Category = 'UNIT_OF_MEASURE')Unit_of_Measure from Inventory i where i.Out_of_Stock = 1 and i.isDeleted = 0 and i.ItemNo like '", textbox_search.Text, "%'"), con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Sort(dataGridView1.Columns["ColReqDate"], System.ComponentModel.ListSortDirection.Ascending);

            }

            else if (comboBox_searchBy.Text == "Item Description")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(string.Concat("select i.ItemNo,i.Item_Description,(select l.Value from [Lookup] l where l.Id = i.Unit_of_Measure and l.Category = 'UNIT_OF_MEASURE')Unit_of_Measure from Inventory i where i.Out_of_Stock = 1 and i.isDeleted = 0 and i.Item_Description like '", textbox_search.Text, "%'"), con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Sort(dataGridView1.Columns["ColReqDate"], System.ComponentModel.ListSortDirection.Ascending);
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            InventryItem_AdminSup uc = new InventryItem_AdminSup(pan, user, label);
            getUC(uc);
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
                if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Add Stock")
                {
                    int itemNo_ = (int)dataGridView1.Rows[e.RowIndex].Cells["ColumnItem_No"].Value;
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand commd = new SqlCommand(string.Concat("Select id from Inventory where ItemNo = ", itemNo_.ToString()), con);
                    int id_ = (int)commd.ExecuteScalar();
                    Item_Editor nf = new Item_Editor(id_, 1, user, label);
                    nf.ShowDialog();
                }
                retrieve();
            }
        }

        private void dataGridView1_SizeChanged(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void OutofStock_Load(object sender, EventArgs e)
        {
            label.Text = "Stockouts";
            retrieve();
        }
    }
}
