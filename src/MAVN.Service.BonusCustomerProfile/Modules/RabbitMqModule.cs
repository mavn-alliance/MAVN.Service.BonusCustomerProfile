using System;
using Autofac;
using JetBrains.Annotations;
using Lykke.Common;
using MAVN.Service.BonusCustomerProfile.Settings;
using MAVN.Service.BonusCustomerProfile.DomainServices.Subscribers;
using Lykke.SettingsReader;

namespace MAVN.Service.BonusCustomerProfile.Modules
{
    [UsedImplicitly]
    public class RabbitMqModule : Module
    {
        private readonly AppSettings _appSettings;

        private const string ParticipatedInCampaignExchangeName = "lykke.bonus.participatedincampaign";
        private const string FriendReferralExchangeName = "lykke.bonus.friendreferral";
        private const string VouchersTokensUsedExchange = "lykke.wallet.vouchertokensused";
        private const string CustomerRegistrationExchangeName = "lykke.customer.registration";
        private const string PropertyLeadApprovedReferralExchangeName = "lykke.bonus.propertyleadapprovedreferral";
        private const string OfferToPurchaseByLeadExchangeName = "lykke.bonus.purchasereferral.offertopurchasebylead";
        private const string HotelCheckoutExchangeName = "lykke.partnersintegration.bonuscustomertrigger";
        private const string HotelCheckoutReferralExchangeName = "lykke.bonus.hotelrferral.referralused";

        public RabbitMqModule(IReloadingManager<AppSettings> appSettings)
        {
            _appSettings = appSettings.CurrentValue ?? throw new ArgumentNullException(nameof(appSettings));
        }

        protected override void Load(ContainerBuilder builder)
        {
            var rabbitMqSettings = _appSettings.BonusCustomerProfileService.RabbitMq;

            //RabbitMq Subscribers
            builder.RegisterType<ParticipatedInCampaignSubscriber>()
                .As<IStartStop>()
                .SingleInstance()
                .WithParameter("connectionString", rabbitMqSettings.RabbitMqConnectionString)
                .WithParameter("exchangeName", ParticipatedInCampaignExchangeName);

            builder.RegisterType<FriendReferralSubscriber>()
                .As<IStartStop>()
                .SingleInstance()
                .WithParameter("connectionString", rabbitMqSettings.RabbitMqConnectionString)
                .WithParameter("exchangeName", FriendReferralExchangeName);

            builder.RegisterType<VoucherPurchaseSubscriber>()
                .As<IStartStop>()
                .SingleInstance()
                .WithParameter("connectionString", rabbitMqSettings.RabbitMqConnectionString)
                .WithParameter("exchangeName", VouchersTokensUsedExchange)
                .WithParameter("tokenSymbol", _appSettings.BonusCustomerProfileService.TokenSymbol);

            builder.RegisterType<CustomerRegisteredSubscriber>()
                .As<IStartStop>()
                .SingleInstance()
                .WithParameter("connectionString", rabbitMqSettings.RabbitMqConnectionString)
                .WithParameter("exchangeName", CustomerRegistrationExchangeName);

            builder.RegisterType<PropertyLeadApprovedReferralSubscriber>()
                .As<IStartStop>()
                .SingleInstance()
                .WithParameter("connectionString", rabbitMqSettings.RabbitMqConnectionString)
                .WithParameter("exchangeName", PropertyLeadApprovedReferralExchangeName);

            builder.RegisterType<OfferToPurchaseByLeadSubscriber>()
                .As<IStartStop>()
                .SingleInstance()
                .WithParameter("connectionString", rabbitMqSettings.RabbitMqConnectionString)
                .WithParameter("exchangeName", OfferToPurchaseByLeadExchangeName);

            builder.RegisterType<HotelCheckoutSubscriber>()
                .As<IStartStop>()
                .SingleInstance()
                .WithParameter("connectionString", rabbitMqSettings.PartnersIntegrationConnectionString)
                .WithParameter("exchangeName", HotelCheckoutExchangeName);

            builder.RegisterType<HotelCheckoutReferrerSubscriber>()
                .As<IStartStop>()
                .SingleInstance()
                .WithParameter("connectionString", rabbitMqSettings.RabbitMqConnectionString)
                .WithParameter("exchangeName", HotelCheckoutReferralExchangeName);
        }
    }
}
