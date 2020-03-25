using JetBrains.Annotations;

namespace Lykke.Service.BonusCustomerProfile.Client
{
    /// <summary>
    /// BonusCustomerProfile client interface.
    /// </summary>
    [PublicAPI]
    public interface IBonusCustomerProfileClient
    {
        // Make your app's controller interfaces visible by adding corresponding properties here.
        // NO actual methods should be placed here (these go to controller interfaces, for example - IBonusCustomerProfileApi).
        // ONLY properties for accessing controller interfaces are allowed.

        /// <summary>Campaigns Api interface</summary>
        ICampaignsApi CampaignsApi { get; }

        /// <summary>Customers Api interface</summary>
        ICustomersApi CustomersApi { get; }
    }
}
