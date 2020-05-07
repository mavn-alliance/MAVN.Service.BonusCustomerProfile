using JetBrains.Annotations;
using Lykke.Sdk.Settings;
using MAVN.Service.CurrencyConvertor.Client;

namespace MAVN.Service.BonusCustomerProfile.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings : BaseAppSettings
    {
        public BonusCustomerProfileSettings BonusCustomerProfileService { get; set; }

        public CurrencyConvertorServiceClientSettings CurrencyConvertorServiceClient { get; set; }
    }
}
