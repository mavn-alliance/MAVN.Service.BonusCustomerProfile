using Lykke.Logs;
using MAVN.Service.BonusCustomerProfile.Domain.Models.CustomerProfile;
using MAVN.Service.BonusCustomerProfile.Domain.Repositories;
using MAVN.Service.BonusCustomerProfile.DomainServices.Services;
using MAVN.Service.BonusCustomerProfile.Tests.DomainServices.Mocks;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using MAVN.Service.BonusCustomerProfile.Domain.Services;
using Xunit;

namespace MAVN.Service.BonusCustomerProfile.Tests.DomainServices.Services
{
    public class CustomerProfileServiceTests
    {
        private readonly Mock<ICustomerProfileRepository> _customerProfileRepositoryMock;
        private readonly Mock<ICurrencyConvertorService> _convertCurrencyServiceMock;

        public CustomerProfileServiceTests()
        {
            _customerProfileRepositoryMock = new Mock<ICustomerProfileRepository>(MockBehavior.Strict);
            _convertCurrencyServiceMock = new Mock<ICurrencyConvertorService>(MockBehavior.Strict);
        }

        #region ConstructorTests
        [Fact]
        public void Constructor_CampaignsContributionRepositoryNotPassed_ThrowArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new CustomerProfileService(null, EmptyLogFactory.Instance, _convertCurrencyServiceMock.Object));

