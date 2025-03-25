using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fesco_Inventory.BL
{
    public class InventoryBL
    {
        int id;
        int itemNo;
        string itemDesc;
        int quantity;
        int unit_of_measure;
        DateTime added_on;
        int added_by;
        bool out_of_stock;
        bool isDeleted;

        public InventoryBL(int id,int itemNo, string itemDesc, int quantity, int unit_of_measure, DateTime added_on, int added_by, bool out_of_stock, bool isDeleted)
        {
            this.id = id;
            this.itemNo = itemNo;
            this.itemDesc = itemDesc;
            this.quantity = quantity;
            this.unit_of_measure = unit_of_measure;
            this.added_on = added_on;
            this.added_by = added_by;
            this.out_of_stock = out_of_stock;
            this.isDeleted = isDeleted;
        }

        public int Id { get => id; set => id = value; }
        public int ItemNo { get => itemNo; set => itemNo = value; }
        public string ItemDesc { get => itemDesc; set => itemDesc = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public int Unit_of_Measure { get => unit_of_measure; set => unit_of_measure = value;}
        public DateTime Added_On { get => added_on; set => added_on = value; }
        public bool IsDeleted { get => isDeleted; set => isDeleted = value; }
        public int Added_By { get => added_by; set => added_by = value; }
        public bool Out_Of_Stock { get => out_of_stock; set => out_of_stock = value;}
    }
}
