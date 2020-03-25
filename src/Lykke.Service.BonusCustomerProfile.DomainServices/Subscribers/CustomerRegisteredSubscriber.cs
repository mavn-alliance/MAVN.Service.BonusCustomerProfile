using Lykke.Common.Log;
using Lykke.Service.BonusCustomerProfile.Domain.Services;
using Lykke.Service.Referral.Contract.Events;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Lykke.Service.CustomerManagement.Contract.Events;

namespace Lykke.Service.BonusCustomerProfile.DomainServices.Subscribers
{
    public class CustomerRegisteredSubscriber :
        RabbitSubscriber<CustomerRegistrationEvent>
    {
        private readonly ICustomerProfileService _customerProfileService;

        public CustomerRegisteredSubscriber(
            string connectionString, 
            string exchangeName, 
            ILogFactory logFactory, 
            ICustomerProfileService customerProfileService)
            : base(connectionString, exchangeName, logFactory)
        {
            _customerProfileService = customerProfileService 
                                      ?? throw new ArgumentNullException(nameof(customerProfileService));

            GuidsFieldsToValidate.Add(nameof(CustomerRegistrationEvent.CustomerId));
        }

        public override async Task<(bool isSuccessful, string errorMessage)> ProcessMessageAsync(CustomerRegistrationEvent message)
        {
            return await _customerProfileService.CreateCustomerProfileAsync(Guid.Parse(message.CustomerId));
        }
    }
}
