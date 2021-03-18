using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_ZPP_1.AppDatabase
{
    /// <summary>
    /// Table containg all paramaters for a single item from <see cref="Items">Items</see> table.
    /// </summary>
    public class ItemParams
    {
        [Key]
        public int Id { get; set; }
        public int Item_id { get; set; }
        public int Querry_id { get; set; }
        public string Property_Name { get; set; }
        public string Property_Value { get; set; }

        public ItemParams(int item_id, int querry_id, string property_Name, string property_Value)
        {
            this.Item_id = item_id;
            this.Querry_id = querry_id;
            Property_Name = property_Name;
            Property_Value = property_Value;
        }
    }
}
