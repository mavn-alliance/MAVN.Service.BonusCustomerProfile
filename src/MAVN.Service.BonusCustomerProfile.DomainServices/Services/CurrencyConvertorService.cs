using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using MAVN.Service.BonusCustomerProfile.Domain.Services;
using MAVN.Service.CurrencyConvertor.Client;
using MAVN.Service.CurrencyConvertor.Client.Models.Enums;

namespace MAVN.Service.BonusCustomerProfile.DomainServices.Services
{
    public class CurrencyConvertorService : ICurrencyConvertorService
    {
        private readonly ICurrencyConvertorClient _currencyConverterClient;
        private readonly string _assetName;
        private readonly ILog _log;

        public CurrencyConvertorService(
            ICurrencyConvertorClient currencyConverterClient,
            string assetName,
            ILogFactory logFactory)
        {
            _currencyConverterClient = currencyConverterClient;
            _assetName = assetName;
            _log = logFactory.CreateLog(this);
        }

        public async Task<(bool isValid, decimal amount)> CovertToBaseCurrencyAsync(decimal amount, string currency)
        {
            var response = await _currencyConverterClient.Converter
                .ConvertAsync(currency, _assetName, amount);

            if (response.ErrorCode != ConverterErrorCode.None)
            {
                _log.Error(message: "An error occured while converting currency amount.",
                    context: $"from: {currency}; to: {_assetName}; error: {response.ErrorCode}");

                return (false, 0);
            }

            return (true, response.Amount);
        }
    }
}
