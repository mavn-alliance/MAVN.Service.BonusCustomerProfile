using JetBrains.Annotations;
using Refit;
using System;
using System.Threading.Tasks;
using Lykke.Service.BonusCustomerProfile.Client.Models.Campaigns;

namespace Lykke.Service.BonusCustomerProfile.Client
{
    /// <summary>
    /// Campaign API interface.
    /// </summary>
    [PublicAPI]
    public interface ICampaignsApi
    {
        /// <summary>
        /// Returns a contributions' ids array by customerId.
        /// </summary>
        /// <param name="customerId">Falcon customer's id</param>
        /// <returns>ContributionsIdsResponseModel</returns>
        [Get("/api/campaigns/contributions/{customerId}")]
        Task<ContributionsIdsResponseModel> GetContributionsByCustomerIdAsync(string customerId);
    }
}
