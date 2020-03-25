using Lykke.Common.Log;
using Lykke.Service.BonusCustomerProfile.Domain.Services;
using Lykke.Service.Referral.Contract.Events;
using System;
using System.Threading.Tasks;

namespace Lykke.Service.BonusCustomerProfile.DomainServices.Subscribers
{
    public class PropertyLeadApprovedReferralSubscriber
      : RabbitSubscriber<PropertyLeadApprovedReferralEvent>
    {
        private readonly ICustomerProfileService _customerProfileService;

        public PropertyLeadApprovedReferralSubscriber(
            string connectionString,
            string exchangeName,
            ILogFactory logFactory,
            ICustomerProfileService customerProfileService)
            : base(connectionString, exchangeName, logFactory)
        {
            _customerProfileService = customerProfileService ??
                throw new ArgumentNullException(nameof(customerProfileService));

            GuidsFieldsToValidate.Add(nameof(PropertyLeadApprovedReferralEvent.ReferrerId));
        }

        public override async Task<(bool isSuccessful, string errorMessage)> ProcessMessageAsync(PropertyLeadApprovedReferralEvent message)
        {
            await _customerProfileService.ProcessPropertyLeadApprovedReferralEvent(Guid.Parse(message.ReferrerId));

            return (true, string.Empty);
        }
    }
}
