using JetBrains.Annotations;
using Refit;
using System;
using System.Threading.Tasks;
using MAVN.Service.BonusCustomerProfile.Client.Models.Campaigns;

namespace MAVN.Service.BonusCustomerProfile.Client
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
