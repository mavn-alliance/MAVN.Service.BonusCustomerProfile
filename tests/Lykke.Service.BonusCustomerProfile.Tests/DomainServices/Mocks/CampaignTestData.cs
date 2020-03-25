using System;
using System.Collections.Generic;
using Lykke.Service.BonusCustomerProfile.MsSqlRepositories.Entities;

namespace Lykke.Service.BonusCustomerProfile.Tests.DomainServices.Mocks
{
    public static class CampaignTestData
    {
        public static Guid CustomerId = new Guid("36e5ca2d-e1d6-401f-b4ca-01d62256f5a1");
        public static Guid CampaignId = new Guid("5da67d7f-5be8-4273-b6e7-206abbbabbf4");

        public static List<CampaignsContribution> CampaignsContributions = new List<CampaignsContribution>()
        {
            new CampaignsContribution()
            {
                Id = Guid.NewGuid(),
                CustomerId = CustomerId,
                CampaignId = Guid.NewGuid()
            },
            new CampaignsContribution()
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                CampaignId = Guid.NewGuid()
            }
        };
    }
}
