using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_ZPP_1.AppDatabase
{
    public class Items
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public float PriceWithShipping { get; set; }
        public int NumberOfPeopleWhoAlsoBought { get; set; }
        public bool ALLEGROsmart { get; set; }
        public string Image { get; set; }
        public List<ItemParams> parameters { get; set; }
        
    }
}
