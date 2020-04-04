using AutoMapper;
using MAVN.Service.BonusCustomerProfile.Client.Models.Customers;
using MAVN.Service.BonusCustomerProfile.Domain.Models.CustomerProfile;

namespace MAVN.Service.BonusCustomerProfile.Profiles
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
