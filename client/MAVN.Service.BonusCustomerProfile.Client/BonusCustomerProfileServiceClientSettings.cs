using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.BonusCustomerProfile.Client 
{
    /// <summary>
    /// BonusCustomerProfile client settings.
    /// </summary>
    public class BonusCustomerProfileServiceClientSettings 
    {
        /// <summary>Service url.</summary>
        [HttpCheck("api/isalive")]
        public string ServiceUrl {get; set;}
    }
}
