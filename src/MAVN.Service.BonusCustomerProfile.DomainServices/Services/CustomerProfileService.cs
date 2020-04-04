using Common.Log;
using Lykke.Common.Log;
using MAVN.Service.BonusCustomerProfile.Domain.Models.CustomerProfile;
using MAVN.Service.BonusCustomerProfile.Domain.Repositories;
using MAVN.Service.BonusCustomerProfile.Domain.Services;
using System;
using System.Threading.Tasks;

namespace MAVN.Service.BonusCustomerProfile.DomainServices.Services
{
    public class CustomerProfileService : ICustomerProfileService
    {
        private readonly ICustomerProfileRepository _customerProfileRepository;
        private readonly ILog _log;
        private readonly ICurrencyConvertorService _currencyConvertorService;

        public CustomerProfileService(
            ICustomerProfileRepository customerProfileRepository,
            ILogFactory logFactory,
            ICurrencyConvertorService currencyConvertorService)
        {
            _customerProfileRepository = customerProfileRepository ?? throw new ArgumentNullException(nameof(customerProfileRepository));
            _currencyConvertorService = currencyConvertorService ?? throw new ArgumentNullException(nameof(currencyConvertorService));
            _log = logFactory.CreateLog(this);
        }

        public async Task<CustomerProfileModel> GetCustomerProfileAsync(Guid customerId)
        {
            return await _customerProfileRepository.GetCustomerProfileAsync(customerId);
        }

        public async Task ProcessParticipatedInCampaignEvent(Guid customerId)
        {
            var customerProfile = await CreateOrGetCustomerProfileAsync(customerId);

            customerProfile.TotalCampaignsContributedCount += 1;

            await _customerProfileRepository.CreateOrUpdateAsync(customerProfile);
        }

        public async Task ProcessFriendReferralEvent(Guid customerId)
        {
            var customerProfile = await CreateOrGetCustomerProfileAsync(customerId);

            customerProfile.TotalReferredFriendCount += 1;

            await _customerProfileRepository.CreateOrUpdateAsync(customerProfile);
        }

        public async Task InsertOrUpdateCustomerPurchaseAmount(Guid customerId, decimal amount)
        {
            var customerProfile = await CreateOrGetCustomerProfileAsync(customerId);

            customerProfile.TotalPurchasedAmount += amount;

            await _customerProfileRepository.CreateOrUpdateAsync(customerProfile);

            _log.Info($"Customer profile updated: Customer Id '{customerId}', Total Purchased Amount '{customerProfile.TotalPurchasedAmount}'");
        }

        public async Task<(bool isSuccessful, string errorMessage)> ProcessHotelStayEventAsync(Guid customerId,
            decimal amount, string currency)
        {
            if (amount < 0)
                return (false, $"Amount has invalid value: '{amount}'.");

            var (isValid, conversionAmount) = await _currencyConvertorService
                .CovertToBaseCurrencyAsync(amount, currency);

            if (!isValid)
                return (false, $"'{currency}' cannot be converted to base currency.");

            var customerProfile = await CreateOrGetCustomerProfileAsync(customerId);

            customerProfile.TotalHotelStayCount += 1;
            customerProfile.TotalHotelStayAmount += conversionAmount;

            await _customerProfileRepository.CreateOrUpdateAsync(customerProfile);

            return (true, string.Empty);
        }

        public async Task<(bool isSuccessful, string errorMessage)> ProcessReferrerHotelStayEventAsync(Guid customerId, decimal amount, string currency)
        {
            if (amount < 0)
                return (false, $"Amount has invalid value: '{amount}'.");

            var (isValid, conversionAmount) = await _currencyConvertorService
                .CovertToBaseCurrencyAsync(amount, currency);

            if (!isValid)
                return (false, $"'{currency}' cannot be converted to base currency.");

            var customerProfile = await CreateOrGetCustomerProfileAsync(customerId);

            customerProfile.TotalHotelReferralStayCount += 1;
            customerProfile.TotalHotelReferralStayAmount += conversionAmount;

            await _customerProfileRepository.CreateOrUpdateAsync(customerProfile);

            return (true, string.Empty);
        }

