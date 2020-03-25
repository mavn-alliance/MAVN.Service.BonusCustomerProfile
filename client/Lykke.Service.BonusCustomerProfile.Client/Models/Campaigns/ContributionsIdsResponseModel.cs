using System;

namespace Lykke.Service.BonusCustomerProfile.Client.Models.Campaigns
{
    /// <summary>
    /// Represents response model with contribution ids array
    /// </summary>
    public class ContributionsIdsResponseModel : BonusEngineErrorResponseModel
    {
        /// <summary>
        /// Represents array of contribution ids
        /// </summary>
        public Guid[] ContributionIds { get; set; }
    }
}
