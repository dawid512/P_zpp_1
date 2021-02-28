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

        public AllegroAppContext() : base(@" Data Source = DESKTOP-IJ22KEC\SQLEXPRESS;Initial Catalog = AllegroAppDatabase; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        {
        }

        public DbSet<QuerryLog> QuerryLogs { get; set; }
        public DbSet<QuerryResult> QuerryResults { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)    //pilnuje konwencji anzwniczej -> nazwy tabel powinny byc w liczbie pojedynczej
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
