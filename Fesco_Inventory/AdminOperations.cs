using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fesco_Inventory.BL;

namespace Fesco_Inventory
{
    public partial class AdminOperations : UserControl
    {
        Panel pan;
        userBL user;
        Label label;
        public AdminOperations(Panel pan, userBL user, Label label)
        {
            InitializeComponent();
            this.pan = pan;
            this.user = user;
            this.label = label;
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Do You want to Logout?", "Confirmation Box", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                login uc = new login(pan, null, null, false, label);
                getUC(uc);
            }
        }

        private void getUC(UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            pan.Controls.Clear();
            pan.Controls.Add(uc);
            uc.BringToFront();
        }

        private void AdminOperations_Load(object sender, EventArgs e)
        {
            //linkLabel_Profile.Text = "Admin Profile";
            label.Text = "Hardware Lab Management";
        }

        private void btn_inventory_Click(object sender, EventArgs e)
        {
            InventryItem_AdminSup uc = new InventryItem_AdminSup(pan, user, label);
            getUC(uc);
        }

        private void btn_userManagement_Click(object sender, EventArgs e)
        {
            userManagement uc = new userManagement(pan, user, label);
            getUC(uc);
        }

        private void btn_profile_Click(object sender, EventArgs e)
        {
            profile uc = new profile(pan, user, label);
            getUC(uc);
        }

        private void btn_bucket_Click_1(object sender, EventArgs e)
        {
            Bucket uc = new Bucket(pan,user, label);
            getUC(uc);
        }

        private void btn_Offices_Click(object sender, EventArgs e)
        {
            Office uc = new Office(pan, user, label);
            getUC(uc);
        }

        private void btn_requisition_Click_1(object sender, EventArgs e)
        {
            AllocateItem uc = new AllocateItem(pan, user, label);
            getUC(uc);
        }
    }
}
