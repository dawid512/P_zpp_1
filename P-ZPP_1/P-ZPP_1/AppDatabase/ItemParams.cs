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
        public int Item_Id { get; set; }
        public string Property_Name { get; set; }
        public string Property_Value { get; set; }

        public ItemParams(int id, int item_Id, string property_Name, string property_Value)
        {
            Id = id;
            Item_Id = item_Id;
            Property_Name = property_Name;
            Property_Value = property_Value;
        }
    }
}
