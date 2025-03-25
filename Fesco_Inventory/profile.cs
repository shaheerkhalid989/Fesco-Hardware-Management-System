using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fesco_Inventory.BL;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Fesco_Inventory
{
    public partial class profile : UserControl
    {
        Panel pan;
        userBL user;
        Label label;

        Image userImg;

        public profile(Panel pan, userBL user, Label label)
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

        private void btn_back_Click(object sender, EventArgs e)
        {
            if (user.Isadmin)
            {
                AdminOperations uc = new AdminOperations(pan, user, label);
                getUC(uc);
            }

            else
            {
                UsualOperations uc = new UsualOperations(pan, user, label);
                getUC(uc);
            }
        }

        private void profile_Load(object sender, EventArgs e)
        {
            label.Text = "Profile";
            textbox_name.Text = user.Name;
            textbox_designation.Text = user.Designation;
            if (user.Cnic == null)
                textbox_cnic.Text = "Null";
            else
                textbox_cnic.Text = user.Cnic;

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand(string.Concat("Select [Value] from Lookup where Id = ", user.Gender, " and Category = 'Gender'"), con);
            string gender = (string)cmd.ExecuteScalar();
            TextBox_gender.Text = gender;

            SqlCommand cmdd = new SqlCommand(string.Concat("Select [Value] from Lookup where Id = ", user.Role, " and Category = 'ROLE'"), con);
            string role = (string)cmdd.ExecuteScalar();
            TextBox_Role.Text = role;

            textbox_user.Text = user.Username;
            textbox_pass.Text = user.Password;

            if (user.Email == null || user.Email == "")
                textbox_email.Text = "Null";
            else
                textbox_email.Text = user.Email;

            if (user.Phone == null || user.Phone == "")
                textbox_phone.Text = "Null";
            else
                textbox_phone.Text = user.Phone;

            if (user.Img == null)
            {
                btn_upload.Text = "Upload";

                if (gender == "Male")
                {
                    pictureBox1.Image = Properties.Resources.boy__1_;
                    userImg = Properties.Resources.boy__1_;
                }
                else
                {
                    pictureBox1.Image = Properties.Resources.girl;
                    userImg = Properties.Resources.girl;
                }

                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            }
            else
            {
                pictureBox1.Image = user.Img;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }

            textbox_name.Enabled = false;
            textbox_phone.Enabled = false;
            textbox_email.Enabled = false;
            textbox_cnic.Enabled = false;
            textbox_user.Enabled = false;
            textbox_designation.Enabled = false;
            textbox_pass.Enabled = false;
            TextBox_Role.Enabled = false;
            TextBox_gender.Enabled = false;

            textbox_pass.PasswordChar = '*';
            btn_hide.Image = Properties.Resources.show;
        }

        private void btn_editUserPass_Click(object sender, EventArgs e)
        {
            EditUserPass newform = new EditUserPass(pan,user,1, label);
            newform.ShowDialog();
            profile uc = new profile(pan,user, label);
            getUC(uc);
        }

        private void btn_hide_Click(object sender, EventArgs e)
        {
            textbox_pass.Enabled=true;
            if (textbox_pass.PasswordChar == '\0')
            {
                textbox_pass.PasswordChar = '*';
                btn_hide.Image = Properties.Resources.show;
            }

            else if (textbox_pass.PasswordChar == '*')
            {
                textbox_pass.PasswordChar = '\0';
                btn_hide.Image = Properties.Resources.hide;
            }
            textbox_pass.Enabled = false;
        }


        private void btn_upload_Click(object sender, EventArgs e)
        {
            string imageLocation = "";
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *gif; *.bmp;)|*.jpg; *.jpeg; *.gif; *.bmp;";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imageLocation = openFileDialog.FileName;

                    byte[] imageBytes = File.ReadAllBytes(imageLocation);
                    Image image = Image.FromFile(imageLocation);

                    long size = 6000*4000;

                    if (image.Height * image.Width < size)
                    {
                        pictureBox1.Image = new Bitmap(imageLocation);
                        var con = Configuration.getInstance().getConnection();
                        SqlCommand im = new SqlCommand(string.Concat("Update Employee set Profile_Picture = @Pp where Id = ", user.ID), con);
                        im.Parameters.AddWithValue("@Pp", imageBytes);
                        im.ExecuteNonQuery();

                        if (btn_upload.Text == "Upload")
                        {
                            MessageBox.Show("Profile image Uploaded", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            btn_upload.Text = "Update";
                        }
                        else
                            MessageBox.Show("Profile image Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        // Convert the byte array back to an Image object
                        using (MemoryStream stream = new MemoryStream(imageBytes))
                        {
                            pictureBox1.Image = Image.FromStream(stream);
                            user.Img = Image.FromStream(stream);
                        }
                    }

                    else
                        MessageBox.Show("Image dimentions should be less than 6000 x 4000 OR 4000 x 6000", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (user.Img != null)
            {
                Profile_Picture nf = new Profile_Picture(user.Img, label);
                nf.ShowDialog();
            }
            else
            {
                Profile_Picture nf = new Profile_Picture(userImg, label);
                nf.ShowDialog();
            }
        }
    }
}
