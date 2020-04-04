using System;

namespace MAVN.Service.BonusCustomerProfile.Domain.Models.CustomerProfile
{
    public class CustomerProfileModel
    {
        public Guid CustomerId { get; set; }

        public int TotalCampaignsContributedCount { get; set; }

        public int TotalReferredFriendCount { get; set; }

        public decimal TotalPurchasedAmount { get; set; }

        public int TotalReferredPurchaseCount { get; set; }

        public decimal TotalReferredPurchasedAmount { get; set; }

        public int TotalReferredEstateLeadsCount { get; set; }

        public int TotalReferredEstatePurchasesCount { get; set; }

        public decimal TotalReferredEstatePurchasesAmount { get; set; }

        public int TotalPropertyPurchasesByLeadCount { get; set; }

        public decimal TotalPropertyPurchasesByLeadAmount { get; set; }

        public int TotalOfferToPurchaseByLeadCount { get; set; }

        public int TotalHotelStayCount { get; set; }

        public decimal TotalHotelStayAmount { get; set; }

        public int TotalHotelReferralStayCount { get; set; }

        public decimal TotalHotelReferralStayAmount { get; set; }
    }
}
