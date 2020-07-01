using System.Data.Common;
using MAVN.Persistence.PostgreSQL.Legacy;
using MAVN.Service.BonusCustomerProfile.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.BonusCustomerProfile.MsSqlRepositories
{
    public class BonusCustomerProfileContext : PostgreSQLContext
    {
        private const string Schema = "bonus_customer_profile";

        public DbSet<CampaignsContribution> CampaignsContributions { get; set; }
        public DbSet<CustomerProfile> CustomerProfiles { get; set; }

        public BonusCustomerProfileContext()
            : base(Schema)
        {
        }

        //Needed for using InMemoryDb when writing tests
        public BonusCustomerProfileContext(DbContextOptions options)
            : base(Schema, options)
        {
        }

        public BonusCustomerProfileContext(string connectionString, bool isTraceEnabled)
            : base(Schema, connectionString, isTraceEnabled)
        {
        }

        public BonusCustomerProfileContext(DbConnection dbConnection)
            : base(Schema, dbConnection)
        {
        }

        protected override void OnMAVNModelCreating(ModelBuilder modelBuilder)
        {
            var campaignsContributionsBuilder = modelBuilder.Entity<CampaignsContribution>();
            campaignsContributionsBuilder.HasIndex(c => new
            {
                c.CustomerId,
                c.CampaignId
            }).IsUnique(false);

            var customerProfileBuilder = modelBuilder.Entity<CustomerProfile>();
            customerProfileBuilder.HasIndex(c => c.CustomerId).IsUnique();
        }
    }
}
