using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.BonusCustomerProfile.Settings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }

        public string DataConnectionString { get; internal set; }
    }
}
