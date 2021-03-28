using P_ZPP_1.AppDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_ZPP_1
{
    class DbQueryInfo
    {
        public QueryInfo queryInfo { get; set; }
        public void Add(string query, DateTime dateTime)
        {
            queryInfo = new QueryInfo(query, dateTime);
            SaveToLogDb(queryInfo);
        }
        public void SaveToLogDb(QueryInfo query)
        {
            using (var db = new AllegroAppContext())
            {
                db.QueryInfo.Add(query);
                db.SaveChanges();
            }

        }
        public QueryInfo GetLastQueryInfo()
        {
            QueryInfo qi = new QueryInfo();
            using (var db = new AllegroAppContext())
            {
                qi = db.QueryInfo.Last();
            }
            return qi;
        }
    }

}