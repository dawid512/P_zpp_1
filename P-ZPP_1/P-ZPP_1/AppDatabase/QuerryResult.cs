using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_ZPP_1.AppDatabase
{
    public class QuerryResult
    {
        [Key]
        public int id { get; set; }
        public string productName { get; set; }
        public float price { get; set; }
        public float priceWithShipping { get; set; } 
        public int numberOfPeopleWhoAlsoBought { get; set; }
        public bool ALLEGROsmart { get; set; }

        public List<object> Pictures { get; set; }
    }
