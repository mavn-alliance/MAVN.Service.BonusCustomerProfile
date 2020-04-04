using System;
using System.Threading.Tasks;
using MAVN.Service.BonusCustomerProfile.Domain.Models.Campaign;

namespace MAVN.Service.BonusCustomerProfile.Domain.Repositories
{
    public interface ICampaignsContributionRepository
    {
        Task<Guid[]> GetCampaignContributionsIdForCustomerAsync(Guid customerId);

        Task<Guid> InsertAsync(CampaignsContributionModel campaign);

        Task<bool> DoesExistAsync(Guid campaignId, Guid customerId);
    }
}
