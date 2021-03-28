using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using P_ZPP_1.AppDatabase;
namespace P_ZPP_1.Classes
{
    [TestClass]
    public class Tests_1
    {
        public void RunDatabaseTests()
        {
            AddSample_QueryInfo_DataObjectsToDatabase();
            AddSample_Items_DataObjectsToDatabase();
            AddSample_ItemParams_DataObjectsToDatabase();
        }
        [TestMethod]
        public void AddSample_QueryInfo_DataObjectsToDatabase()
        {
            var qi = new DbQueryInfo();
            var savedTime = DateTime.Now;
            var sampleQuery = "sampleQuery";
            qi.SaveToLogDb(new QueryInfo(sampleQuery, savedTime));
            using (var db = new AllegroAppContext())
            {                
                    Assert.IsTrue(db.QueryInfo.Where(x => x.Querry == sampleQuery).Any());
                    db.QueryInfo.Remove(db.QueryInfo.Where(x => x.Querry == sampleQuery).FirstOrDefault());
                    db.SaveChanges();
            }
        }
        [TestMethod]
        public void AddSample_Items_DataObjectsToDatabase()
        {
            var i = new P_ZPP_1.Properties.DbItems();
            i.SaveToItemDb(new Items(9999, "sampleProductName", 13M, true, 9999, @"C:\Users\Samo\Desktop\sampleImg.jpg", "https://www.diki.pl/slownik-angielskiego?q=sample+page"));
            using (var db = new AllegroAppContext())
            {
                Assert.IsTrue(db.Items.Where(x => x.Query_Id == 9999 && x.ProductName == "sampleProductName" && x.Price == 13M && x.ALLEGROsmart == true && x.PageNumber == 9999 && x.ImagePath == @"C:\Users\Samo\Desktop\sampleImg.jpg" && x.Hyperlink == "https://www.diki.pl/slownik-angielskiego?q=sample+page").Any());
                db.Items.Remove(db.Items.Where(x => x.Query_Id == 9999 && x.ProductName == "sampleProductName" && x.Price == 13M && x.ALLEGROsmart == true && x.PageNumber == 9999 && x.ImagePath == @"C:\Users\Samo\Desktop\sampleImg.jpg" && x.Hyperlink == "https://www.diki.pl/slownik-angielskiego?q=sample+page").FirstOrDefault());
                db.SaveChanges();
            }
        }
        [TestMethod]
        public void AddSample_ItemParams_DataObjectsToDatabase()
        {
            var ip = new DbItemParams();
            ip.SaveToParamDb(new ItemParams(9999, 9999, "samplePropertyname", "samplePropertyValue"));
            using (var db = new AllegroAppContext())
            {
                Assert.IsTrue(db.ItemParams.Where(x => x.Item_id==9999 && x.Querry_id == 9999 && x.Property_Name == "samplePropertyname" && x.Property_Value== "samplePropertyValue").Any());
                db.ItemParams.Remove(db.ItemParams.Where(x => x.Item_id == 9999 && x.Querry_id == 9999 && x.Property_Name == "samplePropertyname" && x.Property_Value == "samplePropertyValue").FirstOrDefault());
                db.SaveChanges();
            }
        }
    }
}
