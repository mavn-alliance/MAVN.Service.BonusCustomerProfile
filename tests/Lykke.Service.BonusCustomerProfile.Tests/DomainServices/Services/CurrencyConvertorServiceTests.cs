using Lykke.Logs;
using Lykke.Service.BonusCustomerProfile.Domain.Models.Campaign;
using Lykke.Service.BonusCustomerProfile.Domain.Repositories;
using Lykke.Service.BonusCustomerProfile.DomainServices.Services;
using Lykke.Service.BonusCustomerProfile.Tests.DomainServices.Mocks;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Service.CurrencyConvertor.Client;
using Lykke.Service.CurrencyConvertor.Client.Models.Enums;
using Lykke.Service.CurrencyConvertor.Client.Models.Requests;
using Lykke.Service.CurrencyConvertor.Client.Models.Responses;
using Xunit;

namespace Lykke.Service.BonusCustomerProfile.Tests.DomainServices.Services
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
