using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_ZPP_1.AppDatabase
{
    /// <summary>
    /// Clas contains methods for removing oldest query from database <see cref="RemoveOldestQuery()"/> 
    /// </summary>
    public class QueryRemover
    {
        /// <summary>
        /// Method called to remove oldest QuerryInfo entities and all connected elements in Items and ItemParams from database that exceed history range. 
        /// </summary>
        public void QueryRemower_Work()
        {
            int HistoryRange = 3; //  number of queries stored in app history

            var db = new AppDatabase.AllegroAppContext();
            int numberOfItemsToBeRemoved = db.QueryInfo.Count() - HistoryRange;

            if (numberOfItemsToBeRemoved > 0)
            {
                var TmpListOfAllItems = db.QueryInfo.OrderBy(y => y.Date).ToList();

                for (int i = 0; i < numberOfItemsToBeRemoved; i++)
                    RemoveAllEntitiesWithID(TmpListOfAllItems.FirstOrDefault().Id);
            }

            RemoveOutdatedQuery();
            RemoveSponsoredOffersItems();

        }
        /// <summary>
        /// Method invokes all methods required to remove all elements connected to QuerryInfo with Id of queryID from database:
        /// </summary>
        /// <param name="queryId"></param>
        public void RemoveAllEntitiesWithID(int queryId)
        {
            RemoveFromQueryInfoElements(queryId);
            RemoveFromItemElements(queryId);
            RemoveFromItemParamsElements(queryId);
        }
        /// <summary>
        /// Method removes entities from QueryInfo with id equal to queryId
        /// </summary>
        /// <param name="queryId"></param>
        public void RemoveFromQueryInfoElements(int queryId)
        {
            var db = new AppDatabase.AllegroAppContext();
            var toBeRemoved = db.QueryInfo.Where(x => x.Id == queryId).ToList();

            foreach (var entity in toBeRemoved)
                db.QueryInfo.Remove(entity);
            db.SaveChanges();
        }
        /// <summary>
        /// Method removes entities from Items with Query_Id equal to queryId
        /// </summary>
        /// <param name="queryId"></param>
        public void RemoveFromItemElements(int queryId)
        {
            var db = new AppDatabase.AllegroAppContext();
            var toBeRemoved = db.Items.Where(x => x.Query_Id == queryId).ToList();

            foreach (var entity in toBeRemoved)
                db.Items.Remove(entity);
            db.SaveChanges();
        }
        /// <summary>
        /// Method removes entities from ItemParams with Querry_Id equal to queryId
        /// </summary>
        /// <param name="queryId"></param>
        public void RemoveFromItemParamsElements(int queryId)
        {
            var db = new AppDatabase.AllegroAppContext();
            var toBeRemoved = db.ItemParams.Where(x => x.Querry_id == queryId).ToList();

            foreach (var entity in toBeRemoved)
                db.ItemParams.Remove(entity);
            db.SaveChanges();
        }
        /// <summary>
        /// Method removes sposored offer taht are duplicated on every page.
        /// </summary>
        public void RemoveSponsoredOffersItems()
        {
            /*var db = new AppDatabase.AllegroAppContext();
            var LatestQueryId = db.QueryInfo.OrderBy(r => r.Date).FirstOrDefault().Id;

            List<Items> LatestItemListPage_1 = db.Items.Where(qid => qid.Query_Id == LatestQueryId && qid.PageNumber == 1).ToList();
            List<Items> LatestItemListPage_2 = db.Items.Where(qid => qid.Query_Id == LatestQueryId && qid.PageNumber == 2).ToList();
            List<AppDatabase.Items> SponsoredOffersList = new List<AppDatabase.Items>();

            foreach (var itemFromPage_1 in LatestItemListPage_1)
                foreach (var itemFromPage_2 in LatestItemListPage_2)
                    if (itemFromPage_1.ProductName == itemFromPage_2.ProductName && itemFromPage_1.Price == itemFromPage_2.Price)
                        SponsoredOffersList.Add(itemFromPage_1);

            var LatestItemList = db.Items.Where(q => q.Query_Id == LatestQueryId);

            foreach (var item in LatestItemList)
                if (SponsoredOffersList.Contains(item) && item.PageNumber != 1)
                {
                    db.Items.Remove(item);
                    foreach (var ItemParamsToBeRemoved in db.ItemParams.Where(y=> y.Item_id == item.Id).ToList())
                        db.ItemParams.Remove(ItemParamsToBeRemoved);
                }
            db.SaveChanges();  */        
        }
        /// <summary>
        /// Method removes Old query results, if current query already exists in datatabase
        /// </summary>
        /// <returns>bool true if old querry wa removed</returns>
        public void RemoveOutdatedQuery()
        {
            var db = new AppDatabase.AllegroAppContext();
            var LatestQuery = db.QueryInfo.OrderBy(r => r.Date).FirstOrDefault();

            var SearchForOutdatedQuery = db.QueryInfo.Where(x => x.Querry == LatestQuery.Querry && x.Id != LatestQuery.Id);

            if (SearchForOutdatedQuery.Any())
                RemoveAllEntitiesWithID(SearchForOutdatedQuery.FirstOrDefault().Id);
        }
    }
}