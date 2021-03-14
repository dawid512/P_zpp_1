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

        public Items(int id, string productName, decimal price, float priceWithShipping, int numberOfPeopleWhoAlsoBought, bool aLLEGROsmart, string image, List<ItemParams> parameters)
        {
            Id = id;
            ProductName = productName;
            Price = price;
            PriceWithShipping = priceWithShipping;
            NumberOfPeopleWhoAlsoBought = numberOfPeopleWhoAlsoBought;
            ALLEGROsmart = aLLEGROsmart;
            Image = image;
            this.parameters = parameters;
        }
    }
}
