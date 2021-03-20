using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_ZPP_1.AppDatabase
{
    /// <summary>
    /// The base class of database, storing the information about query and date.
    /// </summary>
    public class QueryInfo
    {
        private string query;
        private DateTime dateTime;

        [Key]
        public int Id { get; set; }
        public string Querry { get; set; }
        public DateTime Date { get; set; }
        public QueryInfo()
        {

        }
        

        

        public QueryInfo(string query, DateTime dateTime)
        {
            this.query = query;
            this.Date = dateTime;
        }
    }
}
