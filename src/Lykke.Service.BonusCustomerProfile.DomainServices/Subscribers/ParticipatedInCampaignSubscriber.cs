using Lykke.Common.Log;
using Lykke.Service.BonusCustomerProfile.Domain.Models.Campaign;
using Lykke.Service.BonusCustomerProfile.Domain.Services;
using Lykke.Service.BonusEngine.Contract.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Log;

namespace Lykke.Service.BonusCustomerProfile.DomainServices.Subscribers
{
    public class ParticipatedInCampaignSubscriber :
        RabbitSubscriber<ParticipatedInCampaignEvent>
    {
        private readonly ICampaignService _campaignService;
        private readonly ICustomerProfileService _customerProfileService;
        private readonly ILog _log;

        public ParticipatedInCampaignSubscriber(string connectionString,
            string exchangeName,
            ILogFactory logFactory,
            ICampaignService campaignService,
            ICustomerProfileService customerProfileService)
            : base(connectionString, exchangeName, logFactory)
        {
            _campaignService = campaignService 
                               ?? throw new ArgumentNullException(nameof(campaignService));
            _customerProfileService = customerProfileService 
                                      ?? throw new ArgumentNullException(nameof(customerProfileService));
            _log = logFactory.CreateLog(this);

            GuidsFieldsToValidate.Add(nameof(ParticipatedInCampaignEvent.CustomerId));
            GuidsFieldsToValidate.Add(nameof(ParticipatedInCampaignEvent.CampaignId));
        }

        public override async Task<(bool isSuccessful, string errorMessage)> ProcessMessageAsync(ParticipatedInCampaignEvent message)
        {
            //insert a new Row in CampaignsContribution if we dont have CampaignsContribution with this campaignId and customerId
            var customerId = Guid.Parse(message.CustomerId);
            var doesExist = await _campaignService
                .DoesCampaignsContributionExistAsync(customerId, Guid.Parse(message.CampaignId));

            if (!doesExist)
            {
                await _campaignService.InsertAsync(new CampaignsContributionModel
                {
                     CampaignId = message.CampaignId,
                     CustomerId = message.CustomerId
                });

                await _customerProfileService.ProcessParticipatedInCampaignEvent(customerId);
            }

            return (true, string.Empty);
        }
    }
}
