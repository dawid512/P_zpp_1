﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_ZPP_1.DatabaseManagement
{
    /// <summary>
    /// Clas contains methods for removing oldest query from database <see cref="RemoveOldestQuery()"/> 
    /// </summary>
    public class QueryRemover
    {
        /// <summary>
        /// Method called to remove oldest QuerryInfo entities and all connected elements in Items and ItemParams from database that exceed history range. 
        /// </summary>
        public void RemoveOldestQueries()
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
    }
}