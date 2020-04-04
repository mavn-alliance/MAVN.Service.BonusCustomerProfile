using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAVN.Service.BonusCustomerProfile.MsSqlRepositories.Entities
{
    [Table("customer_profile")]
    public class CustomerProfile
    {
        [Key]
        [Column("customer_id")]
        public Guid CustomerId { get; set; }

        [Column("total_campaigns_contributed_count")]
        public int TotalCampaignsContributedCount { get; set; }

        [Column("total_referred_friend_count")]
        public int TotalReferredFriendCount { get; set; }

        [Column("total_purchased_amount")]
        public decimal TotalPurchasedAmount { get; set; }

        [Column("total_referred_purchase_count")]
        public int TotalReferredPurchaseCount { get; set; }

        [Column("total_referred_purchase_amount")]
        public decimal TotalReferredPurchasedAmount { get; set; }

        [Column("total_referred_estate_leads_count")]
        public int TotalReferredEstateLeadsCount { get; set; }

        [Column("total_referred_estate_purchases_count")]
        public int TotalReferredEstatePurchasesCount { get; set; }

        [Column("total_referred_estate_purchases_amount")]
        public decimal TotalReferredEstatePurchasesAmount { get; set; }

        [Column("total_property_purchases_by_lead_count")]
        public int TotalPropertyPurchasesByLeadCount { get; set; }

        [Column("total_property_purchases_by_lead_amount")]
        public decimal TotalPropertyPurchasesByLeadAmount { get; set; }

        [Column("total_offer_to_purchases_by_lead_count")]
        public int TotalOfferToPurchaseByLeadCount { get; set; }

        [Column("total_hotel_stay_count")]
        public int TotalHotelStayCount { get; set; }

        [Column("total_hotel_stay_amount")]
        public decimal TotalHotelStayAmount { get; set; }

        [Column("total_hotel_referral_stay")]
        public int TotalHotelReferralStayCount { get; set; }

        [Column("total_hotel_referral_stay_amount")]
        public decimal TotalHotelReferralStayAmount { get; set; }
    }
}
