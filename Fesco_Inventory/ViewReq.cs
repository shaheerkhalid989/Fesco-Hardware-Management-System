using Fesco_Inventory.BL;
using Fesco_Inventory.DL;
using Guna.UI2.WinForms;
using Org.BouncyCastle.Ocsp;
using System;
using System.CodeDom.Compiler;
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

namespace Fesco_Inventory
{
    public partial class ViewReq : Form
    {
        RequisitionBL req;
        InventoryBL item2;
        int id_;

        int itemCodeCheck = 0;
        public ViewReq( int id_)
        {
            InitializeComponent();
            this.id_ = id_;
        }

        private void ViewReq_Load(object sender, EventArgs e)
        {
            req = RequisitionDL.getRequisitionfromReqTable(id_);
            var con = Configuration.getInstance().getConnection();
            SqlCommand sql = new SqlCommand(string.Concat("select value from [Lookup] where id = ",req.Now," and Category = 'NATURE_OF_WORK'"),con);
            comboBox_NatureOfWork.SelectedItem = (string)sql.ExecuteScalar();
            if(comboBox_NatureOfWork.SelectedItem.ToString() == "Item Repair")
            {
                tableLayoutPanel_balance.Visible = false;
                tableLayoutPanel_balance.Enabled = false;
                
            }

            else
            {
                tableLayoutPanel_balance.Visible = true;
                tableLayoutPanel_balance.Enabled = true;
            }
            SqlCommand sql6 = new SqlCommand(string.Concat("Select Reason_Repair from Repair where id = ",req.Id),con);
            textbox_repair.Text = (string)sql6.ExecuteScalar();
            textbox_FolioNo.Text = req.FolioNo.ToString();
            item2 = InventoryDL.getItemFromInventoryTable(req.ItemId);
            textbox_itemCode.Text = item2.ItemNo.ToString();
            textbox_itemName.Text = item2.ItemDesc.ToString();
            textbox_balance.Text = item2.Quantity.ToString();
            textbox_itemQuantity.Text = req.Quantity.ToString();
            SqlCommand sql2 = new SqlCommand(string.Concat("select Office_Code from Office where id = ",req.OfficeId),con);
            
            int oc = (int)sql2.ExecuteScalar();
            textbox_officeCode.Text = oc.ToString();
            SqlCommand sql3 = new SqlCommand(string.Concat("select office_Add from Office where id = ", req.OfficeId), con);
            textbox_officeName.Text = (string)sql3.ExecuteScalar();
            date_requisition.Value = req.ReqD.Date;
            textBox_issuingOfficer.Text = req.IO;
            textbox_Receiver.Text = req.Receiver;
            textbox_receiverDesig.Text = req.Rece_Desig;
            textbox_receivedBy.Text = req.Rece_By;
            date_Receive.Value = req.Rece_Date.Date;
            SqlCommand sql4 = new SqlCommand(string.Concat("select value from [Lookup] where id = ", req.Approver, " and Category = 'APPROVER'"), con);
            comboBox_approvedBy.SelectedItem = (string)sql4.ExecuteScalar();
            try
            {
                using (MemoryStream memoryStream = new MemoryStream(req.RForm_pdf))
                {
                    pdfViewer1.LoadFromStream(memoryStream);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            tableLayoutPanel_now.Visible = false;
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

        private void textbox_itemCode_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textbox_officeCode_KeyPress(object sender, KeyPressEventArgs e)
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

                    if (File.Exists(pdfLocation))
                    {
                        req.RForm_pdf = File.ReadAllBytes(pdfLocation);
                        MessageBox.Show("Pdf file selected!", "information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

            try
            {
                using (MemoryStream memoryStream = new MemoryStream(req.RForm_pdf))
                {
                    pdfViewer1.LoadFromStream(memoryStream);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void textbox_receivedBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Guna2TextBox textBox = (Guna2TextBox)sender;

            //string originalText = textBox.Text;
            //string capitalizedText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(originalText);

            //textBox.Text = capitalizedText;
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

                    var con = Configuration.getInstance().getConnection();
                    SqlCommand sql = new SqlCommand(string.Concat("select id from lookup where category = 'NATURE_OF_WORK' and value = '",comboBox_NatureOfWork.SelectedItem.ToString(),"'"),con);
                    if (req.Now == (int)sql.ExecuteScalar())
                        textbox_itemQuantity.Text = req.Quantity.ToString();
                    else
                        textbox_itemQuantity.Text = "";

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

                    var con = Configuration.getInstance().getConnection();
                    SqlCommand sql = new SqlCommand(string.Concat("select id from lookup where category = 'NATURE_OF_WORK' and value = '", comboBox_NatureOfWork.SelectedItem.ToString(), "'"), con);
                    if (req.Now == (int)sql.ExecuteScalar())
                        textbox_itemQuantity.Text = req.Quantity.ToString();
                    else
                    {
                        textbox_itemQuantity.Text = "";
                    }
                }
            }
        }

        private void textbox_itemQuantity_Click(object sender, EventArgs e)
        {
            if (textbox_itemName.Text != "" && textbox_itemName.Text != "Item not found!" && comboBox_NatureOfWork.SelectedItem.ToString() == "Item Issue")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand co = new SqlCommand(string.Concat("select id from Inventory where ItemNo = ", textbox_itemCode.Text, " and Item_Description = '", textbox_itemName.Text, "'"), con);
                InventoryBL item_1 = InventoryDL.getItemFromInventoryTable((int)co.ExecuteScalar());
            
                tableLayoutPanel_balance.Enabled = true;
                tableLayoutPanel_balance.Visible = true;
                textbox_balance.Text = item_1.Quantity.ToString();
            }
        }

        private void textbox_itemName_TextChanged(object sender, EventArgs e)
        {
            if (textbox_itemName.Text != "Item not found!")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand co = new SqlCommand(string.Concat("select id from Inventory where ItemNo = ", textbox_itemCode.Text, " and Item_Description = '", textbox_itemName.Text, "'"), con);
                InventoryBL item_1 = InventoryDL.getItemFromInventoryTable((int)co.ExecuteScalar());
                textbox_balance.Text = item_1.Quantity.ToString();
            }
        }

        private void btn_applyChanges_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            if (!string.IsNullOrEmpty(textbox_FolioNo.Text) && !string.IsNullOrEmpty(textbox_officeCode.Text) && !string.IsNullOrEmpty(textbox_officeName.Text) && !string.IsNullOrEmpty(textbox_itemCode.Text) && !string.IsNullOrEmpty(textbox_itemName.Text) && !string.IsNullOrEmpty(textbox_itemQuantity.Text) && !string.IsNullOrEmpty(textbox_receivedBy.Text) && !string.IsNullOrEmpty(textbox_Receiver.Text) && !string.IsNullOrEmpty(textbox_receiverDesig.Text) && !string.IsNullOrEmpty(textBox_issuingOfficer.Text) && comboBox_approvedBy.SelectedItem != null && comboBox_NatureOfWork.SelectedItem != null)
            {
                if (comboBox_NatureOfWork.SelectedItem.ToString() == "Item Issue")
                {
                    int folioNo = int.Parse(textbox_FolioNo.Text);
                    int itemQuantity = int.Parse(textbox_itemQuantity.Text);

                    if (textbox_itemName.Text != "Item not found!" && textbox_officeCode.Text != "Office not found!" && folioNo > 0 && itemQuantity > 0)
                    {
                        SqlCommand cmd = new SqlCommand(string.Concat("select id from Requisition where FolioNo = ", folioNo.ToString(), " and Year(Requisition_Date) = ", date_requisition.Value.Year.ToString()," and id != ",req.Id), con);
                        object val = cmd.ExecuteScalar();

                        

                        if (val == null)
                        {

                            SqlCommand co = new SqlCommand(string.Concat("select id from Inventory where ItemNo = ", textbox_itemCode.Text, " and Item_Description = '", textbox_itemName.Text, "'"), con);
                            InventoryBL item_1 = InventoryDL.getItemFromInventoryTable((int)co.ExecuteScalar());

                            if (item_1.Id == item2.Id)
                            {

                                if (itemQuantity > req.Quantity)
                                {
                                    if (itemQuantity - req.Quantity <= item_1.Quantity)
                                    {
                                        SqlCommand cdd = new SqlCommand(string.Concat("Insert into Remaining (Id,rQuan,Date_rQuan) values (", item_1.Id, ",", item_1.Quantity, ",@date)"), con);
                                        cdd.Parameters.AddWithValue("@date", DateTime.Now);
                                        cdd.ExecuteNonQuery();

                                        SqlCommand upd = new SqlCommand(string.Concat("Update Requisition set FolioNo = @1, OfficeId = (select id from Office where Office_Code = ", textbox_officeCode.Text, " and office_Add = '", textbox_officeName.Text, "' and isDeleted != 1), ItemId = @3, Quantity = @4, Requisition_Date = @5, Approved_By = (select id from [lookup] where [value] = '", comboBox_approvedBy.SelectedItem.ToString(), "' and Category = 'APPROVER'), Issuing_officer = @7, Receiver = @8, Receiver_Designation = @9, Received_By = @10, Receving_Date = @11, rForm = @rForm, Updated = 1 where id = ", req.Id), con);
                                        upd.Parameters.AddWithValue("@1", folioNo);
                                        upd.Parameters.AddWithValue("@3", item_1.Id);
                                        upd.Parameters.AddWithValue("@4", itemQuantity);
                                        upd.Parameters.AddWithValue("@5", date_requisition.Value);
                                        upd.Parameters.AddWithValue("@7", textBox_issuingOfficer.Text);
                                        upd.Parameters.AddWithValue("@8", textbox_Receiver.Text);
                                        upd.Parameters.AddWithValue("@9", textbox_receiverDesig.Text);
                                        upd.Parameters.AddWithValue("@10", textbox_receivedBy.Text);
                                        upd.Parameters.AddWithValue("@11", date_Receive.Value);
                                        upd.Parameters.AddWithValue("@rForm", req.RForm_pdf);
                                        upd.ExecuteNonQuery();

                                        SqlCommand c = new SqlCommand(string.Concat("update Inventory set Quantity = @Quantity, Out_of_Stock = @Out_of_Stock where id = ", item_1.Id), con);
                                        c.Parameters.AddWithValue("@Quantity", item_1.Quantity - (itemQuantity- req.Quantity));
                                        if (item_1.Quantity - (itemQuantity - req.Quantity) == 0)
                                            c.Parameters.AddWithValue("@Out_of_Stock", 1);
                                        else
                                            c.Parameters.AddWithValue("@Out_of_Stock", 0);
                                        c.ExecuteNonQuery();

                                        SqlCommand cd = new SqlCommand(string.Concat("Insert into Remaining (Id,rQuan,Date_rQuan) values (", item_1.Id, ",", item_1.Quantity - (itemQuantity - req.Quantity), ",@date)"), con);
                                        cd.Parameters.AddWithValue("@date", DateTime.Now);
                                        cd.ExecuteNonQuery();

                                        MessageBox.Show("Changes Applied", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        this.Close();
                                    }

                                    else
                                        MessageBox.Show("Qunatity unavailable!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                }

                                else if (itemQuantity == req.Quantity)
                                {
                                    SqlCommand upd = new SqlCommand(string.Concat("Update Requisition set FolioNo = @1, OfficeId = (select id from Office where Office_Code = ", textbox_officeCode.Text, " and office_Add = '", textbox_officeName.Text, "' and isDeleted != 1), ItemId = @3, Quantity = @4, Requisition_Date = @5, Approved_By = (select id from [lookup] where [value] = '", comboBox_approvedBy.SelectedItem.ToString(), "' and Category = 'APPROVER'), Issuing_officer = @7, Receiver = @8, Receiver_Designation = @9, Received_By = @10, Receving_Date = @11, rForm = @rForm, Updated = 1 where id = ", req.Id), con);
                                    upd.Parameters.AddWithValue("@1", folioNo);
                                    upd.Parameters.AddWithValue("@3", item_1.Id);
                                    upd.Parameters.AddWithValue("@4", itemQuantity);
                                    upd.Parameters.AddWithValue("@5", date_requisition.Value);
                                    upd.Parameters.AddWithValue("@7", textBox_issuingOfficer.Text);
                                    upd.Parameters.AddWithValue("@8", textbox_Receiver.Text);
                                    upd.Parameters.AddWithValue("@9", textbox_receiverDesig.Text);
                                    upd.Parameters.AddWithValue("@10", textbox_receivedBy.Text);
                                    upd.Parameters.AddWithValue("@11", date_Receive.Value);
                                    upd.Parameters.AddWithValue("@rForm",req.RForm_pdf);
                                    upd.ExecuteNonQuery();

                                    MessageBox.Show("Changes Applied", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    this.Close();
                                }

                                else
                                {
                                    SqlCommand upd = new SqlCommand(string.Concat("Update Requisition set FolioNo = @1, OfficeId = (select id from Office where Office_Code = ", textbox_officeCode.Text, " and office_Add = '", textbox_officeName.Text, "' and isDeleted != 1), ItemId = @3, Quantity = @4, Requisition_Date = @5, Approved_By = (select id from [lookup] where [value] = '", comboBox_approvedBy.SelectedItem.ToString(), "' and Category = 'APPROVER'), Issuing_officer = @7, Receiver = @8, Receiver_Designation = @9, Received_By = @10, Receving_Date = @11, rForm = @rForm, Updated = 1 where id = ", req.Id), con);
                                    upd.Parameters.AddWithValue("@1", folioNo);
                                    upd.Parameters.AddWithValue("@3", item_1.Id);
                                    upd.Parameters.AddWithValue("@4", itemQuantity);
                                    upd.Parameters.AddWithValue("@5", date_requisition.Value);
                                    upd.Parameters.AddWithValue("@7", textBox_issuingOfficer.Text);
                                    upd.Parameters.AddWithValue("@8", textbox_Receiver.Text);
                                    upd.Parameters.AddWithValue("@9", textbox_receiverDesig.Text);
                                    upd.Parameters.AddWithValue("@10", textbox_receivedBy.Text);
                                    upd.Parameters.AddWithValue("@11", date_Receive.Value);
                                    upd.Parameters.AddWithValue("@rForm", req.RForm_pdf);
                                    upd.ExecuteNonQuery();

                                    SqlCommand c = new SqlCommand(string.Concat("update Inventory set Quantity = @Quantity, Out_of_Stock = @Out_of_Stock where id = ", item_1.Id), con);
                                    c.Parameters.AddWithValue("@Quantity", item_1.Quantity + (  req.Quantity - itemQuantity));
                                    c.Parameters.AddWithValue("@Out_of_Stock", 0);
                                    c.ExecuteNonQuery();

                                    SqlCommand cd = new SqlCommand(string.Concat("Insert into Remaining (Id,rQuan,Date_rQuan) values (", item_1.Id, ",", item_1.Quantity + (req.Quantity - itemQuantity), ",@date)"), con);
                                    cd.Parameters.AddWithValue("@date", DateTime.Now);
                                    cd.ExecuteNonQuery();

                                    MessageBox.Show("Changes Applied", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    this.Close();
                                }
                            }

                            else
                            {
                                if(item_1.Quantity >= itemQuantity)
                                {
                                    SqlCommand c_ = new SqlCommand(string.Concat("update Inventory set Quantity = @Quantity, Out_of_Stock = @Out_of_Stock where id = ", item2.Id), con);
                                    c_.Parameters.AddWithValue("@Quantity", item2.Quantity + req.Quantity);
                                    c_.Parameters.AddWithValue("@Out_of_Stock", 0);
                                    c_.ExecuteNonQuery();

                                    SqlCommand cd_ = new SqlCommand(string.Concat("Insert into Remaining (Id,rQuan,Date_rQuan) values (", item2.Id, ",", item2.Quantity + req.Quantity, ",@date)"), con);
                                    cd_.Parameters.AddWithValue("@date", DateTime.Now);
                                    cd_.ExecuteNonQuery();

                                    //------------------------------------------------------------------------------------------------------------------------------

                                    SqlCommand cdd = new SqlCommand(string.Concat("Insert into Remaining (Id,rQuan,Date_rQuan) values (", item_1.Id, ",", item_1.Quantity, ",@date)"), con);
                                    cdd.Parameters.AddWithValue("@date", DateTime.Now);
                                    cdd.ExecuteNonQuery();

                                    SqlCommand upd = new SqlCommand(string.Concat("Update Requisition set FolioNo = @1, OfficeId = (select id from Office where Office_Code = ", textbox_officeCode.Text, " and office_Add = '", textbox_officeName.Text, "' and isDeleted != 1), ItemId = @3, Quantity = @4, Requisition_Date = @5, Approved_By = (select id from [lookup] where [value] = '", comboBox_approvedBy.SelectedItem.ToString(), "' and Category = 'APPROVER'), Issuing_officer = @7, Receiver = @8, Receiver_Designation = @9, Received_By = @10, Receving_Date = @11, rForm = @rForm, Updated = 1 where id = ", req.Id), con);
                                    upd.Parameters.AddWithValue("@1", folioNo);
                                    upd.Parameters.AddWithValue("@3", item_1.Id);
                                    upd.Parameters.AddWithValue("@4", itemQuantity);
                                    upd.Parameters.AddWithValue("@5", date_requisition.Value);
                                    upd.Parameters.AddWithValue("@7", textBox_issuingOfficer.Text);
                                    upd.Parameters.AddWithValue("@8", textbox_Receiver.Text);
                                    upd.Parameters.AddWithValue("@9", textbox_receiverDesig.Text);
                                    upd.Parameters.AddWithValue("@10", textbox_receivedBy.Text);
                                    upd.Parameters.AddWithValue("@11", date_Receive.Value);
                                    upd.Parameters.AddWithValue("@rForm", req.RForm_pdf );
                                    upd.ExecuteNonQuery();

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

                                    MessageBox.Show("Changes Applied", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    this.Close();
                                }

                                else
                                    MessageBox.Show("Qunatity unavailable!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            }

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
                        SqlCommand cmd = new SqlCommand(string.Concat("select id from Requisition where FolioNo = ", folioNo.ToString(), " and Year(Requisition_Date) = ", date_requisition.Value.Year.ToString(), " and id != ", req.Id), con);
                        object val = cmd.ExecuteScalar();

                        if (val == null)
                        {

                            SqlCommand co = new SqlCommand(string.Concat("select id from Inventory where ItemNo = ", textbox_itemCode.Text, " and Item_Description = '", textbox_itemName.Text, "'"), con);
                            InventoryBL item_1 = InventoryDL.getItemFromInventoryTable((int)co.ExecuteScalar());

                            SqlCommand upd = new SqlCommand(string.Concat("Update Requisition set FolioNo = @1, OfficeId = (select id from Office where Office_Code = ", textbox_officeCode.Text, " and office_Add = '", textbox_officeName.Text, "' and isDeleted != 1), ItemId = @3, Quantity = @4, Requisition_Date = @5, Approved_By = (select id from [lookup] where [value] = '", comboBox_approvedBy.SelectedItem.ToString(), "' and Category = 'APPROVER'), Issuing_officer = @7, Receiver = @8, Receiver_Designation = @9, Received_By = @10, Receving_Date = @11, rForm = @rForm, Updated = 1 where id = ", req.Id), con);
                            upd.Parameters.AddWithValue("@1", folioNo);
                            upd.Parameters.AddWithValue("@3", item_1.Id);
                            upd.Parameters.AddWithValue("@4", itemQuantity);
                            upd.Parameters.AddWithValue("@5", date_requisition.Value);
                            upd.Parameters.AddWithValue("@7", textBox_issuingOfficer.Text);
                            upd.Parameters.AddWithValue("@8", textbox_Receiver.Text);
                            upd.Parameters.AddWithValue("@9", textbox_receiverDesig.Text);
                            upd.Parameters.AddWithValue("@10", textbox_receivedBy.Text);
                            upd.Parameters.AddWithValue("@11", date_Receive.Value);
                            upd.Parameters.AddWithValue("@rForm", req.RForm_pdf);
                            upd.ExecuteNonQuery();

                            SqlCommand comand = new SqlCommand(string.Concat("Update Repair set Reason_Repair =  '",textbox_repair.Text, "' where id = ",req.Id), con);
                            comand.ExecuteNonQuery();

                            MessageBox.Show("Changes Applied", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.Close();
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

        private void textbox_receivedBy_Leave(object sender, EventArgs e)
        {
            Guna2TextBox textBox = (Guna2TextBox)sender;

            string originalText = textBox.Text;
            string capitalizedText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(originalText);

            textBox.Text = capitalizedText;
        }
    }
}
