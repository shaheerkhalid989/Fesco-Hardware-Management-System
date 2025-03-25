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
using Fesco_Inventory.BL;
using Fesco_Inventory.DL;

namespace Fesco_Inventory
{
    public partial class userManagement : UserControl
    {
        userBL user;
        Panel pan;
        Label label;
        public userManagement(Panel pan, userBL user,Label label) 
        {
            InitializeComponent();
            this.pan = pan;
            this.user = user;
            this.label = label;

        }

        void retrieve()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("select e.EmployeeName,e.Cnic,e.Designation,(select l.Value from [Lookup] l where l.Id = e.UserRole and l.Category = 'ROLE') UserRole,(select l.Value from [Lookup] l where l.Id = e.Gender and l.Category = 'GENDER') Gender,e.Phone,e.email,e.Username,e.UserPassword from Employee e where e.isDeleted != 1", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            DataGridViewColumn column = dataGridView1.Columns["Username"];
            column.Visible = false;
            DataGridViewColumn column_ = dataGridView1.Columns["UserPassword"];
            column_.Visible = false;
            dataGridView1.Sort(dataGridView1.Columns["ColumnName"], System.ComponentModel.ListSortDirection.Ascending);
        }

        void search()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand(string.Concat("select e.EmployeeName,e.Cnic,e.Designation,(select l.Value from [Lookup] l where l.Id = e.UserRole and l.Category = 'ROLE') UserRole,(select l.Value from [Lookup] l where l.Id = e.Gender and l.Category = 'GENDER') Gender,e.Phone,e.email,e.Username,e.UserPassword from Employee e where e.EmployeeName like '",textbox_search.Text,"%' and e.isDeleted != 1"), con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            DataGridViewColumn column = dataGridView1.Columns["Username"];
            column.Visible = false;
            DataGridViewColumn column_ = dataGridView1.Columns["UserPassword"];
            column_.Visible = false;
            dataGridView1.Sort(dataGridView1.Columns["ColumnName"], System.ComponentModel.ListSortDirection.Ascending);
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
            AdminOperations uc = new AdminOperations(pan, user, label);
            getUC(uc);
        }

        private void userManagement_Load(object sender, EventArgs e)
        {
            label.Text = "User Management";
            retrieve();
        }

        private void textbox_search_TextChanged(object sender, EventArgs e)
        {
            if (textbox_search.Text != "Search")
                search();
        }

        private void textbox_search_Click(object sender, EventArgs e)
        {
            if (textbox_search.Text == "Search")
                textbox_search.Text = "";
        }

        private void textbox_search_Leave(object sender, EventArgs e)
        {
            if (textbox_search.Text == "")
                textbox_search.Text = "Search";
        }

        private void btn_addUser_Click(object sender, EventArgs e)
        {
            AddUser uc = new AddUser(pan, user, label);
            getUC(uc);
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

                if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Delete")
                {
                    DialogResult confirm = MessageBox.Show("Are you sure, You want to delete this user?", "Confirmation Box", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm == DialogResult.Yes)
                    {

                        string pass = (string)dataGridView1.Rows[e.RowIndex].Cells["Password"].Value;
                        string user = (string)dataGridView1.Rows[e.RowIndex].Cells["Username"].Value;

                        var con = Configuration.getInstance().getConnection();
                        SqlCommand cmd = new SqlCommand(string.Concat("Select Id from Employee where Username = '", user, "' and UserPassword = '", pass, "'"), con);
                        int id = (int)cmd.ExecuteScalar();

                        SqlCommand co = new SqlCommand("select count(*) FROM Employee where isDeleted = 0", con);
                        int count = (int)co.ExecuteScalar();

                        SqlCommand cmnnd = new SqlCommand("Update Employee set isDeleted = @isDeleted WHERE Id = @Id", con);
                        cmnnd.Parameters.AddWithValue("@Id", id);
                        cmnnd.Parameters.AddWithValue("@isDeleted", 1);
                        cmnnd.ExecuteNonQuery();

                        SqlCommand c = new SqlCommand("select count(*) FROM Employee where isDeleted = 0", con);
                        int cunt = (int)c.ExecuteScalar();

                        con.Close();

                        if (count > cunt)
                            MessageBox.Show("User deleted Successfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                else if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Edit")
                {

                    string pass = (string)dataGridView1.Rows[e.RowIndex].Cells["UserPassword"].Value;
                    string userr = (string)dataGridView1.Rows[e.RowIndex].Cells["Username"].Value;

                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand(string.Concat("Select Id from Employee where Username = '", userr, "' and UserPassword = '", pass, "'"), con);
                    int id = (int)cmd.ExecuteScalar();

                    userBL user_ = userDL.getUserFromEmployeeTable(id);

                    reCaptha uc = new reCaptha(pan, null, null, 2, user_, user, label);
                    getUC(uc);
                }
                retrieve();
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || e.Value == DBNull.Value)
            {
                e.Value = "Null";
                e.FormattingApplied = true;
            }
        }
    }
}
