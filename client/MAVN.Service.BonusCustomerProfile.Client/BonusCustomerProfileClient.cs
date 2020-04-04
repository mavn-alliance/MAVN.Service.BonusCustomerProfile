using Lykke.HttpClientGenerator;

namespace MAVN.Service.BonusCustomerProfile.Client
{
    /// <summary>
    /// BonusCustomerProfile API aggregating interface.
    /// </summary>
    public class BonusCustomerProfileClient : IBonusCustomerProfileClient
    {
        // Note: Add similar Api properties for each new service controller

        /// <inheritdoc />
        /// <summary>Campaigns Api interface</summary>
        public ICampaignsApi CampaignsApi { get; private set; }
        
        /// <inheritdoc />
        /// <summary>Customers Api interface</summary>
        public ICustomersApi CustomersApi { get; private set; }

        /// <summary>C-tor</summary>
        public BonusCustomerProfileClient(IHttpClientGenerator httpClientGenerator)
        {
            CampaignsApi = httpClientGenerator.Generate<ICampaignsApi>();
            CustomersApi = httpClientGenerator.Generate<ICustomersApi>();
        }
    }
}
