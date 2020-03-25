using Lykke.Service.BonusCustomerProfile.Client;
using Lykke.Service.BonusCustomerProfile.Client.Models.Campaigns;
using Lykke.Service.BonusCustomerProfile.Client.Models.Enums;
using Lykke.Service.BonusCustomerProfile.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;

namespace Lykke.Service.BonusCustomerProfile.Controllers
{
    [Route("api/campaigns")]
    [ApiController]
    public class CampaignsController : Controller, ICampaignsApi
    {
        private readonly ICampaignService _campaignService;
        private readonly ILog _log;

        public CampaignsController(ICampaignService campaignService, ILogFactory logFactory)
        {
            _campaignService = campaignService ?? throw new ArgumentNullException(nameof(campaignService));
            _log = logFactory.CreateLog(this);
        }
        /// <response code="200">An array of campaign contributions ids for a customer</response>
        [HttpGet("contributions/{customerId}")]
        [SwaggerOperation("GetContributionsByCustomerId")]
        [ProducesResponseType(typeof(ContributionsIdsResponseModel), (int)HttpStatusCode.OK)]
        public async Task<ContributionsIdsResponseModel> GetContributionsByCustomerIdAsync([Required] [FromRoute] string customerId)
        {
            if (!Guid.TryParse(customerId, out var customerIdGuid))
            {
                var errorMessage = "Invalid identifier";

                _log.Warning(errorMessage, context: customerId);

                return new ContributionsIdsResponseModel
                {
                    ErrorCode = BonusCustomerProfileErrorCodes.GuidCanNotBeParsed,
                    ErrorMessage = errorMessage
                };
            }

            var result = await _campaignService.GetCampaignIdsAsync(customerIdGuid);
            return new ContributionsIdsResponseModel
            {
                ContributionIds = result.ContributionIds,
                ErrorCode = BonusCustomerProfileErrorCodes.None
            };
        }
    }
}
