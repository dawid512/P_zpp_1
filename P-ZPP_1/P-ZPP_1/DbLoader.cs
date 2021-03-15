using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P_ZPP_1.AppDatabase;

namespace P_ZPP_1
{
    class DbLoader
    {
        public void SaveToLogDb(QueryInfo query)
        {
            using(var db = new AllegroAppContext())
            {
                db.QueryInfo.Add(query);
                db.SaveChanges();
            }

        }

        public void SaveToItemDb(Items result)
        {
            using (var db = new AllegroAppContext())
            {
                db.Items.Add(result);
                db.SaveChanges();
            }

        }

        public void SaveToParamDb(ItemParams param)
        {
            using (var db = new AllegroAppContext())
            {
                db.ItemParams.Add(param);
                db.SaveChanges();
            }

        }
    }
}
