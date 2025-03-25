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
    public partial class Profile_Picture : Form
    {
        Image img;
        Label label;
        public Profile_Picture(Image img, Label label)
        {
            InitializeComponent();
            this.img = img;
            this.label = label;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Profile_Picture_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = img;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

        }
    }
}
