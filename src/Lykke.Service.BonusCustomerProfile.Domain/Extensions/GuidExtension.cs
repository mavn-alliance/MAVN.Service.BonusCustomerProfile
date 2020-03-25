using System;

namespace Lykke.Service.BonusCustomerProfile.Domain.Extensions
{
    public static class GuidExtension
    {
        public static Guid StringToGuid(this string input)
        {
            if (!Guid.TryParse(input, out var guid))
            {
                throw new ArgumentException("Invalid identifier", nameof(input));
            }

            return guid;
        }
    }
}
