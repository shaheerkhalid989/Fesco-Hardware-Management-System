using Fesco_Inventory.BL;
using Fesco_Inventory.DL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.Native.WinApi;

namespace Fesco_Inventory
{
    public partial class AllocateItem : UserControl
    {
        Panel pan;
        userBL user;
        Label label;
        byte[] Req_form = null;

        int itemCodeCheck = 0;
        string isOperator = "";

        public AllocateItem(Panel pan, userBL user, Label label)
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

        string getRole()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmdd = new SqlCommand(string.Concat("Select [Value] from Lookup where Id = ", user.Role, " and Category = 'ROLE'"), con);
            string role = (string)cmdd.ExecuteScalar();
            return role;
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

        private void btn_allocations_Click(object sender, EventArgs e)
        {
            Allocated_items uc = new Allocated_items(pan, user, label);
            getUC(uc);
        }

        private void btn_repairs_Click(object sender, EventArgs e)
        {
            repair_items uc = new repair_items(pan, user, label);
            getUC(uc);
        }

        private void comboBox_NatureOfWork_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox_NatureOfWork.SelectedItem != null)
            {
                if (comboBox_NatureOfWork.SelectedItem.ToString() == "Item Repair")
                {
                    tableLayoutPanel_itemCode.Enabled = true;
                    tableLayoutPanel_itemCode.Visible = true;
                    tableLayoutPanel_ItemName.Enabled = true;
                    tableLayoutPanel_ItemName.Visible = true;
                    tableLayoutPanel_itemQ.Enabled = true;
                    tableLayoutPanel_itemQ.Visible = true;
                    tableLayoutPanel_rr.Enabled = true;
                    tableLayoutPanel_rr.Visible = true;
                    tableLayoutPanel_balance.Enabled = false;
                    tableLayoutPanel_balance.Visible = false;

                    label5.Enabled = true;
                    label5.Visible = true;
                    textbox_repair.Enabled = true;
                    textbox_repair.Visible = true;
                    label6.Enabled = true;
                    label6.Visible = true;
                    label7.Enabled = true;
                    label7.Visible = true;
                    textbox_itemCode.Enabled = true;
                    textbox_itemCode.Visible = true;
                    textbox_itemName.Enabled = true;
                    textbox_itemName.Visible = true;
                    label8.Visible = true;
                    label8.Enabled = true;
                    textbox_itemQuantity.Enabled = true;
                    textbox_itemQuantity.Visible = true;


                }

                else if (comboBox_NatureOfWork.SelectedItem.ToString() == "Item Issue")
                {
                    tableLayoutPanel_itemCode.Enabled = true;
                    tableLayoutPanel_itemCode.Visible = true;
                    tableLayoutPanel_ItemName.Enabled = true;
                    tableLayoutPanel_ItemName.Visible = true;
                    tableLayoutPanel_itemQ.Enabled = true;
                    tableLayoutPanel_itemQ.Visible = true;
                    tableLayoutPanel_rr.Enabled = false;
                    tableLayoutPanel_rr.Visible = false;

                    label5.Enabled = false;
                    label5.Visible = false;
                    textbox_repair.Enabled = false;
                    textbox_repair.Visible = false;
                    label6.Enabled = true;
                    label6.Visible = true;
                    label7.Enabled = true;
                    label7.Visible = true;
                    textbox_itemCode.Enabled = true;
                    textbox_itemCode.Visible = true;
                    textbox_itemName.Enabled = true;
                    textbox_itemName.Visible = true;
                    label8.Visible = true;
                    label8.Enabled = true;
                    textbox_itemQuantity.Enabled = true;
                    textbox_itemQuantity.Visible = true;
                }
            }
        }

        private void AllocateItem_Load(object sender, EventArgs e)
        {
            label.Text = "Requisition Management";

            label5.Enabled = false;
            label5.Visible = false;
            textbox_repair.Enabled = false;
            textbox_repair.Visible = false;
            label6.Enabled = false;
            label6.Visible = false;
            label7.Enabled = false;
            label7.Visible = false;
            textbox_itemCode.Enabled = false;
            textbox_itemCode.Visible = false;
            textbox_itemName.Enabled = false;
            textbox_itemName.Visible = false;
            label8.Visible = false;
            label8.Enabled = false;
            textbox_itemQuantity.Enabled = false;
            textbox_itemQuantity.Visible = false;

            tableLayoutPanel_itemCode.Enabled = false;
            tableLayoutPanel_itemCode.Visible = false;
            tableLayoutPanel_ItemName.Enabled = false;
            tableLayoutPanel_ItemName.Visible = false;
            tableLayoutPanel_balance.Enabled = false;
            tableLayoutPanel_balance.Visible = false;
            tableLayoutPanel_itemQ.Enabled = false;
            tableLayoutPanel_itemQ.Visible = false;
            tableLayoutPanel_rr.Enabled = false;
            tableLayoutPanel_rr.Visible = false;

            date_Receive.Value = DateTime.Today;
            date_requisition.Value = DateTime.Today;
            isOperator = getRole();
            if (isOperator == "Operator")
            {
                btn_allocations.Enabled = false;
                btn_allocations.Visible = false;
                btn_repairs.Enabled = false;
                btn_repairs.Visible = false;
            }
            //isLoad = true;
        }

        private void textbox_officeCode_Leave(object sender, EventArgs e)
        {
            if (textbox_officeCode.Text != "")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(string.Concat("select office_Add from Office where Office_Code = ", textbox_officeCode.Text, " and isDeleted != 1"), con);
                object answer = cmd.ExecuteScalar();

                if (answer != null)
                {
                    textbox_officeName.Text = answer.ToString();
                }

                else
                    textbox_officeName.Text = "Office not found!";
            }

        }

        private void textbox_officeCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex regex = new Regex(@"^[\d]+$");

            // Check if the pressed key matches the pattern
            if (!regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void textbox_itemCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex regex = new Regex(@"^[\d]+$");

            // Check if the pressed key matches the pattern
            if (!regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void textbox_itemCode_Leave(object sender, EventArgs e)
        {
            if (textbox_itemCode.Text != "")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(string.Concat("select Item_Description from Inventory where ItemNo = ", textbox_itemCode.Text, " and isDeleted != 1"), con);
                object answer = cmd.ExecuteScalar();

                if (answer != null)
                {
                    textbox_itemName.Text = answer.ToString();

                    SqlCommand co = new SqlCommand(string.Concat("select id from Inventory where ItemNo = ", textbox_itemCode.Text, " and Item_Description = '", textbox_itemName.Text, "'"), con);
                    InventoryBL item_1 = InventoryDL.getItemFromInventoryTable((int)co.ExecuteScalar());

                    if (comboBox_NatureOfWork.SelectedItem != null && comboBox_NatureOfWork.SelectedItem.ToString() == "Item Issue" && itemCodeCheck != item_1.ItemNo)
                    {
                        //MessageBox.Show("Available quantity = " + item_1.Quantity.ToString(), "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tableLayoutPanel_balance.Enabled = true;
                        tableLayoutPanel_balance.Visible = true;
                        textbox_balance.Text = item_1.Quantity.ToString();
                        itemCodeCheck = item_1.ItemNo;
                    }

                    else if (itemCodeCheck == item_1.ItemNo || comboBox_NatureOfWork.SelectedItem.ToString() == "Item Repair")
                    {

                    }

                    else
                    {
                        MessageBox.Show("Set Nature of Work first!", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textbox_itemCode.Text = "";
                        textbox_itemName.Text = "";
                    }

                }

                else
                {
                    tableLayoutPanel_balance.Enabled = false;
                    tableLayoutPanel_balance.Visible = false;
                    textbox_itemName.Text = "Item not found!";

                }
            }
        }

        private void textbox_FolioNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex regex = new Regex(@"^[\d]+$");

            // Check if the pressed key matches the pattern
            if (!regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void textbox_itemQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex regex = new Regex(@"^[\d]+$");

            // Check if the pressed key matches the pattern
            if (!regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void textBox_issuingOfficer_Leave(object sender, EventArgs e)
        {
            Guna2TextBox textBox = (Guna2TextBox)sender;

            string originalText = textBox.Text;
            string capitalizedText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(originalText);

            textBox.Text = capitalizedText;
        }

        private void textbox_Receiver_Leave(object sender, EventArgs e)
        {
            Guna2TextBox textBox = (Guna2TextBox)sender;

            string originalText = textBox.Text;
            string capitalizedText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(originalText);

            textBox.Text = capitalizedText;
        }

        private void textbox_receiverDesig_Leave(object sender, EventArgs e)
        {
            //Guna2TextBox textBox = (Guna2TextBox)sender;

            //string originalText = textBox.Text;
            //string capitalizedText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(originalText);

            //textBox.Text = capitalizedText;
        }

        private void textbox_receivedBy_Leave(object sender, EventArgs e)
        {
            Guna2TextBox textBox = (Guna2TextBox)sender;

            string originalText = textBox.Text;
            string capitalizedText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(originalText);

            textBox.Text = capitalizedText;
        }

        private void btn_requisit_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            if (!string.IsNullOrEmpty(textbox_FolioNo.Text) && !string.IsNullOrEmpty(textbox_officeCode.Text) && !string.IsNullOrEmpty(textbox_officeName.Text) && !string.IsNullOrEmpty(textbox_itemCode.Text) && !string.IsNullOrEmpty(textbox_itemName.Text) && !string.IsNullOrEmpty(textbox_itemQuantity.Text) && !string.IsNullOrEmpty(textbox_receivedBy.Text) && !string.IsNullOrEmpty(textbox_Receiver.Text) && !string.IsNullOrEmpty(textbox_receiverDesig.Text) && !string.IsNullOrEmpty(textBox_issuingOfficer.Text) && comboBox_approvedBy.SelectedItem != null && comboBox_NatureOfWork.SelectedItem != null && Req_form != null)
            {
                if (comboBox_NatureOfWork.SelectedItem.ToString() == "Item Issue")
                {
                    int folioNo = int.Parse(textbox_FolioNo.Text);
                    int itemQuantity = int.Parse(textbox_itemQuantity.Text);

                    if (textbox_itemName.Text != "Item not found!" && textbox_officeCode.Text != "Office not found!" && folioNo > 0 && itemQuantity > 0)
                    {
                        SqlCommand cmd = new SqlCommand(string.Concat("select id from Requisition where FolioNo = ", folioNo.ToString(), " and Year(Requisition_Date) = ", date_requisition.Value.Year.ToString()), con);
                        object val = cmd.ExecuteScalar();

                        if (val == null)
                        {

                            SqlCommand co = new SqlCommand(string.Concat("select id from Inventory where ItemNo = ", textbox_itemCode.Text, " and Item_Description = '", textbox_itemName.Text, "'"), con);
                            InventoryBL item_1 = InventoryDL.getItemFromInventoryTable((int)co.ExecuteScalar());

                            if (item_1.Quantity >= itemQuantity)
                            {
                                SqlCommand cdd = new SqlCommand(string.Concat("Insert into Remaining (Id,rQuan,Date_rQuan) values (", item_1.Id, ",", item_1.Quantity, ",@date)"), con);
                                cdd.Parameters.AddWithValue("@date", DateTime.Now);
                                cdd.ExecuteNonQuery();

                                SqlCommand cmdd = new SqlCommand(string.Concat("insert into Requisition (FolioNo,OfficeId,Nature_of_Work,ItemId,Quantity,Requisition_Date,Approved_By,Issuing_officer,Receiver,Receiver_Designation,Received_By,Receving_Date,Requisit_By,DED,rForm) values (", folioNo, ",(select id from Office where Office_Code = ", textbox_officeCode.Text, " and office_Add = '", textbox_officeName.Text, "' and isDeleted != 1),(select id from [Lookup] where Category = 'NATURE_OF_WORK' and [Value] = '", comboBox_NatureOfWork.SelectedItem.ToString(), "'),(select id from Inventory where ItemNo = ", textbox_itemCode.Text, " and Item_Description = '", textbox_itemName.Text, "'),@quantity , @ReqDate, (select id from [lookup] where [value] = '", comboBox_approvedBy.SelectedItem.ToString(), "' and Category = 'APPROVER'),@IO ,@Rece, @RD, @RB, @RDate,@Requisit_By,@DED,@rForm)"), con);
                                cmdd.Parameters.AddWithValue("@quantity", itemQuantity);
                                cmdd.Parameters.AddWithValue("@ReqDate", date_requisition.Value.Date);
                                cmdd.Parameters.AddWithValue("@IO", textBox_issuingOfficer.Text);
                                cmdd.Parameters.AddWithValue("@Rece", textbox_Receiver.Text);
                                cmdd.Parameters.AddWithValue("@RD", textbox_receiverDesig.Text);
                                cmdd.Parameters.AddWithValue("@RB", textbox_receivedBy.Text);
                                cmdd.Parameters.AddWithValue("@RDate", date_Receive.Value.Date);
                                cmdd.Parameters.AddWithValue("@Requisit_By", user.ID);
                                cmdd.Parameters.AddWithValue("@DED", DateTime.Now);
                                cmdd.Parameters.AddWithValue("@rForm", Req_form);
                                cmdd.ExecuteNonQuery();

                                SqlCommand c = new SqlCommand(string.Concat("update Inventory set Quantity = @Quantity, Out_of_Stock = @Out_of_Stock where id = ", item_1.Id), con);
                                c.Parameters.AddWithValue("@Quantity", item_1.Quantity - itemQuantity);
                                if (item_1.Quantity - itemQuantity == 0)
                                    c.Parameters.AddWithValue("@Out_of_Stock", 1);
                                else
                                    c.Parameters.AddWithValue("@Out_of_Stock", 0);
                                c.ExecuteNonQuery();

                                SqlCommand cd = new SqlCommand(string.Concat("Insert into Remaining (Id,rQuan,Date_rQuan) values (", item_1.Id, ",", item_1.Quantity - itemQuantity, ",@date)"), con);
                                cd.Parameters.AddWithValue("@date", DateTime.Now);
                                cd.ExecuteNonQuery();



                                MessageBox.Show("Requisition Added! \n\nYou can update this record within 3 days!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                textbox_FolioNo.Text = "";
                                textbox_officeCode.Text = string.Empty;
                                textbox_officeName.Text = string.Empty;
                                textbox_itemCode.Text = string.Empty;
                                textbox_itemName.Text = string.Empty;
                                textbox_Receiver.Text = string.Empty;
                                textbox_receiverDesig.Text = string.Empty;
                                textbox_repair.Text = string.Empty;
                                textBox_issuingOfficer.Text = string.Empty;
                                textbox_receivedBy.Text = string.Empty;
                                comboBox_approvedBy.SelectedItem = null;
                                comboBox_NatureOfWork.SelectedItem = null;
                                date_Receive.Value = DateTime.Today;
                                date_requisition.Value = DateTime.Today;
                                textbox_itemQuantity.Text = string.Empty;
                                label5.Enabled = false;
                                label5.Visible = false;
                                textbox_repair.Enabled = false;
                                textbox_repair.Visible = false;
                                tableLayoutPanel_balance.Enabled = false;
                                tableLayoutPanel_balance.Visible = false;

                            }

                            else
                                MessageBox.Show("Qunatity unavailable!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        }

                        else
                        {
                            MessageBox.Show("Folio No '" + folioNo + "' in the year " + date_requisition.Value.Year.ToString() + " already Exists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            textbox_FolioNo.Text = "";
                        }
                    }

                    else
                    {
                        MessageBox.Show("Invalid Data Entered!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }

                else if (comboBox_NatureOfWork.SelectedItem.ToString() == "Item Repair" && !string.IsNullOrEmpty(textbox_repair.Text))
                {
                    int folioNo = int.Parse(textbox_FolioNo.Text);
                    int itemQuantity = int.Parse(textbox_itemQuantity.Text);

                    if (textbox_itemName.Text != "Item not found!" && textbox_officeCode.Text != "Office not found!" && folioNo > 0 && itemQuantity > 0)
                    {
                        SqlCommand cmd = new SqlCommand(string.Concat("select id from Requisition where FolioNo = ", folioNo.ToString(), " and Year(Requisition_Date) = ", date_requisition.Value.Year.ToString()), con);
                        object val = cmd.ExecuteScalar();

                        if (val == null)
                        {
                            SqlCommand cmdd = new SqlCommand(string.Concat("insert into Requisition (FolioNo,OfficeId,Nature_of_Work,ItemId,Quantity,Requisition_Date,Approved_By,Issuing_officer,Receiver,Receiver_Designation,Received_By,Receving_Date,Requisit_By,DED,rForm) values (", folioNo, ",(select id from Office where Office_Code = ", textbox_officeCode.Text, " and office_Add = '", textbox_officeName.Text, "' and isDeleted != 1),(select id from [Lookup] where Category = 'NATURE_OF_WORK' and [Value] = '", comboBox_NatureOfWork.SelectedItem.ToString(), "'),(select id from Inventory where ItemNo = ", textbox_itemCode.Text, " and Item_Description = '", textbox_itemName.Text, "'),@quantity , @ReqDate, (select id from [lookup] where [value] = '", comboBox_approvedBy.SelectedItem.ToString(), "' and Category = 'APPROVER'),@IO ,@Rece, @RD, @RB, @RDate,@Requisit_By,@DED,@rForm)"), con);
                            cmdd.Parameters.AddWithValue("@quantity", itemQuantity);
                            cmdd.Parameters.AddWithValue("@ReqDate", date_requisition.Value.Date);
                            cmdd.Parameters.AddWithValue("@IO", textBox_issuingOfficer.Text);
                            cmdd.Parameters.AddWithValue("@Rece", textbox_Receiver.Text);
                            cmdd.Parameters.AddWithValue("@RD", textbox_receiverDesig.Text);
                            cmdd.Parameters.AddWithValue("@RB", textbox_receivedBy.Text);
                            cmdd.Parameters.AddWithValue("@RDate", date_Receive.Value.Date);
                            cmdd.Parameters.AddWithValue("@Requisit_By", user.ID);
                            cmdd.Parameters.AddWithValue("@DED", DateTime.Now);
                            cmdd.Parameters.AddWithValue("@rForm", Req_form);
                            cmdd.ExecuteNonQuery();

                            SqlCommand cm = new SqlCommand("select Max(Id) from Requisition", con);
                            int reqId = (int)cm.ExecuteScalar();

                            SqlCommand comand = new SqlCommand(string.Concat("Insert into Repair (Id,Reason_Repair) values (", reqId.ToString(), ", '", textbox_repair.Text, "')"), con);
                            comand.ExecuteNonQuery();

                            MessageBox.Show("Requisition Added! \n\nYou can update this record within 3 days!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            textbox_FolioNo.Text = "";
                            textbox_officeCode.Text = string.Empty;
                            textbox_officeName.Text = string.Empty;
                            textbox_itemCode.Text = string.Empty;
                            textbox_itemName.Text = string.Empty;
                            textbox_Receiver.Text = string.Empty;
                            textbox_receiverDesig.Text = string.Empty;
                            textbox_repair.Text = string.Empty;
                            textBox_issuingOfficer.Text = string.Empty;
                            textbox_receivedBy.Text = string.Empty;
                            comboBox_approvedBy.SelectedItem = null;
                            comboBox_NatureOfWork.SelectedItem = null;
                            date_Receive.Value = DateTime.Today;
                            date_requisition.Value = DateTime.Today;
                            textbox_itemQuantity.Text = string.Empty;
                            label5.Enabled = false;
                            label5.Visible = false;
                            textbox_repair.Enabled = false;
                            textbox_repair.Visible = false;
                            tableLayoutPanel_rr.Enabled = false;
                            tableLayoutPanel_rr.Visible = false;
                            tableLayoutPanel_balance.Enabled = false;
                            tableLayoutPanel_balance.Visible = false;
                        }

                        else
                        {
                            MessageBox.Show("Folio No '" + folioNo + "' in the year " + date_requisition.Value.Year.ToString() + " already Exists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            textbox_FolioNo.Text = "";
                        }
                    }

                    else
                    {
                        MessageBox.Show("Invalid Data Entered!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }

            else
            {
                MessageBox.Show("Required Data section should not be empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void tableLayoutPanel3_SizeChanged(object sender, EventArgs e)
        {
            if (tableLayoutPanel3.Width > 1222 && tableLayoutPanel3.Height > 574)
            {
                tableLayoutPanel3.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 23.0f);
                tableLayoutPanel3.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 54.0f);
                tableLayoutPanel3.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 23.0f);
            }

            else if (tableLayoutPanel3.Width <= 1222 && tableLayoutPanel3.Height <= 574)
            {
                tableLayoutPanel3.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 15.0f);
                tableLayoutPanel3.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 70.0f);
                tableLayoutPanel3.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 15.0f);
            }
        }

        private void linkLabel_choose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string pdfLocation = "";
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Pdf File|*.pdf";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pdfLocation = openFileDialog.FileName;

                    if(File.Exists(pdfLocation))
                    {
                        Req_form = File.ReadAllBytes(pdfLocation);
                        MessageBox.Show("Pdf file selected!","information",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void linkLabel_view_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(Req_form != null)
            {
                FormView nf = new FormView(Req_form);
                nf.ShowDialog();
            }
            else
                MessageBox.Show("Pdf file isn't selected!", "information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
