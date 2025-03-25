using Fesco_Inventory.BL;
using Spire.Pdf.Exporting.XPS.Schema;
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
    public partial class ReportType : Form
    {
        Panel pan;
        userBL user;
        Label label;
        public ReportType(Panel pan , userBL user, Label label)
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
         
        private void btn_add_Click(object sender, EventArgs e)
        {
            if (comboBox_reportType.SelectedItem != null)
            {
                int usgae = 0;

                if(comboBox_reportType.SelectedItem.ToString() == "Departmental Allocations Report (Overall)")
                    usgae = 1;

                else if (comboBox_reportType.SelectedItem.ToString() == "Departmental Allocations Report (Single Department)")
                    usgae = 2;

                else if (comboBox_reportType.SelectedItem.ToString() == "Departmental Allocations Report (Single Item)")
                    usgae = 3;

                else if (comboBox_reportType.SelectedItem.ToString() == "Departmental Repairs Report (Single Department)")
                    usgae = 5;

                else if (comboBox_reportType.SelectedItem.ToString() == "Discarded Items Report (Overall)")
                    usgae = 6;

                else if (comboBox_reportType.SelectedItem.ToString() == "Discarded Items Report (Single Item)")
                    usgae = 7;

                    

                //MessageBox.Show(usgae.ToString());

                reports uc = new reports(pan, user, usgae, label);
                getUC(uc);
                this.Close();
            }
            else
                MessageBox.Show("Please Select Report Type!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }
    }
}
