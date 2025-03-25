using Fesco_Inventory.BL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fesco_Inventory.DL
{
    public class InventoryDL
    {
        public static InventoryBL getItemFromInventoryTable(int id_)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand command = new SqlCommand(string.Concat("select * from Inventory where id = ", id_.ToString()), con);
            SqlDataReader reader = command.ExecuteReader();

            int id;
            int itemNo;
            string itemDesc;
            int quantity;
            int unit_of_measure;
            DateTime added_on;
            int added_by;
            bool out_of_stock;
            bool isDeleted;

            reader.Read();

            id = (int)reader["Id"];
            itemNo = (int)reader["ItemNo"];
            itemDesc = (string)reader["Item_Description"];
            quantity = (int)reader["Quantity"];
            unit_of_measure = (int)reader["Unit_of_Measure"];
            added_on = (DateTime)reader["Added_On"];
            added_by = (int)reader["Added_By"];
            out_of_stock = (bool)reader["Out_of_Stock"];
            isDeleted = (bool)reader["isDeleted"];

            reader.Close();

            InventoryBL invent = new InventoryBL(id,itemNo,itemDesc,quantity,unit_of_measure,added_on, added_by, out_of_stock,isDeleted);
            return invent;
        }
    }
}