            Assert.Equal("customerProfileRepository", exception.ParamName);
        }

        [Fact]
        public void Constructor_CurrencyConvertorClientNotPassed_ThrowArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new CustomerProfileService(_customerProfileRepositoryMock.Object, EmptyLogFactory.Instance, null));

            Assert.Equal("currencyConvertorService", exception.ParamName);
        }
        #endregion

        #region ProcessParticipatedInCampaignEvent

        [Fact]
        public async Task ProcessParticipatedInCampaignEvent_WhenCustomerProfileDoesNotExist_InsertNewOne()
        {
            //Arrange
            var service = new CustomerProfileService(_customerProfileRepositoryMock.Object, EmptyLogFactory.Instance, _convertCurrencyServiceMock.Object);
            var customerId = Guid.NewGuid();

            _customerProfileRepositoryMock.Setup(c => c.GetCustomerProfileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(CustomerProfileTestData.CustomerProfileModels.FirstOrDefault(c =>
                    c.CustomerId == customerId));

            _customerProfileRepositoryMock.Setup(x => x.CreateOrUpdateAsync(It.IsAny<CustomerProfileModel>()))
                .Returns(Task.CompletedTask);
            //Act
            await service.ProcessParticipatedInCampaignEvent(Guid.Parse(CustomerProfileTestData.CustomerId));

            //Assert
            _customerProfileRepositoryMock.Verify(c => c.CreateOrUpdateAsync(It.IsAny<CustomerProfileModel>()));
        }

        [Fact]
        public async Task ProcessParticipatedInCampaignEvent_WhenCustomerProfileExists_UpdateTotalCampaignsContributedCount()
        {
            //Arrange
            var service = new CustomerProfileService(_customerProfileRepositoryMock.Object, EmptyLogFactory.Instance, _convertCurrencyServiceMock.Object);
            var customerId = Guid.Parse(CustomerProfileTestData.CustomerId);

            _customerProfileRepositoryMock.Setup(c => c.GetCustomerProfileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(CustomerProfileTestData.CustomerProfileModels.FirstOrDefault(c =>
                    c.CustomerId == customerId));

            _customerProfileRepositoryMock.Setup(x => x.CreateOrUpdateAsync(It.IsAny<CustomerProfileModel>()))
                .Returns(Task.CompletedTask);
            //Act
            await service.ProcessParticipatedInCampaignEvent(customerId);

            //Assert
            _customerProfileRepositoryMock.Verify(c => c.CreateOrUpdateAsync(It.IsAny<CustomerProfileModel>()));
        }
        #endregion

        #region ProcessFriendReferralEvent

        [Fact]
        public async Task ProcessFriendReferralEvent_WhenSuchProfileDoesNotExist()
        {
            //Arrange
            _customerProfileRepositoryMock.Setup(x => x.GetCustomerProfileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(default(CustomerProfileModel));
            _customerProfileRepositoryMock.Setup(x => x.CreateOrUpdateAsync(It.IsAny<CustomerProfileModel>()))
                .Returns(Task.CompletedTask);

            var service = new CustomerProfileService(_customerProfileRepositoryMock.Object, EmptyLogFactory.Instance, _convertCurrencyServiceMock.Object);
            var rightGuid = Guid.NewGuid();

            //Act
            await service.ProcessFriendReferralEvent(rightGuid);

            //Assert
            _customerProfileRepositoryMock.Verify(x => x.GetCustomerProfileAsync(It.IsAny<Guid>()), Times.Once);
            _customerProfileRepositoryMock.Verify(x => x.CreateOrUpdateAsync(It.Is<CustomerProfileModel>(c =>
                c.TotalReferredFriendCount == 1)), Times.Once);
            _customerProfileRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<CustomerProfileModel>()), Times.Never);
        }

        [Fact]
        public async Task ProcessFriendReferralEvent_WhenSuchProfileExist()
        {
            //Arrange
            _customerProfileRepositoryMock.Setup(x => x.GetCustomerProfileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new CustomerProfileModel { TotalReferredFriendCount = 1 });
            _customerProfileRepositoryMock.Setup(x => x.CreateOrUpdateAsync(It.IsAny<CustomerProfileModel>()))
                .Returns(Task.CompletedTask);

            var service = new CustomerProfileService(_customerProfileRepositoryMock.Object, EmptyLogFactory.Instance, _convertCurrencyServiceMock.Object);
            var rightGuid = Guid.NewGuid();

            //Act
            await service.ProcessFriendReferralEvent(rightGuid);

            //Assert
            _customerProfileRepositoryMock.Verify(x => x.GetCustomerProfileAsync(It.IsAny<Guid>()), Times.Once);
            _customerProfileRepositoryMock.Verify(x => x.CreateOrUpdateAsync(It.Is<CustomerProfileModel>(c =>
                c.TotalReferredFriendCount == 2)), Times.Once);
        }
        #endregion

        #region InsertOrUpdateCustomerPurchaseAmount

        [Fact]
        public async Task InsertCustomerPurchaseAmount_WhenSuchProfileDoesNotExist()
        {
            //Arrange
            _customerProfileRepositoryMock.Setup(x => x.GetCustomerProfileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(default(CustomerProfileModel));
            _customerProfileRepositoryMock.Setup(x => x.CreateOrUpdateAsync(It.IsAny<CustomerProfileModel>()))
                .Returns(Task.CompletedTask);

            var service = new CustomerProfileService(_customerProfileRepositoryMock.Object, EmptyLogFactory.Instance, _convertCurrencyServiceMock.Object);

            //Act
            await service.InsertOrUpdateCustomerPurchaseAmount(Guid.NewGuid(), 50);

            //Assert
            _customerProfileRepositoryMock.Verify(x => x.GetCustomerProfileAsync(It.IsAny<Guid>()), Times.Once);
            _customerProfileRepositoryMock.Verify(x => x.CreateOrUpdateAsync(It.Is<CustomerProfileModel>(c => c.TotalPurchasedAmount == 50)), Times.Once);
            _customerProfileRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<CustomerProfileModel>()), Times.Never);
        }

        [Fact]
        public async Task UpdateCustomerPurchaseAmount_WhenSuchProfileExists()
        {
            //Arrange
            _customerProfileRepositoryMock.Setup(x => x.GetCustomerProfileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new CustomerProfileModel
                {
                    TotalPurchasedAmount = 20
                });
            _customerProfileRepositoryMock.Setup(x => x.CreateOrUpdateAsync(It.IsAny<CustomerProfileModel>()))
                .Returns(Task.CompletedTask);

            var service = new CustomerProfileService(_customerProfileRepositoryMock.Object, EmptyLogFactory.Instance, _convertCurrencyServiceMock.Object);

            //Act
            await service.InsertOrUpdateCustomerPurchaseAmount(Guid.NewGuid(), 50);

            //Assert
            _customerProfileRepositoryMock.Verify(x => x.GetCustomerProfileAsync(It.IsAny<Guid>()), Times.Once);
            _customerProfileRepositoryMock.Verify(x => x.CreateOrUpdateAsync(It.Is<CustomerProfileModel>(c => c.TotalPurchasedAmount == 70)), Times.Once);
        }

        #endregion
    }
}
