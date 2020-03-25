using System;
using System.Threading.Tasks;
using Lykke.Common.Log;
using Lykke.Service.BonusCustomerProfile.Domain.Services;
using Lykke.Service.PartnersIntegration.Contract;
using Lykke.Service.Referral.Contract.Events;

namespace Lykke.Service.BonusCustomerProfile.DomainServices.Subscribers
{
    public class HotelCheckoutReferrerSubscriber : RabbitSubscriber<HotelReferralUsedEvent>
    {
        private readonly ICustomerProfileService _customerProfileService;
        
        public HotelCheckoutReferrerSubscriber(
            string connectionString,
            string exchangeName,
            ICustomerProfileService customerProfileService,
            ILogFactory logFactory)
            : base(connectionString, exchangeName, logFactory)
        {
            _customerProfileService = customerProfileService;
            GuidsFieldsToValidate.Add(nameof(BonusCustomerTriggerEvent.CustomerId));
        }

        public override Task<(bool isSuccessful, string errorMessage)> ProcessMessageAsync(HotelReferralUsedEvent message)
        {
            return _customerProfileService.ProcessReferrerHotelStayEventAsync(
                Guid.Parse(message.CustomerId),
                message.Amount,
                message.CurrencyCode);
        }
    }
}