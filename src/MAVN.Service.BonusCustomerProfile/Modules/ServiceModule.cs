using Autofac;
using JetBrains.Annotations;
using Lykke.Sdk;
using MAVN.Service.BonusCustomerProfile.Domain.Services;
using MAVN.Service.BonusCustomerProfile.DomainServices.Services;
using MAVN.Service.BonusCustomerProfile.MsSqlRepositories;
using MAVN.Service.BonusCustomerProfile.Services;
using MAVN.Service.BonusCustomerProfile.Settings;
using MAVN.Service.CurrencyConvertor.Client;
using Lykke.SettingsReader;

namespace MAVN.Service.BonusCustomerProfile.Modules
{
    [UsedImplicitly]
    public class ServiceModule : Module
    {
        private readonly IReloadingManager<AppSettings> _appSettings;

        public ServiceModule(IReloadingManager<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(
                new AutofacModule(_appSettings.CurrentValue.BonusCustomerProfileService.Db.DataConnectionString));

            builder.RegisterCurrencyConvertorClient(_appSettings.CurrentValue.CurrencyConvertorServiceClient);

            // Services
            builder.RegisterType<CampaignService>()
                .As<ICampaignService>()
                .SingleInstance();

            builder.RegisterType<CustomerProfileService>()
                .As<ICustomerProfileService>()
                .SingleInstance();

            builder.RegisterType<VoucherOperationsService>()
                .As<IVoucherOperationsService>()
                .SingleInstance();

            builder.RegisterType<CurrencyConvertorService>()
                .As<ICurrencyConvertorService>()
                .WithParameter("assetName", _appSettings.CurrentValue.BonusCustomerProfileService.BaseCurrencyCode)
                .SingleInstance();

            // Managers
            builder.RegisterType<StartupManager>()
                .As<IStartupManager>()
                .SingleInstance()
                .AutoActivate();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>()
                .SingleInstance()
                .AutoActivate();
        }
    }
}
