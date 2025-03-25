using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fesco_Inventory.BL
{
    public class RequisitionBL
    {
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

        public RequisitionBL(int id, int folioNo, int officeId, int now, int itemId, int qauntity, DateTime reqD, int approver, string io, string receiver, string rece_Desig, string rece_by, DateTime rece_Date, int req_by, DateTime ded, byte[] rForm_pdf)
        {
            this.id = id;
            this.folioNo = folioNo;
            this.officeId = officeId;
            this.now = now;
            this.itemId = itemId;
            this.qauntity = qauntity;
            this.reqD = reqD;
            this.approver = approver;
            this.io = io;
            this.receiver = receiver;
            this.rece_Desig = rece_Desig;
            this.rece_by = rece_by;
            this.rece_Date = rece_Date;
            this.req_by = req_by;
            this.ded = ded;
            this.rForm_pdf = rForm_pdf;
        }

        public int Id { get => id; set => id = value; }
        public int OfficeId { get => officeId; set => officeId = value; }
        public int Now { get => now; set => now = value; }
        public int ItemId { get => itemId; set => itemId = value; }
        public int FolioNo { get => folioNo; set => folioNo = value; }
        public int Quantity { get => qauntity; set => qauntity = value; }
        public DateTime ReqD { get => reqD; set => reqD = value; }
        public int Approver { get => approver; set => approver = value; }
        public string IO { get => io; set => io = value; }
        public string Receiver { get => receiver; set => receiver = value; }
        public string Rece_Desig { get => rece_Desig; set => rece_Desig = value; }
        public string Rece_By { get => rece_by; set => rece_by = value; }
        public DateTime Rece_Date { get => rece_Date; set => rece_Date = value; }
        public int Req_By { get => req_by; set => req_by = value; }
        public DateTime Ded { get => ded; set => ded = value; }
        public byte[] RForm_pdf { get => rForm_pdf; set => rForm_pdf = value; }
    }
}