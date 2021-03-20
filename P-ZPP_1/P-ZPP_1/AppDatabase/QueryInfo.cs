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
        [Key]
        public int Id { get; set; }
        public string Querry { get; set; }
        public DateTime Date { get; set; }
        public QueryInfo()
        {

        }
        public QueryInfo(string querry, DateTime date)
        {
            Querry = querry;
            Date = date;
        }
    }
}
