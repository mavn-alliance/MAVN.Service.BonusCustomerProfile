using JetBrains.Annotations;
using Refit;
using System.Threading.Tasks;
using Lykke.Service.BonusCustomerProfile.Client.Models.Customers;

namespace Lykke.Service.BonusCustomerProfile.Client
{
    /// <summary>
    /// Friend Referral API interface.
    /// </summary>
    [PublicAPI]
    public interface ICustomersApi
    {
        /// <summary>
        /// Returns bonus profile for customer.
        /// </summary>
        /// <param name="customerId">Falcon customer bonus profile</param>
        /// <returns>CustomerResponseModel</returns>
        [Get("/api/customers/{customerId}")]
        Task<CustomerResponseModel> GetCustomerAsync(string customerId);
    }
}
