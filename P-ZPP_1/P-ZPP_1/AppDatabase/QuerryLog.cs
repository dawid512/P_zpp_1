using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_ZPP_1.AppDatabase
{
    public class QuerryLog
    {
        public int id { get; set; }
        public string querry { get; set; }
        public DateTime date { get; set; }

        public QuerryResult querryResult { get; set; }
    }
}
