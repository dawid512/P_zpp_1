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
        private int itemId;
        private int queryId;
        private string propertyName;
        private string propertyValue;

        [Key]
        public int Id { get; set; }
        public int Item_id { get; set; }
        public int Querry_id { get; set; }
        public string Property_Name { get; set; }
        public string Property_Value { get; set; }

        

        

        public ItemParams(int itemId, int queryId, string propertyName, string propertyValue)
        {
            this.itemId = itemId;
            this.queryId = queryId;
            this.propertyName = propertyName;
            this.propertyValue = propertyValue;
        }
    }
}
