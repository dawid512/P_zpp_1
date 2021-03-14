using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_ZPP_1.AppDatabase
{
    public class ItemParams
    {
        [Key]
        public int Id { get; set; }
        public int item_id { get; set; }
        public int querry_id { get; set; }
        public string Property_Name { get; set; }
        public string Property_Value { get; set; }

        public ItemParams(int id, int item_id, int querry_id, string property_Name, string property_Value)
        {
            Id = id;
            this.item_id = item_id;
            this.querry_id = querry_id;
            Property_Name = property_Name;
            Property_Value = property_Value;
        }
    }
}
