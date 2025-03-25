using Spire.Pdf.Graphics;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fesco_Inventory
{
    public partial class FormView : Form
    {
        byte[] data;
        public FormView(byte[] data)
        {
            InitializeComponent();
            this.data = data;
        }

        private void FormView_Load(object sender, EventArgs e)
        {
            try
            {
                using (MemoryStream memoryStream = new MemoryStream(data))
                {
                    pdfViewer1.LoadFromStream(memoryStream);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                MessageBox.Show("Error: " + ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            }
        }
    }
}
