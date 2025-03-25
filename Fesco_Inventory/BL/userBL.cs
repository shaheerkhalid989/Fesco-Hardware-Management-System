using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fesco_Inventory.BL
{
    public class userBL
    {
        int id;
        string name;
        string email;
        string username;
        string password;
        int gender;
        string phone;
        string designation;
        int role;
        bool isadmin;
        bool isdeleted;
        string cnic;
        int secQuestion;
        bool firstLogin;
        string sqa;
        Image  img;

        public userBL(int id, string name, string email, string username, string password, int gender, string phone, string designation, int role, bool isadmin, bool isdeleted, string cnic, int secQuestion, bool firstLogin, string sqa,Image img)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.username = username;
            this.password = password;
            this.gender = gender;
            this.phone = phone;
            this.designation = designation;
            this.role = role;
            this.isadmin = isadmin;
            this.isdeleted = isdeleted;
            this.cnic = cnic;
            this.secQuestion = secQuestion;
            this.firstLogin = firstLogin;
            this.sqa = sqa;
            this.img = img;
        }

        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public int Gender { get => gender; set => gender = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Designation { get => designation; set => designation = value; }
        public int Role { get => role; set => role = value; }
        public bool Isadmin { get => isadmin; set => isadmin = value; }
        public bool Isdeleted { get => isdeleted; set => isdeleted = value; }
        public string Cnic { get => cnic; set => cnic = value; } 
        public int SecQuestion { get => secQuestion; set => secQuestion = value; }
        public bool FirstLogin { get => firstLogin; set => firstLogin = value; }
        public string Sqa { get => sqa; set => sqa = value; }
        public Image Img { get => img; set => img = value; }
    }
}
