using Lykke.Common.Log;
using Lykke.Service.BonusCustomerProfile.Domain.Services;
using Lykke.Service.Referral.Contract.Events;
using System;
using System.Threading.Tasks;

namespace Lykke.Service.BonusCustomerProfile.DomainServices.Subscribers
{
    public class OfferToPurchaseByLeadSubscriber
        : RabbitSubscriber<OfferToPurchaseByLeadEvent>
    {
        private readonly ICustomerProfileService _customerProfileService;

        public OfferToPurchaseByLeadSubscriber(
            string connectionString,
            string exchangeName,
            ILogFactory logFactory,
            ICustomerProfileService customerProfileService)
            : base(connectionString, exchangeName, logFactory)
        {
            _customerProfileService = customerProfileService ??
                                      throw new ArgumentNullException(nameof(customerProfileService));

            GuidsFieldsToValidate.Add(nameof(OfferToPurchaseByLeadEvent.AgentId));
        }

        public override async Task<(bool isSuccessful, string errorMessage)> ProcessMessageAsync(OfferToPurchaseByLeadEvent message)
        {
            await _customerProfileService.ProcessOfferToPurchaseByLeadEvent(Guid.Parse(message.AgentId));

            return (true, string.Empty);
        }
    }
}
