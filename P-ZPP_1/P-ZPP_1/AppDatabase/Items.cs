using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_ZPP_1.AppDatabase
{
    /// <summary>
    /// The derived table containg all items from a result of the query from <see cref="QueryInfo">QueryInfo</see> base table.
    /// </summary>
    public class Items
    {
        [Key]
        public int Id { get; set; }
        public int Query_Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public bool ALLEGROsmart { get; set; }
        public int PageNumber { get; set; }
        public string ImagePath { get; set; }
        public string Hyperlink { get; set; }

        public Items(int query_Id, string productName, decimal price, bool aLLEGROsmart, int page, string imagepath, string hyperlink)
        {
            Query_Id = query_Id;
            ProductName = productName;
            Price = price;
            ALLEGROsmart = aLLEGROsmart;
            PageNumber = page;
            ImagePath = imagepath;
            Hyperlink = hyperlink;
        }

        public Items()
        {
            
        }
    }
}
