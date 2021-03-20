using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P_ZPP_1.AppDatabase;

namespace P_ZPP_1
{
    /// <summary>
    /// Class containing simple methods that are meant to help adding data to tables more efficently.
    /// </summary>
    class DbLoader
    {
        /// <summary>
        /// Saves data to <see cref="QueryInfo"/>table.
        /// </summary>
        /// <param name="query">Information about query</param>
        public void SaveToLogDb(QueryInfo query)
        {
            using(var db = new AllegroAppContext())
            {
                db.QueryInfo.Add(query);
                db.SaveChanges();
            }

        }
        /// <summary>
        /// Saves data to <see cref="Items"/>table.
        /// </summary>
        /// <param name="result">Item information - name, price, etc.</param>
        public void SaveToItemDb(Items result)
        {
            using (var db = new AllegroAppContext())
            {
                db.Items.Add(result);
                db.SaveChanges();
            }

        }
        /// <summary>
        /// Saves data to <see cref="ItemParams"/> table.
        /// </summary>
        /// <param name="param">Item parameters.</param>
        public void SaveToParamDb(ItemParams param)
        {
            using (var db = new AllegroAppContext())
            {
                db.ItemParams.Add(param);
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
