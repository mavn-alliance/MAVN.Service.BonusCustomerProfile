using JetBrains.Annotations;

namespace Lykke.Service.BonusCustomerProfile.Client.Models.Customers
{
    /// <summary>
    /// Represents response model with contribution ids array
    /// </summary>
    [PublicAPI]
    public class CustomerResponseModel : BonusEngineErrorResponseModel
    {
        /// <summary>
        /// The Id of the customer.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Total number of campaigns the user has contributed into.
        /// </summary>
        public int TotalCampaignsContributedCount { get; set; }

        /// <summary>
        /// Total friends the user has referred.
        /// </summary>
        public int TotalReferredFriendCount { get; set; }

        /// <summary>
        /// Total purchased amount in base unit.
        /// </summary>
        public decimal TotalPurchasedAmount { get; set; }

        /// <summary>
        /// Total purchase the user has referred
        /// </summary>
        public int TotalReferredPurchaseCount { get; set; }

        /// <summary>
        /// Total referred purchased amount in base unit
        /// </summary>
        public decimal TotalReferredPurchasedAmount { get; set; }

        /// <summary>
        /// Total referred estate leads count
        /// </summary>
        public int TotalReferredEstateLeadsCount { get; set; }

        /// <summary>
        /// Total referred estate purchases count
        /// </summary>
        public int TotalReferredEstatePurchasesCount { get; set; }

        /// <summary>
        /// Total commission amount of referred estate purchases.
        /// </summary>
        public decimal TotalReferredEstatePurchasesAmount { get; set; }

        /// <summary>
        /// Total property purchases by lead
        /// </summary>
        public int TotalPropertyPurchasesByLeadCount { get; set; }

        /// <summary>
        /// Total commission amount of property purchases by lead.
        /// </summary>
        public decimal TotalPropertyPurchasesByLeadAmount { get; set; }

        /// <summary>
        /// Total offer to purchases by lead
        /// </summary>
        public int TotalOfferToPurchaseByLeadCount { get; set; }
        
        /// <summary>
        /// Total number of the hotel stay.
        /// </summary>
        public int TotalHotelStayCount { get; set; }

        /// <summary>
        /// Total amount in the base unit of the hotel stay.
        /// </summary>
        public decimal TotalHotelStayAmount { get; set; }
        
        /// <summary>
        /// Total number of usages of hotel referrals.
        /// </summary>
        public int TotalHotelReferralStayCount { get; set; }
        
        /// <summary>
        /// Total amount spent by others via hotel referrals.
        /// </summary>
        public decimal TotalHotelReferralStayAmount { get; set; }
    }
}
