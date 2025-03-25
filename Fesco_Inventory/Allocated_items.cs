using CrystalDecisions.ReportAppServer.CubeDefModel;
using Fesco_Inventory.BL;
using Fesco_Inventory.DL;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fesco_Inventory
{
    public partial class Allocated_items : UserControl
    {
        Panel pan;
        userBL user;
        Label label;
         
        public Allocated_items(Panel pan, userBL user, Label label)
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
            SqlCommand cmd = new SqlCommand("select r.Updated, r.id, r.FolioNo,(select ItemNo from Inventory where id = r.ItemId) ItemNO,(select Item_Description from Inventory where id = r.ItemId)ItemName,r.Quantity, (select [Value] from [Lookup] where id = r.Approved_By and Category = 'APPROVER')ApprovedBy,r.Issuing_officer,(select Office_Code from Office where id = r.OfficeId)OfficeId,(select office_Add from Office where id = r.OfficeId)OfficeName,r.Receiver,r.Receiver_Designation,r.Received_By,(select EmployeeName from Employee where id = r.Requisit_By)[Requisit_By],r.Requisition_Date,r.Receving_Date from Requisition r where r.Nature_of_Work = (select id from [Lookup] where [Value] = 'Item Issue' and Category = 'NATURE_OF_WORK')", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Sort(dataGridView1.Columns["ColReqDate"], System.ComponentModel.ListSortDirection.Descending);
            DataGridViewColumn column1 = dataGridView1.Columns["id"];
            column1.Visible = false;

            if(!user.Isadmin)
            {
                DataGridViewColumn column2 = dataGridView1.Columns["ColumnUpd"];
                column2.Visible = false;
            }
        }

        void search()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand(string.Concat("select r.Updated, r.id, r.FolioNo,(select ItemNo from Inventory where id = r.ItemId) ItemNO,(select Item_Description from Inventory where id = r.ItemId)ItemName,r.Quantity, (select [Value] from [Lookup] where id = r.Approved_By and Category = 'APPROVER')ApprovedBy,r.Issuing_officer,(select Office_Code from Office where id = r.OfficeId)OfficeId,(select office_Add from Office where id = r.OfficeId)OfficeName,r.Receiver,r.Receiver_Designation,r.Received_By,(select EmployeeName from Employee where id = r.Requisit_By)[Requisit_By],r.Requisition_Date,r.Receving_Date from Requisition r where r.FolioNo like '", textbox_search.Text,"%' and Year(r.Requisition_Date) = ",textbox_year.Text," and r.Nature_of_Work = (select id from [Lookup] where [Value] = 'Item Issue' and Category = 'NATURE_OF_WORK')"), con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Sort(dataGridView1.Columns["ColReqDate"], System.ComponentModel.ListSortDirection.Descending);
            DataGridViewColumn column1 = dataGridView1.Columns["id"];
            column1.Visible = false;

            if (!user.Isadmin)
            {
                DataGridViewColumn column2 = dataGridView1.Columns["ColumnUpd"];
                column2.Visible = false;
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            AllocateItem uc = new AllocateItem(pan, user, label);
            getUC(uc);
        }

        private void Allocated_items_Load(object sender, EventArgs e)
        {
            label.Text = "Allocated Requisitions";
            retrieve();
        }

        private void textbox_year_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex regex = new Regex(@"^[\d]+$");

            // Check if the pressed key matches the pattern
            if (!regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void textbox_year_Click(object sender, EventArgs e)
        {
            if(textbox_year.Text == "Enter Year")
            {
                textbox_year.Text = "";
            }
        }

        private void textbox_year_Leave(object sender, EventArgs e)
        {
            if(textbox_year.Text == "")
            {
                textbox_year.Text = "Enter Year";
            }
            else
            {
                if (int.TryParse(textbox_year.Text, out int enteredYear))
                {
                    // Perform a range check to ensure the year is reasonable (e.g., between 1000 and 9999)
                    if (enteredYear >= 1000 && enteredYear <= 9999)
                    {
                        // Optionally, you can perform additional checks if needed (e.g., check for a leap year).
                        textbox_search.Enabled = true;
                    }
                    else
                    {
                        textbox_search.Enabled = false;
                        MessageBox.Show("Invalid year. Please enter a valid 4-digit year.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void textbox_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex regex = new Regex(@"^[\d]+$");

            // Check if the pressed key matches the pattern
            if (!regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void textbox_search_TextChanged(object sender, EventArgs e)
        {
            if (textbox_search.Text != "Search (Folio#)")
                search();       
        }

        private void textbox_search_Leave(object sender, EventArgs e)
        {
            if (textbox_search.Text == "")
                textbox_search.Text = "Search (Folio#)";
        }

        private void textbox_search_Click(object sender, EventArgs e)
        {
            if(textbox_search.Text == "Search (Folio#)" && textbox_year.Text != "Enter Year" && textbox_year.Text != "")
            {
                textbox_search.Enabled = true;
                textbox_search.Text = "";
            }

            if(textbox_year.Text.Length == 4 && textbox_search.Text != "Search (Folio#)")
            {

            }

            else
            {
                textbox_search.Enabled = false;
                MessageBox.Show("Enter Required Year!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void textbox_year_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textbox_year.Text, out int enteredYear))
            {
                // Perform a range check to ensure the year is reasonable (e.g., between 1000 and 9999)
                if (enteredYear >= 1000 && enteredYear <= 9999)
                {
                    // Optionally, you can perform additional checks if needed (e.g., check for a leap year).
                    textbox_search.Enabled = true;
                }
            }
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
                if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "View")
                {
                    int id_ = (int)dataGridView1.Rows[e.RowIndex].Cells["id"].Value;
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand sqll = new SqlCommand(string.Concat("select DED from Requisition where id = ",id_),con);

                    DateTime targetDateTime = (DateTime)sqll.ExecuteScalar(); // Replace this with the datetime you want to compare

                    TimeSpan difference = DateTime.Now - targetDateTime;

                    if (difference.TotalDays <= 3)
                    {
                        ViewReq nf = new ViewReq(id_);
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
    }
}
