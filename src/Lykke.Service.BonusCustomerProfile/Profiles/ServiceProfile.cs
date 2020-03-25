using AutoMapper;
using Lykke.Service.BonusCustomerProfile.Client.Models.Customers;
using Lykke.Service.BonusCustomerProfile.Domain.Models.CustomerProfile;

namespace Lykke.Service.BonusCustomerProfile.Profiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<CustomerProfileModel, CustomerResponseModel>(MemberList.Source);

            CreateMap<CustomerResponseModel, CustomerProfileModel>(MemberList.Destination);
        }
    }
}
