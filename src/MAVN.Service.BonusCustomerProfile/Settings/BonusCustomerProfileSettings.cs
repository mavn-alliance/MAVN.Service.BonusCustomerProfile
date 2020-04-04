using JetBrains.Annotations;

namespace MAVN.Service.BonusCustomerProfile.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class BonusCustomerProfileSettings
    {
        public DbSettings Db { get; set; }

        public RabbitMqSettings RabbitMq { get; set; }

        public string BaseCurrencyCode { get; set; }

        public string TokenSymbol { get; set; }
    }
}
