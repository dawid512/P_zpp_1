using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_ZPP_1.Classes
{
    public class ParsingToWpf
    {        
        public int Id { get; set; }
        public int Query_Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public bool ALLEGROsmart { get; set; }
        public int PageNumber { get; set; }
        public string ImagePath { get; set; }
        public string Hyperlink { get; set; }
        public string myParameters { get; set; }

        public ParsingToWpf(P_ZPP_1.AppDatabase.Items item, string myParams)
        {
            Id = item.Id;
            Query_Id = item.Query_Id;
            ProductName = item.ProductName;
            Price = item.Price;
            ALLEGROsmart = item.ALLEGROsmart;
            PageNumber = item.PageNumber;
            ImagePath = item.ImagePath;
            Hyperlink = item.Hyperlink;
            this.myParameters = myParams;
        }
    }
}
