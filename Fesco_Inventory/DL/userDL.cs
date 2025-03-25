using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fesco_Inventory.BL;
using Fesco_Inventory.DL;
using System.Web.UI;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Fesco_Inventory.DL
{
    public class userDL
    {
        public static userBL getUserFromEmployeeTable(int id_)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand command = new SqlCommand(string.Concat("select * from Employee where id = ",id_.ToString()), con);
            SqlDataReader reader = command.ExecuteReader();
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
            Image img;

            reader.Read();

            id = (int)reader["Id"];
            name = (string)reader["EmployeeName"];  
            object obj = reader["email"];
            if (obj is DBNull)
                email = null;
            else
                email = (string)obj;
            username = (string)reader["Username"];
            password = (string)reader["UserPassword"];
            gender = (int)reader["Gender"];
            object objj = reader["Phone"];
            if (objj is DBNull)
                phone = null;
            else
                phone = (string)objj;
            designation = (string)reader["Designation"];
            role = (int)reader["UserRole"];
            isadmin = (bool)reader["isAdmin"];
            isdeleted = (bool)reader["isDeleted"];
            object cnicc = reader["Cnic"];
            if (cnicc is DBNull)
                cnic = null;
            else
                cnic = (string)reader["Cnic"];
            secQuestion = (int)reader["SQ"];
            firstLogin = (bool)reader["FL"];
            object secQA = reader["SQA"];
            if (secQA is DBNull)
                sqa = null;
            else
                sqa = (string)reader["SQA"];
            object img_ = reader["Profile_Picture"];
            if (img_ is DBNull)
                img  = null;
            else
            {
                byte[] imageBytes = (byte[])reader["Profile_Picture"];

                // Convert the byte array back to an Image object
                using (MemoryStream stream = new MemoryStream(imageBytes))
                {
                    img = Image.FromStream(stream);
                }
            }


            reader.Close();

            userBL user = new userBL(id,name, email, username, password, gender, phone, designation, role,isadmin,isdeleted,cnic,secQuestion,firstLogin,sqa,img);
            return user;

        }
    }
}
