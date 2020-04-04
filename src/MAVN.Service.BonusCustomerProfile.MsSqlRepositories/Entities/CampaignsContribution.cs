using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAVN.Service.BonusCustomerProfile.MsSqlRepositories.Entities
{
    [Table("campaigns_contribution")]
    public class CampaignsContribution : BaseEntity
    {
        [Column("customer_id")]
        public Guid CustomerId { get; set; }

        [Column("campaign_id")]
        public Guid CampaignId { get; set; }
    }
}
