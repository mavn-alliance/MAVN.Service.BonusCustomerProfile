using Autofac;
using Lykke.Common.MsSql;
using Lykke.Service.BonusCustomerProfile.Domain.Repositories;
using Lykke.Service.BonusCustomerProfile.MsSqlRepositories.Repositories;

namespace Lykke.Service.BonusCustomerProfile.MsSqlRepositories
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
            builder.RegisterMsSql(_connectionString,
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
