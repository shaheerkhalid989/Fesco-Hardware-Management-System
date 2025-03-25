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
    public partial class reCaptha : UserControl
    {
        Random random;
        string capt_ = "";
        Label label;

        Panel pan;
        string user;
        string pass;
        int usage;
        userBL user_;
        userBL uAdmin;
        public reCaptha(Panel pan, string user, string pass, int usage,userBL user_, userBL uAdmin, Label label)
        {
            InitializeComponent();
            this.pan = pan;
            this.user = user;
            this.pass = pass;
            this.usage = usage;
            this.user_ = user_;
            this.uAdmin = uAdmin;
            this.label = label;
        }

        private void reCaptha_Load(object sender, EventArgs e)
        {
            random = new Random();

            // Add a paint event handler to the panel
            tableLayoutPanel2.Paint += new PaintEventHandler(this.tableLayoutPanel2_Paint);

            // Start a timer that will fire 0.5 second
            Timer timer = new Timer();
            timer.Interval = 500;
            timer.Tick += new EventHandler(this.timer1_Tick);
            timer.Start();
            textbox_reCaptcha.Focus();
            set_reCaptcha();
        }

        public void set_reCaptcha()
        {
            capt_ = "";
            Random rand = new Random();
            int total = 0;
            string recap = "";

            do
            {
                int chr = rand.Next(48, 123);
                if ((chr >= 48 && chr <= 57) || (chr >= 65 && chr <= 90) || (chr >= 97 && chr <= 122))
                {
                    recap = recap + (char)chr;
                    total++;

                    if (total == 1)
                        label1.Text = "" + (char)chr;

                    else if (total == 2)
                        label2.Text = "" + (char)chr;

                    else if (total == 3)
                        label3.Text = "" + (char)chr;

                    else if (total == 4)
                        label4.Text = "" + (char)chr;

                    else if (total == 5)
                        label5.Text = "" + (char)chr;

                    else if (total == 6)
                    {
                        label6.Text = "" + (char)chr;
                        break;
                    }
                }
            }
            while (true);
            capt_ = recap;
            //MessageBox.Show(capt_);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tableLayoutPanel2.Invalidate();
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {
            Color color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

            // Set the color of the panel
                e.Graphics.FillRectangle(new SolidBrush(color), tableLayoutPanel2.ClientRectangle);
        }

        private void getUC(UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            pan.Controls.Clear();
            pan.Controls.Add(uc);
            uc.BringToFront();
        }

        private void btn_verify_Click(object sender, EventArgs e)
        {
            if (capt_ == textbox_reCaptcha.Text)
            {
                MessageBox.Show("reCaptcha Verified!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.timer1.Enabled = false;

                if (usage == 1)
                {
                    login uc = new login(pan, user, pass, true, label);
                    getUC(uc);
                }

                else if(usage == 2)
                {
                    userManagement uc = new userManagement(pan, uAdmin, label);
                    getUC(uc);

                    EditUser nf = new EditUser(user_, label);
                    nf.ShowDialog();
                }
            }

            else
            {
                MessageBox.Show("reCaptcha failed ....... try again!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                textbox_reCaptcha.Text = "";
                this.OnLoad(e);
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            if (usage == 1)
            {
                login uc = new login(pan, null, null, false, label);
                getUC(uc);
            }

            else if (usage == 2)
            {
                userManagement uc = new userManagement(pan, user_, label);
                getUC(uc);
            }
        }
    }
}
