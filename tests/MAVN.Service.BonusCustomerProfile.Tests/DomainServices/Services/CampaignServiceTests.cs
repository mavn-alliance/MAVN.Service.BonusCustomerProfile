using Lykke.Logs;
using MAVN.Service.BonusCustomerProfile.Domain.Models.Campaign;
using MAVN.Service.BonusCustomerProfile.Domain.Repositories;
using MAVN.Service.BonusCustomerProfile.DomainServices.Services;
using MAVN.Service.BonusCustomerProfile.Tests.DomainServices.Mocks;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MAVN.Service.BonusCustomerProfile.Tests.DomainServices.Services
{
    public class CampaignServiceTests
    {
        #region ConstructorTests
        [Fact]
        public void Constructor_CampaignsContributionRepository_ThrowArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new CampaignService(null, EmptyLogFactory.Instance));

            Assert.Equal("campaignsContributionRepository", exception.ParamName);
        }
        #endregion

        #region GetCampaignContributionsIdForCustomerAsync

        [Fact]
        public async Task GetCampaignContributionsIdForCustomerAsync_WhenCorrectCustomerIdPassed_ReturnsValidResponse()
        {
            // Arrange
            var id = CampaignTestData.CustomerId;
            var campaignsContributionRepositoryMock = new Mock<ICampaignsContributionRepository>();
            var service = new CampaignService(campaignsContributionRepositoryMock.Object, EmptyLogFactory.Instance);

            campaignsContributionRepositoryMock.Setup(r => r.GetCampaignContributionsIdForCustomerAsync(It.IsAny<Guid>()))
                .ReturnsAsync(CampaignTestData.CampaignsContributions.Where(c => c.CustomerId == id)
                    .Select(c => c.CampaignId).ToArray());

            //Act
            var result = await service.GetCampaignIdsAsync(id);

            //Assert
            Assert.IsType<ContributionsIdsModel>(result);
            Assert.NotEmpty(result.ContributionIds);
        }
        #endregion

        #region GetCampaignContributionsIdForCustomerAsync

        [Fact]
        public async Task GetIfCampaignContributionExists_WhenValidIdsArePassed()
        {
            // Arrange
            var customerId = CampaignTestData.CustomerId;
            var campaignId = CampaignTestData.CampaignId;
            var campaignsContributionRepositoryMock = new Mock<ICampaignsContributionRepository>();
            var service = new CampaignService(campaignsContributionRepositoryMock.Object, EmptyLogFactory.Instance);

            campaignsContributionRepositoryMock.Setup(r => r.DoesExistAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(true);

            //Act
            var result = await service.DoesCampaignsContributionExistAsync(customerId, campaignId);

            //Assert
            Assert.True(result);
        }
        #endregion

        #region InsertAsync

        [Fact]
        public async Task InsertCampaignContribution_WhenValidDataIsPassed()
        {
            // Arrange
            var customerId = CampaignTestData.CustomerId.ToString("D");
            var campaignId = CampaignTestData.CampaignId.ToString("D");
            var campaignsContributionRepositoryMock = new Mock<ICampaignsContributionRepository>();
            var service = new CampaignService(campaignsContributionRepositoryMock.Object, EmptyLogFactory.Instance);

            campaignsContributionRepositoryMock.Setup(r => r.DoesExistAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(true);

            //Act
            var result = await service.InsertAsync(new CampaignsContributionModel
            {
                CustomerId = customerId,
                CampaignId = campaignId
            });

            //Assert
            campaignsContributionRepositoryMock.Verify(x => x.InsertAsync(It.Is<CampaignsContributionModel>(c => 
                c.CustomerId == customerId && c.CampaignId == campaignId)), Times.Once);
        }
        #endregion
    }
}
