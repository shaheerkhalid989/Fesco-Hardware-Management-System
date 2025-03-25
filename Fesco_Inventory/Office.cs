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
    public partial class Office : UserControl
    {
        userBL user;
        Panel pan;
        Label label;
        public Office(Panel pan, userBL user, Label label)
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

        private void retrieve()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Office_Code, office_Add from Office where isDeleted = 0", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Sort(dataGridView1.Columns["ColumnOC"], System.ComponentModel.ListSortDirection.Ascending);

        }

        void search()
        {
            if(comboBox_searchBy.Text == "Office Code")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(string.Concat("Select Office_Code, office_Add from Office where isDeleted = 0 and Office_Code like '",textbox_search.Text,"%'"), con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }

            else if(comboBox_searchBy.Text == "Office Name")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(string.Concat("Select Office_Code, office_Add from Office where isDeleted = 0 and office_Add like '", textbox_search.Text, "%'"), con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            dataGridView1.Sort(dataGridView1.Columns["ColumnOC"], System.ComponentModel.ListSortDirection.Ascending);


        }

        private void Office_Load(object sender, EventArgs e)
        {
            label.Text = "Offices Management";
            retrieve();
            comboBox_searchBy.SelectedText = "Search By";
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
            if ((comboBox_searchBy.Text == "Office Code" || comboBox_searchBy.Text == "Office Name") && textbox_search.Text == "Search")
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

        private void dataGridView1_SizeChanged(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btn_addoffice_Click(object sender, EventArgs e)
        {
            Office_Input newform = new Office_Input(null, null, label);
            newform.ShowDialog();
            retrieve();
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
                int officeCode = (int)dataGridView1.Rows[e.RowIndex].Cells["ColumnOC"].Value;
                string officeName = (string)dataGridView1.Rows[e.RowIndex].Cells["ColumnOA"].Value;

                var con = Configuration.getInstance().getConnection();
                SqlCommand command = new SqlCommand(string.Concat("Select id from Office where office_Code = ", officeCode, " and office_Add = '", officeName, "' and isDeleted = 0"), con);
                int id = (int)command.ExecuteScalar();

                if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Delete")
                {
                    DialogResult confirm = MessageBox.Show("Are you sure, You want to delete this Item?", "Confirmation Box", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm == DialogResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand(string.Concat("Update Office Set isDeleted = 1 where id = ", id), con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Office Details Deleted!", "Forbidden", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }

                else if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Edit")
                {
                    Office_Input nf = new Office_Input(officeCode.ToString(), officeName, label);
                    nf.ShowDialog();
                }
                retrieve();
            }
        }
    }
}
