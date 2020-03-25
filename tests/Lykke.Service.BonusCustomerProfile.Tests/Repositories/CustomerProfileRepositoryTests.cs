using System;
using System.Threading.Tasks;
using Lykke.Common.MsSql;
using Lykke.Service.BonusCustomerProfile.Domain.Models.CustomerProfile;
using Lykke.Service.BonusCustomerProfile.MsSqlRepositories;
using Lykke.Service.BonusCustomerProfile.MsSqlRepositories.Repositories;
using Lykke.Service.BonusCustomerProfile.Tests.Repositories.Fixtures;
using Xunit;

namespace Lykke.Service.BonusCustomerProfile.Tests.Repositories
{
    public class CustomerProfileRepositoryTests : IClassFixture<BonusCustomerProfileContextFixture>
    {
        private readonly CustomerProfileRepository _customerProfileRepository;

        public CustomerProfileRepositoryTests()
        {
            var mapper = MapperHelper.CreateAutoMapper();

            var contextFixture = new BonusCustomerProfileContextFixture();

            var bonusCustomerProfileContext = contextFixture.BonusCustomerProfileContext;

            var msSqlContextFactory = new MsSqlContextFactory<BonusCustomerProfileContext>(
                dbCtxOptions => bonusCustomerProfileContext, contextFixture.DbContextOptions);

            _customerProfileRepository = new CustomerProfileRepository(msSqlContextFactory, mapper);
        }

        #region CreateOrUpdateAsync
        [Fact]
        public void ShouldUpdateWhenDbUpdateExceptionIsThrown_WhenCreateOrUpdateAsyncCalled()
        {
            //Act
            async Task CreateOrUpdate()
            {
                await _customerProfileRepository.CreateOrUpdateAsync(new CustomerProfileModel()
                {
                    CustomerId = new Guid("57ec267c-2a4f-41f9-9c2c-3cc10559b0aa"),
                    TotalCampaignsContributedCount = 2
                });
            }

            //Assert that there are DbUpdateException
            var ex = Record.ExceptionAsync(CreateOrUpdate);
            Assert.NotNull(ex.Result);
        }

        [Fact]
        public void ShouldInsertWithoutExceptions_WhenCreateOrUpdateAsyncCalled()
        {
            //Act
            async Task CreateOrUpdate()
            {
                await _customerProfileRepository.CreateOrUpdateAsync(new CustomerProfileModel()
                {
                    //not existing customer
                    CustomerId = new Guid("57ec267c-2a4f-41f9-9c2c-3cc10559b0a3"),
                    TotalCampaignsContributedCount = 1
                });
            }

            //Assert if there are no exceptions when inserting
            var ex = Record.ExceptionAsync(CreateOrUpdate);
            Assert.Null(ex.Result);
        }
        #endregion
    }
}
