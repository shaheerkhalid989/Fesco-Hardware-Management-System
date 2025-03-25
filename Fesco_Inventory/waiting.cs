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
    public partial class waiting : UserControl
    {
        Panel pan;
        userBL user;
        Panel panel;
        Form f;
        Label label;

        public waiting(Panel pan, userBL user, Panel panel, Form f, Label label)
        {
            InitializeComponent();
            this.pan = pan;
            this.user = user;
            this.panel = panel;
            this.f = f;
            this.label = label;
        }

        private void getUC_Panel(UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            panel.Controls.Clear();
            panel.Controls.Add(uc);
            uc.BringToFront();
        }

        private void waiting_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            Otp_UC uc = new Otp_UC(pan, user, panel,f,label);
            getUC_Panel(uc);

        }
    }
}