        public async Task<(bool isSuccessful, string errorMessage)> ProcessPurchaseReferralEvent(Guid referrerId, decimal amount, string currency)
        {
            var (isValidConversion, conversionAmount) = await _currencyConvertorService.CovertToBaseCurrencyAsync(amount, currency);

            if (!isValidConversion)
            {
                return (false, $"'{currency}' cannot be converted to base currency.");
            }

            var customerProfile = await CreateOrGetCustomerProfileAsync(referrerId);

            customerProfile.TotalPurchasedAmount += conversionAmount;
            customerProfile.TotalReferredPurchaseCount += 1;

            await _customerProfileRepository.CreateOrUpdateAsync(customerProfile);

            return (true, string.Empty);
        }

        public async Task<(bool isSuccessful, string errorMessage)> CreateCustomerProfileAsync(Guid customerId)
        {
            var isSuccessInsert = await _customerProfileRepository
                .InsertAsync(new CustomerProfileModel
                {
                    CustomerId = customerId
                });

            return !isSuccessInsert ?
                (false, $"Bonus Customer profile with id '{customerId}' already exists.")
                : (true, string.Empty);
        }

        public async Task ProcessPropertyLeadApprovedReferralEvent(Guid referrerId)
        {
            var customerProfile = await CreateOrGetCustomerProfileAsync(referrerId);

            customerProfile.TotalReferredEstateLeadsCount += 1;

            await _customerProfileRepository.CreateOrUpdateAsync(customerProfile);
        }

        public async Task<(bool isSuccessful, string errorMessage)> ProcessPropertyPurchaseReferralEvent(
            Guid referrerId, decimal amount, string currency)
        {
            var (isValidConversion, conversionAmount) =
                await _currencyConvertorService.CovertToBaseCurrencyAsync(amount, currency);

            if (!isValidConversion)
                return (false, $"'{currency}' cannot be converted to base currency.");
            
            var customerProfile = await CreateOrGetCustomerProfileAsync(referrerId);

            customerProfile.TotalReferredEstatePurchasesCount += 1;
            customerProfile.TotalReferredEstatePurchasesAmount += conversionAmount;

            await _customerProfileRepository.CreateOrUpdateAsync(customerProfile);
            
            return (true, string.Empty);
        }

        public async Task<(bool isSuccessful, string errorMessage)> ProcessPropertyPurchaseByLeadEvent(
            Guid customerId, decimal amount, string currency)
        {
            var (isValidConversion, conversionAmount) =
                await _currencyConvertorService.CovertToBaseCurrencyAsync(amount, currency);

            if (!isValidConversion)
                return (false, $"'{currency}' cannot be converted to base currency.");
            
            var customerProfile = await CreateOrGetCustomerProfileAsync(customerId);

            customerProfile.TotalPropertyPurchasesByLeadCount += 1;
            customerProfile.TotalPropertyPurchasesByLeadAmount += conversionAmount;

            await _customerProfileRepository.CreateOrUpdateAsync(customerProfile);
            
            return (true, string.Empty);
        }

        public async Task ProcessOfferToPurchaseByLeadEvent(Guid customerId)
        {
            var customerProfile = await CreateOrGetCustomerProfileAsync(customerId);

            customerProfile.TotalOfferToPurchaseByLeadCount += 1;

            await _customerProfileRepository.CreateOrUpdateAsync(customerProfile);
        }

        private async Task<CustomerProfileModel> CreateOrGetCustomerProfileAsync(Guid customerId)
        {
            var customerProfileModel = await _customerProfileRepository.GetCustomerProfileAsync(customerId);

            if (customerProfileModel == null)
            {
                return new CustomerProfileModel
                {
                    CustomerId = customerId
                };
            }

            return customerProfileModel;
        }
    }
}
