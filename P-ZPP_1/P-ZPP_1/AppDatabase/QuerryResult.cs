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
        public int Querry_Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public float PriceWithShipping { get; set; }
        public int NumberOfPeopleWhoAlsoBought { get; set; }
        public bool ALLEGROsmart { get; set; }
        public string Image { get; set; }

        public QuerryResult(int id, int querry_Id, string productName, double price, float priceWithShipping, int numberOfPeopleWhoAlsoBought, bool aLLEGROsmart, string image)
        {
            Id = id;
            Querry_Id = querry_Id;
            ProductName = productName;
            Price = price;
            PriceWithShipping = priceWithShipping;
            NumberOfPeopleWhoAlsoBought = numberOfPeopleWhoAlsoBought;
            ALLEGROsmart = aLLEGROsmart;
            Image = image;
        }
    }
}
