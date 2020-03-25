using System;
using System.Threading.Tasks;
using Lykke.Logs;
using Lykke.Service.BonusCustomerProfile.Domain.Models.Vouchers;
using Lykke.Service.BonusCustomerProfile.Domain.Services;
using Lykke.Service.BonusCustomerProfile.DomainServices.Services;
using Moq;
using Xunit;

namespace Lykke.Service.BonusCustomerProfile.Tests.DomainServices.Services
{
    public class VoucherOperationsServiceTest
    {
        [Fact]
        public async Task When_CurrencyConvertorClientReturnsInvalidResponse_Should_NotCallInsertOrUpdateCustomerPurchaseAmount()
        {
            //Arrange
            var purchaseModel = new VoucherPurchaseModel();

            var customerProfileServiceMock = new Mock<ICustomerProfileService>(MockBehavior.Strict);
            customerProfileServiceMock.Setup(x => x.InsertOrUpdateCustomerPurchaseAmount(It.IsAny<Guid>(), It.IsAny<decimal>()))
                .Returns(Task.CompletedTask);

            var currencyConvertorClientMock = new Mock<ICurrencyConvertorService>(MockBehavior.Strict);
            currencyConvertorClientMock.Setup(x => x.CovertToBaseCurrencyAsync(It.IsAny<decimal>(), It.IsAny<string>()))
                .ReturnsAsync((false, 0));

            var service = new VoucherOperationsService(customerProfileServiceMock.Object, currencyConvertorClientMock.Object, EmptyLogFactory.Instance);

            //Act
            await service.ProcessVoucherPurchaseEvent(purchaseModel);

            //Assert
            customerProfileServiceMock.Verify(
                x => x.InsertOrUpdateCustomerPurchaseAmount(It.IsAny<Guid>(), It.IsAny<decimal>()), 
                Times.Never);
        }

        [Fact]
        public async Task When_CurrencyConvertorClientReturnsValidResponse_Should_CallInsertOrUpdateCustomerPurchaseAmount()
        {
            //Arrange
            var purchaseModel = new VoucherPurchaseModel();

            var customerProfileServiceMock = new Mock<ICustomerProfileService>(MockBehavior.Strict);
            customerProfileServiceMock
                .Setup(x => x.InsertOrUpdateCustomerPurchaseAmount(It.IsAny<Guid>(), It.IsAny<decimal>()))
                .Returns(Task.CompletedTask);

            var currencyConvertorClientMock = new Mock<ICurrencyConvertorService>(MockBehavior.Strict);
            currencyConvertorClientMock.Setup(x => x.CovertToBaseCurrencyAsync(It.IsAny<decimal>(), It.IsAny<string>()))
                .ReturnsAsync((true, 1));

            var service = new VoucherOperationsService(customerProfileServiceMock.Object, currencyConvertorClientMock.Object, EmptyLogFactory.Instance);

            //Act
            await service.ProcessVoucherPurchaseEvent(purchaseModel);

            //Assert
            customerProfileServiceMock.Verify(
                x => x.InsertOrUpdateCustomerPurchaseAmount(It.IsAny<Guid>(), It.IsAny<decimal>()), 
                Times.Once);
        }
    }
}
