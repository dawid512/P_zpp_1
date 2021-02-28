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
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public float PriceWithShipping { get; set; }
        public int NumberOfPeopleWhoAlsoBought { get; set; }
        public bool ALLEGROsmart { get; set; }
        //tymczasowe zdjecie
        public string Image { get; set; }
        public List<object> Pictures { get; set; }

        public QuerryResult(int id, string ProductName, float Price, float PriceWithShipping, int NumberOfPeopleWhoAlsoBought, bool ALLEGROsmart, string Image)
        {
            Id = id;
            ProductName = ProductName;
            Price = Price;
            PriceWithShipping = PriceWithShipping;
            NumberOfPeopleWhoAlsoBought = NumberOfPeopleWhoAlsoBought;
            ALLEGROsmart = ALLEGROsmart;
            Image = Image;

        }
    }
}
