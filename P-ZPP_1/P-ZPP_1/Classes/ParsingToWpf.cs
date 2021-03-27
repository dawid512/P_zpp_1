using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_ZPP_1.Classes
{
   public  class ParsingToWpf
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

        public ParsingToWpf(int id, int query_Id, string productName, decimal price, bool aLLEGROsmart, int pageNumber, string imagePath, string hyperlink, string myParameters)
        {
            Id = id;
            Query_Id = query_Id;
            ProductName = productName;
            Price = price;
            ALLEGROsmart = aLLEGROsmart;
            PageNumber = pageNumber;
            ImagePath = imagePath;
            Hyperlink = hyperlink;
            this.myParameters = myParameters;
        }
    }
}
