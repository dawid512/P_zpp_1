using P_ZPP_1.AppDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_ZPP_1.Properties
{
    class DbItems
    {
        public Items item { get; set; }
        public void Add(int queryId, string productName, decimal price, bool allegroSmart, int pageNumber, string imagepath, string hyperlink)
        {
            item = new Items(queryId, productName, price, allegroSmart, pageNumber, imagepath, hyperlink);
            SaveToItemDb(item);
        }

        public void SaveToItemDb(Items result)
        {
            using (var db = new AllegroAppContext())
            {
                db.Items.Add(result);
                db.SaveChanges();
            }
        }
    }
}
