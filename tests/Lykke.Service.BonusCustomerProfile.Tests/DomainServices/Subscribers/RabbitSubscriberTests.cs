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
    public class RabbitSubscriberTests
    {
        [Fact]
        public async Task ProcessMessageAsync_SomeInvalidIdentifiersArePassed_EventIsNotProcessed()
        {
            var campaignServiceMock = new Mock<ICampaignService>();
            var customerProfileServiceMock = new Mock<ICustomerProfileService>();

            var subscriber = new ParticipatedInCampaignSubscriber(
                "test",
                "test",
                EmptyLogFactory.Instance,
                campaignServiceMock.Object,
                customerProfileServiceMock.Object);

            await subscriber.StartProcessingAsync(new BonusEngine.Contract.Events.ParticipatedInCampaignEvent()
            {
                CampaignId = "not valid",
                CustomerId = Guid.NewGuid().ToString("D")
            });

            campaignServiceMock.Verify(
                c => c.DoesCampaignsContributionExistAsync(It.IsAny<Guid>(), It.IsAny<Guid>()),
                Times.Never);
        }

        [Fact]
        public async Task ProcessMessageAsync_AllIdentifiersInvalidArePassed_EventIsNotProcessed()
        {
            var campaignServiceMock = new Mock<ICampaignService>();
            var customerProfileServiceMock = new Mock<ICustomerProfileService>();

            var subscriber = new ParticipatedInCampaignSubscriber(
                "test",
                "test", 
                EmptyLogFactory.Instance,
                campaignServiceMock.Object, 
                customerProfileServiceMock.Object);

            await subscriber.StartProcessingAsync(new BonusEngine.Contract.Events.ParticipatedInCampaignEvent()
            {
                CampaignId = "not valid",
                CustomerId = "not valid"
            });

            campaignServiceMock.Verify(
                c => c.DoesCampaignsContributionExistAsync(It.IsAny<Guid>(), It.IsAny<Guid>()),
                Times.Never);
        }

        [Fact]
        public async Task ProcessMessageAsync_NotValidIdentifiersPassed_EventIsProcessed()
        {
            var campaignServiceMock = new Mock<ICampaignService>();
            var customerProfileServiceMock = new Mock<ICustomerProfileService>();

            var subscriber = new ParticipatedInCampaignSubscriber(
                "test",
                "test",
                EmptyLogFactory.Instance,
                campaignServiceMock.Object,
                customerProfileServiceMock.Object);

            await subscriber.StartProcessingAsync(new BonusEngine.Contract.Events.ParticipatedInCampaignEvent()
            {
                CampaignId = Guid.NewGuid().ToString("D"),
                CustomerId = Guid.NewGuid().ToString("D")
            });

            campaignServiceMock.Verify(
                c => c.DoesCampaignsContributionExistAsync(It.IsAny<Guid>(), It.IsAny<Guid>()),
                Times.Once);
        }
    }
}
