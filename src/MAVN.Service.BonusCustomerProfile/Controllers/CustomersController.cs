using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Common.Log;
using Lykke.Common.Log;
using MAVN.Service.BonusCustomerProfile.Client;
using MAVN.Service.BonusCustomerProfile.Client.Models.Customers;
using MAVN.Service.BonusCustomerProfile.Domain.Services;
using MAVN.Service.BonusCustomerProfile.Client.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MAVN.Service.BonusCustomerProfile.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : Controller, ICustomersApi
    {
        private readonly ICustomerProfileService _customerProfileService;
        private readonly IMapper _mapper;
        private readonly ILog _log;

        public CustomersController(
            ICustomerProfileService customerProfileService,
            IMapper mapper,
            ILogFactory logFactory)
        {
            _customerProfileService = customerProfileService ?? throw new ArgumentNullException(nameof(customerProfileService));
            _mapper = mapper;
            _log = logFactory.CreateLog(this);
        }

        /// <response code="200">An array of referred customer ids for a customer</response>
        [HttpGet("{customerId}")]
        [SwaggerOperation("GetCustomerAsync")]
        [ProducesResponseType(typeof(CustomerResponseModel), (int)HttpStatusCode.OK)]
        public async Task<CustomerResponseModel> GetCustomerAsync([Required] [FromRoute] string customerId)
        {
            if (!Guid.TryParse(customerId, out var customerIdGuid))
            {
                return new CustomerResponseModel
                {
                    ErrorMessage = "Invalid identifier.",
                    ErrorCode = BonusCustomerProfileErrorCodes.GuidCanNotBeParsed
                };
            }

            var result = await _customerProfileService.GetCustomerProfileAsync(customerIdGuid);
            return _mapper.Map<CustomerResponseModel>(result);
        }
    }
}
