using Autofac;
using MAVN.Persistence.PostgreSQL.Legacy;
using MAVN.Service.BonusCustomerProfile.Domain.Repositories;
using MAVN.Service.BonusCustomerProfile.MsSqlRepositories.Repositories;

namespace MAVN.Service.BonusCustomerProfile.MsSqlRepositories
{
    public class AutofacModule : Module
    {
        private readonly string _connectionString;

        public AutofacModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterPostgreSQL(_connectionString,
                connectionString => new BonusCustomerProfileContext(connectionString, false),
                dbConnection => new BonusCustomerProfileContext(dbConnection));
            
            builder.RegisterType<CampaignsContributionRepository>()
                .As<ICampaignsContributionRepository>()
                .SingleInstance();

            builder.RegisterType<CustomerProfileRepository>()
                .As<ICustomerProfileRepository>()
                .SingleInstance();
        }
    }
}
