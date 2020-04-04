using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lykke.Common.MsSql;
using MAVN.Service.BonusCustomerProfile.Domain.Models.Campaign;
using MAVN.Service.BonusCustomerProfile.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.BonusCustomerProfile.MsSqlRepositories.Repositories
{
    public class CampaignsContributionRepository : ICampaignsContributionRepository
    {
        private readonly MsSqlContextFactory<BonusCustomerProfileContext> _msSqlContextFactory;
        private readonly IMapper _mapper;

        public CampaignsContributionRepository(
            MsSqlContextFactory<BonusCustomerProfileContext> msSqlContextFactory,
            IMapper mapper)
        {
            _msSqlContextFactory = msSqlContextFactory 
                                   ?? throw new ArgumentNullException(nameof(msSqlContextFactory));
            _mapper = mapper;
        }

        public async Task<Guid[]> GetCampaignContributionsIdForCustomerAsync(Guid customerId)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                return await context.CampaignsContributions
                    .AsNoTracking()
                    .Where(c => c.CustomerId == customerId)
                    .Select(c => c.CampaignId).ToArrayAsync();
            }
        }

        public async Task<Guid> InsertAsync(CampaignsContributionModel campaign)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                var entity = _mapper.Map<Entities.CampaignsContribution>(campaign);

                context.Add(entity);

                await context.SaveChangesAsync();

                return entity.Id;
            }
        }

        public async Task<bool> DoesExistAsync(Guid campaignId, Guid customerId)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                return await context.CampaignsContributions
                    .AsNoTracking()
                    .AnyAsync(c => c.CustomerId == customerId && c.CampaignId == campaignId);
            }
        }
    }
}
