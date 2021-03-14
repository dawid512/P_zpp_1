using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_ZPP_1.AppDatabase
{
    public class AllegroAppContext : DbContext
    {

        public AllegroAppContext() 
            : base("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = AllegroDatabase; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        {
            Database.SetInitializer<AllegroAppContext>(new CreateDatabaseIfNotExists<AllegroAppContext>());
        }


<<<<<<< Updated upstream
<<<<<<< Updated upstream
        public DbSet<QueryInfo> QuerryLogs { get; set; }
=======
        public DbSet<QuerryInfo> QuerryLogs { get; set; }
>>>>>>> Stashed changes
=======
        public DbSet<QuerryInfo> QuerryLogs { get; set; }
>>>>>>> Stashed changes
        public DbSet<Items> QuerryResults { get; set; }
        public DbSet<ItemParams> QuerryItemParams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)    //pilnuje konwencji anzwniczej -> nazwy tabel powinny byc w liczbie pojedynczej
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
