using Fesco_Inventory.BL;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Fesco_Inventory
{
    public partial class reports : UserControl
    {
        Panel pan;
        userBL user;
        int usage;
        Label label;
        int sum = 0;


        List<int> itemcodes = new List<int>();
        List<string> itemNames = new List<string>();

        public reports(Panel pan,userBL user, int usage, Label label)
        {
            InitializeComponent();
            this.pan = pan;
            this.user = user;
            this.usage = usage;
            this.label = label;
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            InventryItem_AdminSup uc = new InventryItem_AdminSup(pan,user, label);
            getUC(uc);
        }

        private void getUC(UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            pan.Controls.Clear();
            pan.Controls.Add(uc);
            uc.BringToFront();
        }

        private void AlignColumnHeaderToTheCenter(DataGridView dataGridView, string headerText)
        {
            // Find the column index based on the header text
            int columnIndex = -1;
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (column.HeaderText == headerText)
                {
                    columnIndex = column.Index;
                    break;
                }
            }

            if (columnIndex == -1)
            {
                // Column not found
                return;
            }

            // Align the header text to the middle center
            dataGridView.Columns[columnIndex].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void AlignColumnRowsToTheLeft(DataGridView dataGridView, string headerText)
        {
            // Find the column index based on the header text
            int columnIndex = -1;
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (column.HeaderText == headerText)
                {
                    columnIndex = column.Index;
                    break;
                }
            }

            if (columnIndex < 0 || columnIndex >= dataGridView.Columns.Count)
            {
                // Invalid column index
                return;
            }

            // Align the cells in the desired column to the left
            dataGridView.Columns[columnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        private void AlignColumnRowsToTheRight(DataGridView dataGridView, string headerText)
        {
            // Find the column index based on the header text
            int columnIndex = -1;
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (column.HeaderText == headerText)
                {
                    columnIndex = column.Index;
                    break;
                }
            }

            if (columnIndex < 0 || columnIndex >= dataGridView.Columns.Count)
            {
                // Invalid column index
                return;
            }

            // Align the cells in the desired column to the left
            dataGridView.Columns[columnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        void retrieve()
        {
            if (usage == 1)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(string.Concat("select i.Id, i.ItemNo [Item Code],(isNull((select Top 1 rQuan from Remaining where Convert(Date,Date_rQuan) >= '",dateTime_from.Value, "' and Convert(Date,Date_rQuan) <= '", dateTime_to.Value, "' and id = i.id Order By Date_rQuan),0)) [Side1],(isNull((select sum(added_stock) from AddedStock where Convert(Date,Date_added) >= '", dateTime_from.Value, "' and Convert(Date,Date_added) <= '", dateTime_to.Value, "' and id = i.Id),0))[Side2],i.Item_Description [Item Name],(isNull((select Top 1 rQuan from Remaining where Convert(Date,Date_rQuan) >= '", dateTime_from.Value, "' and Convert(Date,Date_rQuan) <= '", dateTime_to.Value, "' and id = i.id Order By Date_rQuan),0) + isNull((select sum(added_stock) from AddedStock where Convert(Date,Date_added) >= '", dateTime_from.Value, "' and Convert(Date,Date_added) <= '", dateTime_to.Value, "' and id = i.Id),0)) [Total Quantity],isNull((select Sum(Quantity) from Requisition where ItemId = i.Id and Nature_of_Work = (select id from [Lookup] where Category = 'NATURE_OF_WORK' and [value] = 'Item Issue') and Convert(Date,Receving_Date) >= '", dateTime_from.Value, "' and Convert(Date,Receving_Date) <= '", dateTime_to.Value,"'),0)[Issued Quantity],(isNull((select Top 1 rQuan from Remaining where Convert(Date,Date_rQuan) >= '", dateTime_from.Value,"' and Convert(Date,Date_rQuan) <= '", dateTime_to.Value, "' and id = i.id Order By Date_rQuan),0) + isNull((select sum(added_stock) from AddedStock where Convert(Date,Date_added) >= '", dateTime_from.Value,"' and Convert(Date,Date_added) <= '", dateTime_to.Value, "' and id = i.Id),0) - isNull((select Sum(Quantity) from Requisition where ItemId = i.Id and Nature_of_Work = (select id from [Lookup] where Category = 'NATURE_OF_WORK' and [value] = 'Item Issue') and Convert(Date,Receving_Date) >= '", dateTime_from.Value, "' and Convert(Date,Receving_Date) <= '", dateTime_to.Value, "'),0)) [Available Balance] from Inventory i Order By i.ItemNo"), con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                List <int> ids = new List<int>();

                foreach (DataRow row in dt.Rows)
                {
                    int id = Convert.ToInt32(row["Id"]);
                    SqlCommand ssql = new SqlCommand(string.Concat("Select Top 1 isNull(rQuan,0) from Remaining where Convert(Date,Date_rQuan) <= '", dateTime_from.Value, "' and id = ", id, " Order By Date_rQuan desc"), con);
                    object gotval = ssql.ExecuteScalar();
                    if (gotval != null)
                    {
                        if (Convert.ToInt32(row["Side1"]) == 0)
                        {
                            row["Total Quantity"] = (int)gotval + Convert.ToInt32(row["Side2"]);
                            row["Available Balance"] = (int)gotval + Convert.ToInt32(row["Side2"]);
                        }
                        //break; // If there is only one row with Id=5, you can break the loop after updating the value.
                    }

                    else
                        ids.Add(id);
                }


                for (int i = 0; i < ids.Count; i++)
                {
                    DataRow[] rowsToRemove = dt.Select("Id = " + ids[i]);

                    // Remove the selected rows from the DataTable
                    foreach (DataRow row in rowsToRemove)
                    {
                        dt.Rows.Remove(row);
                    }
                }

                dataGridView1.DataSource = dt;

                DataGridViewColumn column_1 = dataGridView1.Columns["Id"];
                column_1.Visible = false;
                DataGridViewColumn column_ = dataGridView1.Columns["Side1"];
                column_.Visible = false;
                DataGridViewColumn column_2 = dataGridView1.Columns["Side2"];
                column_2.Visible = false;
                DataGridViewColumn column1 = dataGridView1.Columns["Item Code"];
                column1.MinimumWidth = 100;
                DataGridViewColumn column2 = dataGridView1.Columns["Item Name"];
                column2.MinimumWidth = 150;
                DataGridViewColumn column3 = dataGridView1.Columns["Total Quantity"];
                column3.MinimumWidth = 100;
                DataGridViewColumn column4 = dataGridView1.Columns["Issued Quantity"];
                column4.MinimumWidth = 100;
                DataGridViewColumn column5 = dataGridView1.Columns["Available Balance"];
                column5.MinimumWidth = 100;

                DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();

                // Set the alignment to MiddleCenter for the cells
                cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Apply the cell style to all columns in the DataGridView
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.DefaultCellStyle = cellStyle;
                }

                AlignColumnRowsToTheRight(dataGridView1, "Total Quantity");
                AlignColumnRowsToTheRight(dataGridView1, "Issued Quantity");
                AlignColumnRowsToTheRight(dataGridView1, "Available Balance");
                AlignColumnRowsToTheLeft(dataGridView1, "Item Code");
                AlignColumnRowsToTheLeft(dataGridView1, "Item Name");

                dataGridView1.Sort(dataGridView1.Columns["Item Code"], System.ComponentModel.ListSortDirection.Ascending);
            }

            if(usage == 2)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(string.Concat("select (select ItemNo from Inventory where Id = r.ItemId) [Item Code], (select Item_Description from Inventory where Id = r.ItemId) [Item Name], r.Quantity [Issued Quantity], r.Requisition_Date [Requisition Date] from Requisition r where r.OfficeId = (select Id from Office where Office_Code = ",textbox_Code.Text," and office_Add = '",combobox_Name.Text,"') and convert(date,r.Requisition_Date) >= '",dateTime_from.Value,"' and convert(date,r.Requisition_Date) <= '",dateTime_to.Value,"' and r.Nature_of_Work = (select id from [Lookup] where Category = 'NATURE_OF_WORK' and value = 'Item Issue') Order By [Item Code]"), con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                DataGridViewColumn column1 = dataGridView1.Columns["Item Code"];
                column1.MinimumWidth = 110;
                DataGridViewColumn column2 = dataGridView1.Columns["Item Name"];
                column2.MinimumWidth = 130;
                DataGridViewColumn column4 = dataGridView1.Columns["Issued Quantity"];
                column4.MinimumWidth = 130;
                DataGridViewColumn column3 = dataGridView1.Columns["Requisition Date"];
                column3.MinimumWidth = 130;


                DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();

                // Set the alignment to MiddleCenter for the cells
                cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Apply the cell style to all columns in the DataGridView
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.DefaultCellStyle = cellStyle;
                }

                AlignColumnRowsToTheLeft(dataGridView1, "Item Name");
                AlignColumnRowsToTheLeft(dataGridView1, "Item Code");
                AlignColumnRowsToTheLeft(dataGridView1, "Issued Quantity");
                AlignColumnRowsToTheLeft(dataGridView1, "Requisition Date");

                dataGridView1.Sort(dataGridView1.Columns["Item Code"], System.ComponentModel.ListSortDirection.Ascending);
            }

            else if(usage == 3)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(string.Concat("select (select Office_Code from Office where Id = r.OfficeId) [Office Code], (select office_Add from Office where Id = r.OfficeId) [Office Name], r.Quantity [Issued Quantity], r.Requisition_Date [Requisition Date] from Requisition r where r.ItemId = (select Id from Inventory where ItemNo = ",textbox_Code.Text," and Item_Description = '",combobox_Name.Text,"') and convert(date,r.Requisition_Date) >= '",dateTime_from.Value,"' and convert(date,r.Requisition_Date) <= '",dateTime_to.Value,"' and r.Nature_of_Work = (select id from [Lookup] where Category = 'NATURE_OF_WORK' and [value] = 'Item Issue') Order By [Office Code]"), con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                DataGridViewColumn column1 = dataGridView1.Columns["Office Code"];
                column1.MinimumWidth = 110;
                DataGridViewColumn column2 = dataGridView1.Columns["Office Name"];
                column2.MinimumWidth = 130;
                DataGridViewColumn column4 = dataGridView1.Columns["Issued Quantity"];
                column4.MinimumWidth = 130;
                DataGridViewColumn column3 = dataGridView1.Columns["Requisition Date"];
                column3.MinimumWidth = 130;

                DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();

                // Set the alignment to MiddleCenter for the cells
                cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Apply the cell style to all columns in the DataGridView
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.DefaultCellStyle = cellStyle;
                }

                string targetHeaderText = "Issued Quantity"; // Replace with your target header text
                int columnIndex = -1; // Initialize column index

                // Find the column index based on the header text
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    if (column.HeaderText == targetHeaderText)
                    {
                        columnIndex = column.Index;
                        break; // Exit loop once the column is found
                    }
                }

                if (columnIndex != -1) // Column found
                { // Initialize the sum variable

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow && row.Cells[columnIndex].Value != null)
                        {
                            // Assuming the data type of the column is numeric (e.g., decimal or int)
                            int cellValue;
                            if (int.TryParse(row.Cells[columnIndex].Value.ToString(), out cellValue))
                            {
                                sum += cellValue;
                            }
                        }
                    }
                    // Display the sum or use it as needed
                    //MessageBox.Show($"Sum of {targetHeaderText}: {sum}");
                }

                AlignColumnRowsToTheLeft(dataGridView1, "Office Name");
                AlignColumnRowsToTheLeft(dataGridView1, "Office Code");
                AlignColumnRowsToTheLeft(dataGridView1, "Issued Quantity");
                AlignColumnRowsToTheLeft(dataGridView1, "Requisition Date");

                dataGridView1.Sort(dataGridView1.Columns["Office Code"], System.ComponentModel.ListSortDirection.Ascending);
            }

            else if(usage == 5)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(string.Concat("select (select ItemNo from Inventory where Id = r.ItemId) [Item Code], (select Item_Description from Inventory where Id = r.ItemId) [Item Name], r.Quantity [Repairable Quantity],(select Reason_Repair from Repair where Id = r.Id) [Reason Repair], r.Requisition_Date [Requisition Date] from Requisition r where r.OfficeId = (select Id from Office where Office_Code = ",textbox_Code.Text," and office_Add = '",combobox_Name.Text,"') and convert(date,r.Requisition_Date) >= '",dateTime_from.Value,"' and convert(date,r.Requisition_Date) <= '",dateTime_to.Value,"' and r.Nature_of_Work = (select id from [Lookup] where Category = 'NATURE_OF_WORK' and [value] = 'Item Repair') Order By [Item Code]"), con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                DataGridViewColumn column1 = dataGridView1.Columns["Item Code"];
                column1.MinimumWidth = 110;
                DataGridViewColumn column2 = dataGridView1.Columns["Item Name"];
                column2.MinimumWidth = 130;
                DataGridViewColumn column4 = dataGridView1.Columns["Repairable Quantity"];
                column4.MinimumWidth = 130;
                DataGridViewColumn column3 = dataGridView1.Columns["Requisition Date"];
                column3.MinimumWidth = 130;
                DataGridViewColumn column5 = dataGridView1.Columns["Reason Repair"];
                column5.MinimumWidth = 130;

                DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();

                // Set the alignment to MiddleCenter for the cells
                cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Apply the cell style to all columns in the DataGridView
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.DefaultCellStyle = cellStyle;
                }

                AlignColumnRowsToTheLeft(dataGridView1, "Item Name");
                AlignColumnRowsToTheLeft(dataGridView1, "Item Code");
                AlignColumnRowsToTheLeft(dataGridView1, "Repairable Quantity");
                AlignColumnRowsToTheLeft(dataGridView1, "Requisition Date");
                AlignColumnRowsToTheLeft(dataGridView1, "Reason Repair");

                dataGridView1.Sort(dataGridView1.Columns["Item Code"], System.ComponentModel.ListSortDirection.Ascending);
            }

            else if(usage == 6)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(string.Concat("select (select ItemNo from Inventory where Id = r.ItemId) [Item Code], (select Item_Description from Inventory where Id = r.ItemId) [Item Name], r.qDisc [Quantity Discarded], r.disc_On [Discarded On] from Discarded r where convert(date,r.disc_On) >= '",dateTime_from.Value,"' and convert(date,r.disc_On) <= '",dateTime_to.Value,"' Order By [Item Code]"), con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                DataGridViewColumn column1 = dataGridView1.Columns["Item Code"];
                column1.MinimumWidth = 110;
                DataGridViewColumn column2 = dataGridView1.Columns["Item Name"];
                column2.MinimumWidth = 130;
                DataGridViewColumn column4 = dataGridView1.Columns["Quantity Discarded"];
                column4.MinimumWidth = 130;
                DataGridViewColumn column3 = dataGridView1.Columns["Discarded On"];
                column3.MinimumWidth = 130;

                DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();

                // Set the alignment to MiddleCenter for the cells
                cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Apply the cell style to all columns in the DataGridView
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.DefaultCellStyle = cellStyle;
                }

                AlignColumnRowsToTheLeft(dataGridView1, "Item Name");
                AlignColumnRowsToTheLeft(dataGridView1, "Item Code");
                AlignColumnRowsToTheLeft(dataGridView1, "Quantity Discarded");
                AlignColumnRowsToTheLeft(dataGridView1, "Discarded On");

                dataGridView1.Sort(dataGridView1.Columns["Item Code"], System.ComponentModel.ListSortDirection.Ascending);
            }

            else if (usage == 7)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(string.Concat("select r.qDisc [Quantity Discarded], r.disc_On [Discarded On], (select EmployeeName from Employee where Id = r.disc_By) [Discarded By] from Discarded r where convert(date,r.disc_On) >= '", dateTime_from.Value, "' and convert(date,r.disc_On) <= '", dateTime_to.Value, "' and r.ItemId = (select id from inventory where ItemNo = ",textbox_Code.Text," and Item_Description = '",combobox_Name.Text, "') Order By [Discarded On]"), con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;


                DataGridViewColumn column2 = dataGridView1.Columns["Discarded By"];
                column2.MinimumWidth = 130;
                DataGridViewColumn column4 = dataGridView1.Columns["Quantity Discarded"];
                column4.MinimumWidth = 130;
                DataGridViewColumn column3 = dataGridView1.Columns["Discarded On"];
                column3.MinimumWidth = 130;

                DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();

                // Set the alignment to MiddleCenter for the cells
                cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Apply the cell style to all columns in the DataGridView
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.DefaultCellStyle = cellStyle;
                }

                AlignColumnRowsToTheLeft(dataGridView1, "Quantity Discarded");
                AlignColumnRowsToTheLeft(dataGridView1, "Discarded On");
                AlignColumnRowsToTheLeft(dataGridView1, "Discarded By");

                //dataGridView1.Sort(dataGridView1.Columns["Item Code"], System.ComponentModel.ListSortDirection.Ascending);
            }
        }

        private void reports_Load(object sender, EventArgs e)
        {
            label.Text = "Report View";

            label2.Visible = false;
            label2.Enabled = false;
            label1.Visible = false;
            label1.Enabled = false;
            textbox_Code.Enabled = false;
            textbox_Code.Visible = false;
            combobox_Name.Enabled = false;
            combobox_Name.Visible = false;
            dateTime_from.Value = DateTime.Now;
            dateTime_to.Value = DateTime.Now;
            btn_getReport.Enabled = false;
            btn_getReport.Visible = false;

            if(usage == 2 || usage == 5)
            {
                label2.Visible = true;
                label2.Enabled = true;
                label1.Visible = true;
                label1.Enabled = true;
                textbox_Code.Enabled = true;
                textbox_Code.Visible = true;
                combobox_Name.Enabled = true;
                combobox_Name.Visible = true;

                label2.Text = "office Code*";
                label1.Text = "office Name*";

                combobox_Name.Items.Clear();
                itemcodes.Clear();
                itemNames.Clear();
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(string.Concat("Select office_Add,office_Code from Office"), con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // Assuming the column data is of string type
                    string columnValue = reader["office_Add"].ToString();
                    combobox_Name.Items.Add(columnValue);
                    itemNames.Add(columnValue);
                    itemcodes.Add((int)reader["Office_Code"]);
                }
                reader.Close();
            }

            else if(usage == 3 || usage == 7)
            {
                label2.Visible = true;
                label2.Enabled = true;
                label1.Visible = true;
                label1.Enabled = true;
                textbox_Code.Enabled = true;
                textbox_Code.Visible = true;
                combobox_Name.Enabled = true;
                combobox_Name.Visible = true;

                label2.Text = "Item Code*";
                label1.Text = "Item Name*";

                combobox_Name.Items.Clear();
                itemcodes.Clear();
                itemNames.Clear();
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(string.Concat("Select ItemNo,Item_Description from Inventory"), con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // Assuming the column data is of string type
                    string columnValue = reader["Item_Description"].ToString();
                    combobox_Name.Items.Add(columnValue);
                    itemNames.Add(columnValue);
                    itemcodes.Add((int)reader["ItemNo"]);
                }
                reader.Close();
            }

            combobox_Name.DropDownStyle = ComboBoxStyle.DropDown;

            
        }

        private void btn_getReport_Click(object sender, EventArgs e)
        {
            if(usage == 1)
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

                DGVPrinter printer = new DGVPrinter();
                printer.Title = string.Format("{0}\n{1}","Inventory Report (Overall)","         ");
                printer.TitleFont = new Font("Times New Roman", 24, FontStyle.Bold);
                string sub1 = "Date: '" + dateTime_from.Value.Date.ToString("dd/MM/yyyy") + "' - '" + dateTime_to.Value.Date.ToString("dd/MM/yyyy") + "'";
                string sub2 = "         ";
                printer.SubTitle = string.Format("{0}\n{1}", sub1, sub2);
                printer.SubTitleFont = new Font("Times New Roman", 16, FontStyle.Bold);
                printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                printer.PageNumbers = true;
                printer.PageNumberInHeader = false;
                printer.PorportionalColumns = true;
                printer.HeaderCellAlignment = StringAlignment.Near;
                printer.Footer = "Printed On: "+DateTime.Today.Date.ToString("dd/MM/yyyy");
                printer.FooterFont = new Font("Times New Roman", 9, FontStyle.Bold);
                printer.FooterSpacing = 15;
                printer.FooterSpacing = 15;
                printer.PrintMargins.Left = 50;
                printer.PrintMargins.Right = 50;
                printer.PrintMargins.Top = 70;
                printer.PrintMargins.Bottom = 70;
                printer.PrintPreviewDataGridView(dataGridView1);

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            } 

            else if (usage == 2)
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

                DGVPrinter printer = new DGVPrinter();
                printer.Title = string.Format("{0}\n{1}", "Inventory Report (Departmental)", "         ");
                printer.TitleFont = new Font("Times New Roman", 24, FontStyle.Bold);
                string sub1 = "Date: '" + dateTime_from.Value.Date.ToString("dd/MM/yyyy") + "' - '" + dateTime_to.Value.Date.ToString("dd/MM/yyyy") + "'";
                string sub2 = "Office Code = " + textbox_Code.Text + " | Office Name = '" + combobox_Name.Text + "'";
                string sub3 = "         ";
                printer.SubTitle = string.Format("{0}\n{1}\n{2}", sub1, sub2, sub3);
                printer.SubTitleFont = new Font("Times New Roman", 16, FontStyle.Bold);
                printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                printer.PageNumbers = true;
                printer.PageNumberInHeader = false;
                printer.PorportionalColumns = true;
                printer.HeaderCellAlignment = StringAlignment.Near;
                printer.Footer = "Printed On: " + DateTime.Today.Date.ToString("dd/MM/yyyy");
                printer.FooterFont = new Font("Times New Roman", 9, FontStyle.Bold);
                printer.FooterSpacing = 15;
                printer.FooterSpacing = 15;
                printer.PrintMargins.Left = 50;
                printer.PrintMargins.Right = 50;
                printer.PrintMargins.Top = 70;
                printer.PrintMargins.Bottom = 70;
                printer.PrintPreviewDataGridView(dataGridView1);

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            else if (usage == 3)
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

                DGVPrinter printer = new DGVPrinter();
                printer.Title = string.Format("{0}\n{1}", "Inventory Report (Item-Based)", "         ");
                printer.TitleFont = new Font("Times New Roman", 24, FontStyle.Bold);
                string sub1 = "Date: '" + dateTime_from.Value.Date.ToString("dd/MM/yyyy") + "' - '" + dateTime_to.Value.Date.ToString("dd/MM/yyyy") + "'";
                string sub2 = "Item Code = "+textbox_Code.Text +" | Item Name = '"+combobox_Name.Text+"'";
                string sub4 = "Total Issued Quantity = " + sum.ToString();
                string sub3 = "         ";
                printer.SubTitle = string.Format("{0}\n{1}\n{2}\n{3}", sub1, sub2,sub4,sub3);
                printer.SubTitleFont = new Font("Times New Roman", 16, FontStyle.Bold);
                printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                printer.PageNumbers = true;
                printer.PageNumberInHeader = false;
                printer.PorportionalColumns = true;
                printer.HeaderCellAlignment = StringAlignment.Near;
                printer.Footer = "Printed On: " + DateTime.Today.Date.ToString("dd/MM/yyyy");
                printer.FooterFont = new Font("Times New Roman", 9, FontStyle.Bold);
                printer.FooterSpacing = 15;
                printer.FooterSpacing = 15;
                printer.PrintMargins.Left = 50;
                printer.PrintMargins.Right = 50;
                printer.PrintMargins.Top = 70;
                printer.PrintMargins.Bottom = 70;
                printer.PrintPreviewDataGridView(dataGridView1);

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            else if (usage == 5)
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

                DGVPrinter printer = new DGVPrinter();
                printer.Title = string.Format("{0}\n{1}", "Items Repair Report (Departmental)", "         ");
                printer.TitleFont = new Font("Times New Roman", 24, FontStyle.Bold);
                string sub1 = "Date: '" + dateTime_from.Value.Date.ToString("dd/MM/yyyy") + "' - '" + dateTime_to.Value.Date.ToString("dd/MM/yyyy") + "'";
                string sub2 = "Office Code = " + textbox_Code.Text + " | Office Name = '" + combobox_Name.Text + "'";
                string sub3 = "         ";
                printer.SubTitle = string.Format("{0}\n{1}\n{2}", sub1, sub2, sub3);
                printer.SubTitleFont = new Font("Times New Roman", 16, FontStyle.Bold);
                printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                printer.PageNumbers = true;
                printer.PageNumberInHeader = false;
                printer.PorportionalColumns = true;
                printer.HeaderCellAlignment = StringAlignment.Near;
                printer.Footer = "Printed On: " + DateTime.Today.Date.ToString("dd/MM/yyyy");
                printer.FooterFont = new Font("Times New Roman", 9, FontStyle.Bold);
                printer.FooterSpacing = 15;
                printer.FooterSpacing = 15;
                printer.PrintMargins.Left = 50;
                printer.PrintMargins.Right = 50;
                printer.PrintMargins.Top = 70;
                printer.PrintMargins.Bottom = 70;
                printer.PrintPreviewDataGridView(dataGridView1);

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            else if (usage == 6)
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

                DGVPrinter printer = new DGVPrinter();
                printer.Title = string.Format("{0}\n{1}", "Discarded Items Report (Overall)", "         ");
                printer.TitleFont = new Font("Times New Roman", 24, FontStyle.Bold);
                string sub1 = "Date: '" + dateTime_from.Value.Date.ToString("dd/MM/yyyy") + "' - '" + dateTime_to.Value.Date.ToString("dd/MM/yyyy") + "'";
                //string sub2 = "Office Code = " + textbox_Code.Text + " | Office Name = '" + combobox_Name.Text + "'";
                string sub3 = "         ";
                printer.SubTitle = string.Format("{0}\n{1}", sub1, sub3);
                printer.SubTitleFont = new Font("Times New Roman", 16, FontStyle.Bold);
                printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                printer.PageNumbers = true;
                printer.PageNumberInHeader = false;
                printer.PorportionalColumns = true;
                printer.HeaderCellAlignment = StringAlignment.Near;
                printer.Footer = "Printed On: " + DateTime.Today.Date.ToString("dd/MM/yyyy");
                printer.FooterFont = new Font("Times New Roman", 9, FontStyle.Bold);
                printer.FooterSpacing = 15;
                printer.FooterSpacing = 15;
                printer.PrintMargins.Left = 50;
                printer.PrintMargins.Right = 50;
                printer.PrintMargins.Top = 70;
                printer.PrintMargins.Bottom = 70;
                printer.PrintPreviewDataGridView(dataGridView1);

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            else if(usage == 7)
            {
                DGVPrinter printer = new DGVPrinter();
                printer.Title = string.Format("{0}\n{1}", "Discarded Items Report (Item-Based)", "         ");
                printer.TitleFont = new Font("Times New Roman", 24, FontStyle.Bold);
                string sub1 = "Date: '" + dateTime_from.Value.Date.ToString("dd/MM/yyyy") + "' - '" + dateTime_to.Value.Date.ToString("dd/MM/yyyy") + "'";
                string sub2 = "Item Code = " + textbox_Code.Text + " | Item Name = '" + combobox_Name.Text + "'";
                string sub3 = "         ";
                printer.SubTitle = string.Format("{0}\n{1}\n{2}", sub1, sub2, sub3);
                printer.SubTitleFont = new Font("Times New Roman", 16, FontStyle.Bold);
                printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                printer.PageNumbers = true;
                printer.PageNumberInHeader = false;
                printer.PorportionalColumns = true;
                printer.HeaderCellAlignment = StringAlignment.Near;
                printer.Footer = "Printed On: " + DateTime.Today.Date.ToString("dd/MM/yyyy");
                printer.FooterFont = new Font("Times New Roman", 9, FontStyle.Bold);
                printer.FooterSpacing = 15;
                printer.FooterSpacing = 15;
                printer.PrintMargins.Left = 50;
                printer.PrintMargins.Right = 50;
                printer.PrintMargins.Top = 70;
                printer.PrintMargins.Bottom = 70;
                printer.PrintPreviewDataGridView(dataGridView1);

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void textbox_ItemNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Regex regex = new Regex(@"^[\d]+$");

            //// Check if the pressed key matches the pattern
            //if (!regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != (char)Keys.Back)
            //{
            //    e.Handled = true; // Ignore the input
            //}
        }

        private void textbox_ItemNo_Leave(object sender, EventArgs e)
        {
            //if (textbox_Code.Text != "" && (usage == 2 || usage == 5))
            //{
            //    var con = Configuration.getInstance().getConnection();
            //    SqlCommand cmd = new SqlCommand(string.Concat("select office_Add from Office where Office_Code = ", textbox_Code.Text, " and isDeleted != 1"), con);
            //    object answer = cmd.ExecuteScalar();

            //    if (answer != null)
            //    {
            //        combobox_Name.Text = answer.ToString();
            //    }

            //    else
            //        combobox_Name.Text = "Office not found!";
            //}

            //if (textbox_Code.Text != "" && usage == 3)
            //{
            //    var con = Configuration.getInstance().getConnection();
            //    SqlCommand cmd = new SqlCommand(string.Concat("select Item_Description from Inventory where ItemNo = ", textbox_Code.Text, " and isDeleted != 1"), con);
            //    object answer = cmd.ExecuteScalar();

            //    if (answer != null)
            //    {
            //        combobox_Name.Text = answer.ToString();
            //    }

            //    else
            //        combobox_Name.Text = "Item not found!";
            //}
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            if (usage == 1 || usage == 6)
            {
                retrieve();
                btn_getReport.Enabled = true;
                btn_getReport.Visible = true;
            }

            else if (usage == 2 || usage == 3 || usage == 5 || usage == 7)
            {

                if (textbox_Code.Text != "" && (combobox_Name.Text != "Not Found!"))
                {
                    retrieve();
                    btn_getReport.Enabled = true;
                    btn_getReport.Visible = true;
                }

                else
                    MessageBox.Show("Data isn't valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void dataGridView1_SizeChanged(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        

        private void combobox_Name_TextChanged_1(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            //SqlCommand cmdc = new SqlCommand(string.Concat("Select Id from Office where office_Add = '", combobox_Name.Text, "'"), con);
            //object iid = cmdc.ExecuteScalar();


            //combobox_Name.DroppedDown = true;
            if (usage == 2 || usage == 5)
            {
                if (!string.IsNullOrEmpty(combobox_Name.Text))
                {
                    if (!itemNames.Contains(combobox_Name.Text))
                    {
                        combobox_Name.Items.Clear();
                        itemcodes.Clear();
                        itemNames.Clear();
                        combobox_Name.SelectionStart = combobox_Name.Text.Length;

                        SqlCommand cmd = new SqlCommand(string.Concat("Select office_Add,Office_Code from Office where office_Add Like '", combobox_Name.Text, "%'"), con);
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            // Assuming the column data is of string type
                            string columnValue = reader["office_Add"].ToString();
                            combobox_Name.Items.Add(columnValue);
                            itemNames.Add(columnValue);
                            itemcodes.Add((int)reader["Office_Code"]);
                        }
                        reader.Close();
                    }
                }

                else
                {
                    combobox_Name.Items.Clear();
                    itemcodes.Clear();
                    itemNames.Clear();
                    SqlCommand cmd = new SqlCommand(string.Concat("Select office_Add,Office_Code from Office"), con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Assuming the column data is of string type
                        string columnValue = reader["office_Add"].ToString();
                        combobox_Name.Items.Add(columnValue);
                        itemNames.Add(columnValue);
                        itemcodes.Add((int)reader["Office_Code"]);
                    }
                    //combobox_Name.DroppedDown = true;
                    reader.Close();
                }
                //ct = combobox_Name.Text;
            }
            else if(usage == 3)
            {
                if (!string.IsNullOrEmpty(combobox_Name.Text))
                {
                    if (!itemNames.Contains(combobox_Name.Text))
                    {
                        combobox_Name.Items.Clear();
                        itemcodes.Clear();
                        itemNames.Clear();
                        combobox_Name.SelectionStart = combobox_Name.Text.Length;

                        SqlCommand cmd = new SqlCommand(string.Concat("Select Item_Description,ItemNo from Inventory where Item_Description Like '", combobox_Name.Text, "%'"), con);
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            // Assuming the column data is of string type
                            string columnValue = reader["Item_Description"].ToString();
                            combobox_Name.Items.Add(columnValue);
                            itemNames.Add(columnValue);
                            itemcodes.Add((int)reader["ItemNo"]);
                        }
                        reader.Close();
                    }
                }

                else
                {
                    combobox_Name.Items.Clear();
                    itemcodes.Clear();
                    itemNames.Clear();
                    SqlCommand cmd = new SqlCommand(string.Concat("Select Item_Description,ItemNo from Inventory"), con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Assuming the column data is of string type
                        string columnValue = reader["Item_Description"].ToString();
                        combobox_Name.Items.Add(columnValue);
                        itemNames.Add(columnValue);
                        itemcodes.Add((int)reader["ItemNo"]);
                    }
                    //combobox_Name.DroppedDown = true;
                    reader.Close();
                }
            }
            
        }

        private void combobox_Name_Click(object sender, EventArgs e)
        {
            //flg = true;
            combobox_Name.DroppedDown = true; // Open the dropdown list
        }

        private void combobox_Name_Leave(object sender, EventArgs e)
        {
            bool flge = false;
            //combobox_Name.SelectedText = ct;
            string val = combobox_Name.Text.ToString();
            for (int i = 0; i < itemNames.Count; i++)
            {

                //MessageBox.Show(itemNames[i]);
                if (itemNames[i] == val)
                {
                    flge=true;
                    textbox_Code.Text = itemcodes[i].ToString();
                    break;
                }
            }
            if (!flge)
                textbox_Code.Text = "Not Found!";
        }

        private void combobox_Name_SelectedValueChanged(object sender, EventArgs e)
        {
            
            textbox_Code.Focus();
            
            string val = combobox_Name.Text.ToString();
            for (int i = 0; i < itemNames.Count; i++)
            {

                //MessageBox.Show(itemNames[i]);
                if (itemNames[i] == val)
                {

                    textbox_Code.Text = itemcodes[i].ToString();
                    break;
                }
            }

        }
    }
}
