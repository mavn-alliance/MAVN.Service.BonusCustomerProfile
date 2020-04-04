using System;
using System.Threading.Tasks;
using Lykke.Common.Log;
using MAVN.Service.BonusCustomerProfile.Domain.Services;
using Lykke.Service.PartnersIntegration.Contract;

namespace MAVN.Service.BonusCustomerProfile.DomainServices.Subscribers
{
    public class HotelCheckoutSubscriber : RabbitSubscriber<BonusCustomerTriggerEvent>
    {
        private readonly ICustomerProfileService _customerProfileService;

        public HotelCheckoutSubscriber(
            string connectionString,
            string exchangeName,
            ICustomerProfileService customerProfileService,
            ILogFactory logFactory)
            : base(connectionString, exchangeName, logFactory)
        {
            _customerProfileService = customerProfileService;
            GuidsFieldsToValidate.Add(nameof(BonusCustomerTriggerEvent.CustomerId));
        }

        public override async Task<(bool isSuccessful, string errorMessage)> ProcessMessageAsync(
            BonusCustomerTriggerEvent message)
        {
            return await _customerProfileService
                .ProcessHotelStayEventAsync(Guid.Parse(message.CustomerId), message.Amount, message.Currency);
        }
    }
}
