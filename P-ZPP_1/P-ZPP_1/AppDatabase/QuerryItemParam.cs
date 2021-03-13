using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_ZPP_1.AppDatabase
{
    public class QuerryItemParam
    {
        [Key]
        public int Id { get; set; }
        public int Item_Id { get; set; }
        public int Querry_Id { get; set; }
        public string Property_Name { get; set; }
        public string Property_Value { get; set; }

        public QuerryItemParam(int id, int item_Id, int querry_Id, string property_Name, string property_Value)
        {
            Id = id;
            Item_Id = item_Id;
            Querry_Id = querry_Id;
            Property_Name = property_Name;
            Property_Value = property_Value;
        }
    }
}
