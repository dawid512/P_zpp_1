using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_ZPP_1.AppDatabase
{
    public class QuerryInfo
    {
        [Key]
        public int Id { get; set; }
        public string querry { get; set; }
        public DateTime date { get; set; }
        public List<Items> querryItems { get; set; }

        public QuerryInfo(int id, string querry, DateTime date, List<Items> querryItems)
        {
            Id = id;
            this.querry = querry;
            this.date = date;
            this.querryItems = querryItems;
        }
    }
}
