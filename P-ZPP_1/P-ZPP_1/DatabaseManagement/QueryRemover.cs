using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_ZPP_1
{
    /// <summary>
    /// Clas contains methods for removing oldest query from database <see cref="RemoveOldestQuery()"/> 
    /// </summary>
    public class QueryRemover
    {
        /// <summary>
        /// When there are more than 3 entities in QueryInfo this method removes oldest QuerryInfo entity, all connected elements in Items, ItemParams from database and  downloaded photos. 
        /// </summary>
        public void RemoveOldestQuery()
        {
            var db = new AppDatabase.AllegroAppContext();

            if (db.QueryInfo.Count()>3)
            {
                var ItemToBeRemoved = db.QueryInfo.OrderBy(y => y.Date).FirstOrDefault();
                RemoveAllEntitiesWithID(ItemToBeRemoved.Id);
                RemovePictures(ItemToBeRemoved.Id, ItemToBeRemoved.Querry);
            }
            

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
        /// Method removes downloaded photos related to removed query.
        /// </summary>
        /// <param name="queryID"></param>
        /// <param name="query"></param>
        public void RemovePictures(int queryID, string query)
        {
            DirectoryInfo folderToBeDeleted = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), (query + "_" + queryID.ToString())));
            
            foreach (var item in folderToBeDeleted.GetFiles())         
                item.Delete();           
            
            folderToBeDeleted.Delete();
        }
    }
}
