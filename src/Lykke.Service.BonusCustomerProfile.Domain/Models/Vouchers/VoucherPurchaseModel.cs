using System;

namespace Lykke.Service.BonusCustomerProfile.Domain.Models.Vouchers
{
    public class VoucherPurchaseModel
    {
        public Guid CustomerId { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }
}
