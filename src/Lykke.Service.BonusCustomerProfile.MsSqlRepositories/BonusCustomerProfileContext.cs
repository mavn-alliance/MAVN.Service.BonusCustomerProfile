using System.Data.Common;
using Lykke.Common.MsSql;
using Lykke.Service.BonusCustomerProfile.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lykke.Service.BonusCustomerProfile.MsSqlRepositories
{
    public class BonusCustomerProfileContext : MsSqlContext
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

        protected override void OnLykkeConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnLykkeModelCreating(ModelBuilder modelBuilder)
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
