using Common.Log;
using Lykke.Common.Log;
using MAVN.Service.BonusCustomerProfile.Domain.Models.Campaign;
using MAVN.Service.BonusCustomerProfile.Domain.Repositories;
using MAVN.Service.BonusCustomerProfile.Domain.Services;
using System;
using System.Threading.Tasks;

namespace MAVN.Service.BonusCustomerProfile.DomainServices.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignsContributionRepository _campaignsContributionRepository;

        private readonly ILog _log;

        public CampaignService(ICampaignsContributionRepository campaignsContributionRepository, ILogFactory logFactory)
        {
            _campaignsContributionRepository = campaignsContributionRepository ?? throw new ArgumentNullException(nameof(campaignsContributionRepository));
            _log = logFactory.CreateLog(this);
        }

        public async Task<ContributionsIdsModel> GetCampaignIdsAsync(Guid customerId)
        {
            var contributionsIds = await _campaignsContributionRepository.GetCampaignContributionsIdForCustomerAsync(customerId);

            return new ContributionsIdsModel
            {
                ContributionIds = contributionsIds
            };
        }

        public async Task<string> InsertAsync(CampaignsContributionModel campaign)
        {
            var campaignsContributionId = await _campaignsContributionRepository.InsertAsync(campaign);

            _log.Info("Campaign was added", campaign);

            return campaignsContributionId.ToString("D");
        }

        public async Task<bool> DoesCampaignsContributionExistAsync(Guid customerId, Guid campaignId)
        {
            return await _campaignsContributionRepository.DoesExistAsync(campaignId, customerId);
        }
    }
}
