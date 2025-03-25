using Fesco_Inventory.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fesco_Inventory
{
    public partial class Loading : UserControl
    {
        int size;
        Panel pan;
        userBL user;
        string RoleValue;
        Label label;

        public Loading(Panel pan,userBL user, string RoleValue,Label label)
        {
            InitializeComponent();
            this.pan = pan;
            this.RoleValue = RoleValue;
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

        private void Loading_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
            //size = (tableLayoutPanel1.Width - 10) / 7;
            if (tableLayoutPanel2.Width > 1222) size = 8;
            else size = 4;
            label_loading.Text = "Loading";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel_Bar.Width = panel_Bar.Width + size;
            if (panel_Bar.Width >= (tableLayoutPanel1.Width - 10))
            {
                timer1.Stop();
                timer2.Stop();
                if (user.Isadmin == true && RoleValue == "Supervisor")
                {
                    AdminOperations uc = new AdminOperations(pan, user, label);
                    getUC(uc);
                }

                else if (user.Isadmin == false && RoleValue == "Supervisor")
                {
                    UsualOperations uc = new UsualOperations(pan, user, label);
                    getUC(uc);
                }

                else
                {
                    UsualOperations uc = new UsualOperations(pan, user, label);
                    getUC(uc);
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (label_loading.Text == "Loading...")
            {
                label_loading.Text = "Loading";
            }
            else
            {

                if (label_loading.Text == "Loading" || label_loading.Text == "Loading." || label_loading.Text == "Loading..")
                {
                    label_loading.Text = label_loading.Text + ".";
                }
            }
        }

        private void tableLayoutPanel2_SizeChanged(object sender, EventArgs e)
        {

            //size = (tableLayoutPanel1.Width - 10) / 7;
            if(tableLayoutPanel2.Width > 1222) size = 8;
            else size = 4;

            //panel_Bar.Width = (tableLayoutPanel2.Width - ((tableLayoutPanel1.Width - 10) - panel_Bar.Width)) - panel_Bar.Width;
        }
    }
}
