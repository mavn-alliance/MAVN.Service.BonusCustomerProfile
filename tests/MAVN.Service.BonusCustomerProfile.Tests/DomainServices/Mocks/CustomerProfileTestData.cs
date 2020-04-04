using MAVN.Service.BonusCustomerProfile.Domain.Models.CustomerProfile;
using System;
using System.Collections.Generic;

namespace MAVN.Service.BonusCustomerProfile.Tests.DomainServices.Mocks
{
    public static class CustomerProfileTestData
    {
        public const string CustomerId = "36e5ca2d-e1d6-401f-b4ca-01d62256f5a1";

        public static List<CustomerProfileModel> CustomerProfileModels = new List<CustomerProfileModel>()
        {
            new CustomerProfileModel()
            {
                CustomerId = Guid.Parse(CustomerId),
                TotalCampaignsContributedCount = 0
            }
        };
    }
}
