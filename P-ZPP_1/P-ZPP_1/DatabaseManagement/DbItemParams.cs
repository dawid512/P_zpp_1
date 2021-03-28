using P_ZPP_1.AppDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_ZPP_1
{
    class DbItemParams
    {
        public void Add(int itemId, int queryId, string propertyName, string propertyValue)
        {
            ItemParams item = new ItemParams(itemId, queryId, propertyName, propertyValue);
            SaveToParamDb(item);
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
