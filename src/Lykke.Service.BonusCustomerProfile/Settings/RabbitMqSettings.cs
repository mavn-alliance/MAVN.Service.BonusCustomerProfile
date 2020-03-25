using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.BonusCustomerProfile.Settings
{
    public class RabbitMqSettings
    {
        [AmqpCheck]
        public string RabbitMqConnectionString { get; set; }

        [AmqpCheck]
        public string PartnersIntegrationConnectionString { get; set; }

        [AmqpCheck]
        public string ReferralRabbitConnectionString { get; set; }
    }
}
