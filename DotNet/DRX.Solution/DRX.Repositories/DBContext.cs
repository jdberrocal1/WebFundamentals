using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using DRX.Common.Models.Contacts;
using DRX.Common.Models.Vehicles;

namespace DRX.Repositories
{
    public class DBContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public DBContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<DBContext>(new CreateDatabaseIfNotExists<DBContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
