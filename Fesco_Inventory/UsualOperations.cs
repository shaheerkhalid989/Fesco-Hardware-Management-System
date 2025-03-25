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

namespace Fesco_Inventory
{
    
    public partial class UsualOperations : UserControl
    {
        userBL user;
        Panel pan;
        Label label;

        public UsualOperations(Panel pan, userBL user, Label label)
        {
            InitializeComponent();
            this.user = user;
            this.pan = pan;
            this.label = label;
        }

        private void getUC(UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            pan.Controls.Clear();
            pan.Controls.Add(uc);
            uc.BringToFront();
        }

        private void UsualOperations_Load(object sender, EventArgs e)
        {
            label.Text = "Hardware Lab Management";
            if(getSupervisor() == "Operator")
            {
                btn_bucket.Enabled = false;
                btn_bucket.Visible = false;

                btn_Offices.Enabled = false;
                btn_Offices.Visible = false;
            }
        }

        string getSupervisor()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmdd = new SqlCommand(string.Concat("Select [Value] from Lookup where Id = ", user.Role, " and Category = 'ROLE'"), con);
            string role = (string)cmdd.ExecuteScalar();
            return role;
        }
        

        private void btn_logout_Click_1(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Do You want to Logout?", "Confirmation Box", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                login uc = new login(pan, null, null, false, label);
                getUC(uc);
            }
        }

        private void btn_inventory_Click_1(object sender, EventArgs e)
        {
                InventryItem_AdminSup UC = new InventryItem_AdminSup(pan, user, label);
                getUC(UC);
        }

        private void btn_Requsition_Click(object sender, EventArgs e)
        {
                AllocateItem uc = new AllocateItem(pan, user, label);
                getUC(uc);
        }

        private void btn_profile_Click(object sender, EventArgs e)
        {
            profile uc = new profile(pan, user, label);
            getUC(uc);
        }

        private void btn_Offices_Click(object sender, EventArgs e)
        {
            Office uc = new Office(pan, user, label);
            getUC(uc);
        }

        private void btn_bucket_Click_1(object sender, EventArgs e)
        {
            Bucket uc = new Bucket(pan, user, label);
            getUC(uc);
        }
    }
}
