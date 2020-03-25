using Lykke.Service.BonusCustomerProfile.Domain.Models.Campaign;
using System;
using System.Threading.Tasks;

namespace Lykke.Service.BonusCustomerProfile.Domain.Services
{
    public interface ICampaignService
    {
        Task<ContributionsIdsModel> GetCampaignIdsAsync(Guid customerId);

        Task<string> InsertAsync(CampaignsContributionModel campaign);

        Task<bool> DoesCampaignsContributionExistAsync(Guid customerId, Guid campaignId);
    }
}
