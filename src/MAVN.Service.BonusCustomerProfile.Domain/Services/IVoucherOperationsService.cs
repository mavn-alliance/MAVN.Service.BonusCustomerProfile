using MAVN.Service.BonusCustomerProfile.Domain.Models.Vouchers;
using System.Threading.Tasks;

namespace MAVN.Service.BonusCustomerProfile.Domain.Services
{
    public interface IVoucherOperationsService
    {
        Task<(bool isSuccessful, string errorMessage)> ProcessVoucherPurchaseEvent(VoucherPurchaseModel voucherPurchaseModel);
    }
}
