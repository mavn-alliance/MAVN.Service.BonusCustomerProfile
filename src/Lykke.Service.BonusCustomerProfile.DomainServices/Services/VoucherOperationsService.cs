using Common.Log;
using Lykke.Common.Log;
using Lykke.Service.BonusCustomerProfile.Domain.Services;
using System;
using System.Threading.Tasks;
using Lykke.Service.BonusCustomerProfile.Domain.Models.Vouchers;

namespace Lykke.Service.BonusCustomerProfile.DomainServices.Services
{
    public class VoucherOperationsService : IVoucherOperationsService
    {
        private readonly ILog _log;
        private readonly ICustomerProfileService _customerProfileService;
        private readonly ICurrencyConvertorService _currencyConvertorService;

        public VoucherOperationsService(ICustomerProfileService customerProfileService,
            ICurrencyConvertorService currencyConvertorService, 
            ILogFactory logFactory)
        {
            _customerProfileService = customerProfileService ?? throw new ArgumentNullException(nameof(customerProfileService));
            _currencyConvertorService = currencyConvertorService ?? throw new ArgumentNullException(nameof(currencyConvertorService));
            _log = logFactory.CreateLog(this);
        }

        public async Task<(bool isSuccessful, string errorMessage)> ProcessVoucherPurchaseEvent(VoucherPurchaseModel voucherPurchaseModel)
        {
            var (isValidConversion, conversionAmount) = await _currencyConvertorService
                .CovertToBaseCurrencyAsync(voucherPurchaseModel.Amount, voucherPurchaseModel.Currency);

            if (!isValidConversion)
            {
                return (false, $"'{voucherPurchaseModel.Currency}' cannot be converted to base currency.");
            }

            await _customerProfileService.InsertOrUpdateCustomerPurchaseAmount(voucherPurchaseModel.CustomerId, conversionAmount);

            return (true, string.Empty);
        }
    }
}
