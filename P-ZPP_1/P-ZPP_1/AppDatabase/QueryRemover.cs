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
            
            var db = new AppDatabase.AllegroAppContext();
            DateTime teraz = DateTime.Now;
            int numberOfItemsToBeRemoved = db.QueryInfo.Select(x => x.Querry).Distinct().Count() - 3;
            
            
            if (numberOfItemsToBeRemoved > 0)
            {
                var stringOfQueryToBeRemoved = db.QueryInfo.OrderBy(y => y.Date).Distinct().Select(x=> x.Querry).Take(numberOfItemsToBeRemoved).ToList();
                var TmpListOfAllItemsToBeRemoved = new List<QueryInfo>(); 
                
                foreach (var item in stringOfQueryToBeRemoved)
                    foreach (var item2 in db.QueryInfo.Where(x => x.Querry == item).ToList())
                        TmpListOfAllItemsToBeRemoved.Add(item2);

                foreach (var item in TmpListOfAllItemsToBeRemoved)
                    RemoveAllEntitiesWithID(item.Id);
            }else
                RemoveOutdatedQuery(teraz);    
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
        /// Method removes Old query results, if current query already exists in datatabase
        /// </summary>
        /// <returns>bool true if old querry wa removed</returns>
        public void RemoveOutdatedQuery(DateTime LastTime)
        {
            
            using (var db = new AppDatabase.AllegroAppContext())
            {

                var LatestQuery = db.QueryInfo.OrderByDescending(r => r.Date).Select(q=>q.Querry).FirstOrDefault();

                var timeDiference = LastTime.AddSeconds(-15);
                var SearchForOutdatedQuery = db.QueryInfo.Where(x => x.Querry == LatestQuery && x.Date < timeDiference);
                

                if (SearchForOutdatedQuery.Any())
                    foreach (var item in SearchForOutdatedQuery)
                     RemoveAllEntitiesWithID(item.Id);

                db.SaveChanges();
            }
        }
    }
}