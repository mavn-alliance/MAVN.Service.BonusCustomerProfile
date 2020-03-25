using Lykke.Logs;
using Lykke.Service.BonusCustomerProfile.Domain.Services;
using Lykke.Service.BonusCustomerProfile.DomainServices.Subscribers;
using Moq;
using System;
using System.Threading.Tasks;
using Lykke.Service.BonusCustomerProfile.Domain.Models.Campaign;
using Xunit;

namespace Lykke.Service.BonusCustomerProfile.Tests.DomainServices.Subscribers
{
    public class ParticipatedInCampaignSubscriberTests
    {
        #region ConstructorTests
        [Fact]
        public void Constructor_CampaignServiceNull_ThrowArgumentNullException()
        {
            var customerProfileServiceMock = new Mock<ICustomerProfileService>();

            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ParticipatedInCampaignSubscriber("test", "test", EmptyLogFactory.Instance, null, customerProfileServiceMock.Object));

            Assert.Equal("campaignService", exception.ParamName);
        }

        [Fact]
        public void Constructor_CustomerProfileServiceNull_ThrowArgumentNullException()
        {
            var campaignServiceMock = new Mock<ICampaignService>();

            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ParticipatedInCampaignSubscriber("test", "test", EmptyLogFactory.Instance, campaignServiceMock.Object, null));

            Assert.Equal("customerProfileService", exception.ParamName);
        }
        #endregion

        #region ProcessMessageAsync
        [Fact]
        public async Task ProcessMessageAsync_CampaignsContributionExists_CustomerProfileNotUpdated()
        {
            var campaignServiceMock = new Mock<ICampaignService>();
            var customerProfileServiceMock = new Mock<ICustomerProfileService>();

            campaignServiceMock.Setup(c => c.DoesCampaignsContributionExistAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(true);
            campaignServiceMock.Setup(c => c.InsertAsync(It.IsAny<CampaignsContributionModel>()))
                .ReturnsAsync(Guid.NewGuid().ToString());
            customerProfileServiceMock.Setup(c => c.ProcessParticipatedInCampaignEvent(It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);

            var subscriber = new ParticipatedInCampaignSubscriber("test", "test",
                EmptyLogFactory.Instance, campaignServiceMock.Object,
                customerProfileServiceMock.Object);

            await subscriber.ProcessMessageAsync(new BonusEngine.Contract.Events.ParticipatedInCampaignEvent()
            {
                CampaignId = Guid.NewGuid().ToString(),
                CustomerId = Guid.NewGuid().ToString()
            });

            campaignServiceMock.Verify(c => c.InsertAsync(It.IsAny<CampaignsContributionModel>()),
                Times.Never);
        }

        [Fact]
        public async Task ProcessMessageAsync_CampaignsContributionDoesNotExist_CustomerProfileNotUpdated()
        {
            var campaignServiceMock = new Mock<ICampaignService>(); 
            var customerProfileServiceMock = new Mock<ICustomerProfileService>();

            campaignServiceMock.Setup(c => c.DoesCampaignsContributionExistAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(false);

            campaignServiceMock.Setup(c => c.InsertAsync(It.IsAny< CampaignsContributionModel>()))
                .ReturnsAsync(Guid.NewGuid().ToString());
            customerProfileServiceMock.Setup(c => c.ProcessParticipatedInCampaignEvent(It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);

            var subscriber = new ParticipatedInCampaignSubscriber("test", "test",
                EmptyLogFactory.Instance, campaignServiceMock.Object,
                customerProfileServiceMock.Object);

            await subscriber.ProcessMessageAsync(new BonusEngine.Contract.Events.ParticipatedInCampaignEvent()
            {
                CampaignId = Guid.NewGuid().ToString(),
                CustomerId = Guid.NewGuid().ToString()
            });

            campaignServiceMock.Verify(c => c.InsertAsync(It.IsAny<CampaignsContributionModel>()),
                Times.Once);

            customerProfileServiceMock.Verify(c => c.ProcessParticipatedInCampaignEvent(It.IsAny<Guid>()),
                Times.Once);
        }
        #endregion
    }
}
