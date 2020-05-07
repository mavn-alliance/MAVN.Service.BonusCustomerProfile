using System.Threading.Tasks;
using Lykke.Logs;
using MAVN.Service.BonusCustomerProfile.DomainServices.Services;
using MAVN.Service.CurrencyConvertor.Client;
using MAVN.Service.CurrencyConvertor.Client.Models.Enums;
using MAVN.Service.CurrencyConvertor.Client.Models.Responses;
using Moq;
using Xunit;

namespace MAVN.Service.BonusCustomerProfile.Tests.DomainServices.Services
{
    public class CurrencyConvertorServiceTests
    {
        [Fact]
        public async Task GetCampaignContributionsIdForCustomerAsync_WhenCorrectCustomerIdPassed_ReturnsValidResponse()
        {
            // Arrange
            var currencyConvertorClientMock = new Mock<ICurrencyConvertorClient>();

            var service = new CurrencyConvertorService(currencyConvertorClientMock.Object, "USD", EmptyLogFactory.Instance);

            currencyConvertorClientMock.Setup(r => r.Converter.ConvertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>()))
                .ReturnsAsync(new ConverterResponse
                {
                    ErrorCode = ConverterErrorCode.None,
                    Amount = 12
                });

            //Act
            var result = await service.CovertToBaseCurrencyAsync(12, "USD");

            //Assert
            Assert.Equal(12, result.amount);
        }
    }
}
