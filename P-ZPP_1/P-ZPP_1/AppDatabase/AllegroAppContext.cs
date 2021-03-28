using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_ZPP_1.AppDatabase
{
    /// <summary>
    /// The database context class. 
    /// Contains the model of database in Code First modeling in <see href="https://docs.microsoft.com/pl-pl/ef/">Entity Framework</see>.
    /// </summary>
    public class AllegroAppContext : DbContext
    {
        /// <summary>
        /// Database connection string.
        /// </summary>
        public AllegroAppContext()
            : base("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = AllegroDatabase; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False") => Database.SetInitializer<AllegroAppContext>(new CreateDatabaseIfNotExists<AllegroAppContext>());


        public DbSet<QueryInfo> QueryInfo { get; set; }

        public DbSet<Items> Items { get; set; }

        public DbSet<ItemParams> ItemParams { get; set; }

        /// <summary>
        /// Ensures that table names are singular.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)    
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
