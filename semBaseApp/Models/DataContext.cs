using System.Configuration;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace vls.Models
{
    public class DataContext : IdentityDbContext<MyUser>
    {
        public DataContext()
            : base("DefaultConnection")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<TransferFee> TransferFees { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<AgentBranch> AgentBranches { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<IdType> IdTypes { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Configuration.LazyLoadingEnabled = true;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Migrations.Configuration>());
            base.OnModelCreating(modelBuilder);
        }
    }
}