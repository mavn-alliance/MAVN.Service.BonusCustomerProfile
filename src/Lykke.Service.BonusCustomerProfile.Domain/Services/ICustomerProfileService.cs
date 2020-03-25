using System;
using System.Threading.Tasks;
using Lykke.Service.BonusCustomerProfile.Domain.Models.CustomerProfile;

namespace Lykke.Service.BonusCustomerProfile.Domain.Services
{
    public interface ICustomerProfileService
    {
        Task<CustomerProfileModel> GetCustomerProfileAsync(Guid customerId);
        Task ProcessParticipatedInCampaignEvent(Guid customerId);
        Task ProcessFriendReferralEvent(Guid customerId);
        Task InsertOrUpdateCustomerPurchaseAmount(Guid customerId, decimal amount);
        Task<(bool isSuccessful, string errorMessage)> ProcessHotelStayEventAsync(Guid customerId, decimal amount,
            string currency);
        Task<(bool isSuccessful, string errorMessage)> ProcessReferrerHotelStayEventAsync(Guid customerId, decimal amount,
            string currency);
        Task<(bool isSuccessful, string errorMessage)> ProcessPurchaseReferralEvent(Guid referrerId, decimal amount, string currency);
        Task<(bool isSuccessful, string errorMessage)> CreateCustomerProfileAsync(Guid customerId);
        Task ProcessPropertyLeadApprovedReferralEvent(Guid referrerId);
        Task<(bool isSuccessful, string errorMessage)> ProcessPropertyPurchaseReferralEvent(Guid referrerId, decimal amount, string currency);
        Task<(bool isSuccessful, string errorMessage)> ProcessPropertyPurchaseByLeadEvent(Guid customerId, decimal amount, string currency);
        Task ProcessOfferToPurchaseByLeadEvent(Guid customerId);
    }
}
