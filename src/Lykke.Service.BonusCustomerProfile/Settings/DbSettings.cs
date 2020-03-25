using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.BonusCustomerProfile.Settings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }

        public string DataConnectionString { get; internal set; }
    }
}
