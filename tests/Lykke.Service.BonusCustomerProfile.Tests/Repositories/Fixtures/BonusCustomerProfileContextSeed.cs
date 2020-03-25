using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using Lykke.Service.BonusCustomerProfile.MsSqlRepositories;
using Lykke.Service.BonusCustomerProfile.MsSqlRepositories.Entities;

namespace Lykke.Service.BonusCustomerProfile.Tests.Repositories.Fixtures
{
    public static class BonusCustomerProfileContextSeed
    {
        public const string CustomerId = "57ec267c-2a4f-41f9-9c2c-3cc10559b0aa";
        public const string CampaignId = "3282f4bf-67cd-4943-b66b-2d8dbd66ccd2";

        public static void Seed(BonusCustomerProfileContext context)
        {
            if (!context.CustomerProfiles.Any())
            {
                var customerProfiles = new List<CustomerProfile>
                {
                    new CustomerProfile()
                    {
                         CustomerId = new Guid(CustomerId),
                         TotalCampaignsContributedCount = 1
                    }
                };

                context.CustomerProfiles.AddRange(customerProfiles);
                context.SaveChangesAsync();
            }

            if (!context.CampaignsContributions.Any())
            {
                var campaigns = new List<CampaignsContribution>
                {
                    new CampaignsContribution()
                    {
                         Id= Guid.NewGuid(),
                          CustomerId = new Guid(CustomerId),
                          CampaignId = new Guid(CampaignId)
                    }
                };

                context.CampaignsContributions.AddRange(campaigns);
                context.SaveChangesAsync();
            }
        }
    }
}
