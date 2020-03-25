using Lykke.Service.BonusCustomerProfile.Domain.Models.Campaign;
using System;
using System.Threading.Tasks;

namespace Lykke.Service.BonusCustomerProfile.Domain.Services
{
    public interface ICurrencyConvertorService
    {
        Task<(bool isValid, decimal amount)> CovertToBaseCurrencyAsync(decimal amount, string currency);
    }
}
