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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void getUC(UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            panel2.Controls.Clear();
            panel2.Controls.Add(uc);

            uc.BringToFront();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            login uc = new login(panel2,null,null, false,label1);
            getUC(uc);
        }

        private void Form1_MaximumSizeChanged(object sender, EventArgs e)
        {
            if(WindowState == FormWindowState.Maximized)
            {
                label1.Font = new Font("label1",50);
            }
        }

        private void Form1_MaximizedBoundsChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                label1.Font = new Font("label1", 50);
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                Font font = new Font("Comic Sans MS", 62, FontStyle.Bold);

                // Set the font of the Label control
                label1.Font = font;
            }

            else if (WindowState == FormWindowState.Normal)
            {
                Font font = new Font("Comic Sans MS", 37, FontStyle.Bold);

                // Set the font of the Label control
                label1.Font = font;
            }
        }
    }
}
