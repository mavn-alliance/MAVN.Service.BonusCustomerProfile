using MAVN.Service.BonusCustomerProfile.Domain.Models.Campaign;
using System;
using System.Threading.Tasks;

namespace MAVN.Service.BonusCustomerProfile.Domain.Services
{
    public interface ICampaignService
    {
        Task<ContributionsIdsModel> GetCampaignIdsAsync(Guid customerId);

        Task<string> InsertAsync(CampaignsContributionModel campaign);

        Task<bool> DoesCampaignsContributionExistAsync(Guid customerId, Guid campaignId);
    }
}
