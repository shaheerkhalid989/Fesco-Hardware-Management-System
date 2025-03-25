using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fesco_Inventory.BL;

namespace Fesco_Inventory.DL
{
    public class RequisitionDL
    {
        public static RequisitionBL getRequisitionfromReqTable(int id_)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand command = new SqlCommand(string.Concat("select * from Requisition where id = ", id_.ToString()), con);
            SqlDataReader reader = command.ExecuteReader();

            int id;
            int folioNo;
            int officeId;
            int now;
            int itemId;
            int qauntity;
            DateTime reqD;
            int approver;
            string io;
            string receiver;
            string rece_Desig;
            string rece_by;
            DateTime rece_Date;
            int req_by;
            DateTime ded;
            byte[] rForm_pdf;

            reader.Read();

            id = (int)reader["Id"];
            folioNo = (int)reader["FolioNo"];
            officeId = (int)reader["OfficeId"];
            now = (int)reader["Nature_of_Work"];
            itemId = (int)reader["ItemId"];
            qauntity = (int)reader["Quantity"];
            reqD = (DateTime)reader["Requisition_Date"];
            approver = (int)reader["Approved_By"];
            io = (string)reader["Issuing_officer"];
            receiver = (string)reader["Receiver"];
            rece_Desig = (string)reader["Receiver_Designation"];
            rece_by = (string)reader["Received_By"];
            rece_Date = (DateTime)reader["Requisition_Date"];
            req_by = (int)reader["Requisit_By"];
            ded = (DateTime)reader["DED"];
            List<byte> byteList = new List<byte>();
            if (!reader.IsDBNull(0)) // Check if the value is not null
            {
                rForm_pdf = (byte[])reader["rForm"];

                byteList.AddRange(rForm_pdf);
            }
            rForm_pdf = (byte[])reader["rForm"];

            reader.Close();

            RequisitionBL req = new RequisitionBL(id, folioNo,officeId, now, itemId, qauntity, reqD, approver, io, receiver, rece_Desig, rece_by, rece_Date, req_by, ded, rForm_pdf);
            return req;
        }
    }
}
