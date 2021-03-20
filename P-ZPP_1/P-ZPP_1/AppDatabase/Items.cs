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
        private int queryId;

        [Key]
        public int Id { get; set; }
        public int Query_Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public bool ALLEGROsmart { get; set; }
        public int PageNumber { get; set; }
        //public string ImagePath { get; set; }

        public Items()
        {

        }

        

        public Items(int queryId, string productName, decimal price, bool allegroSmart, int pageNumber)
        {
            this.queryId = queryId;
            ProductName = productName;
            Price = price;
            ALLEGROsmart = allegroSmart;
            PageNumber = pageNumber;
        }
    }
}
