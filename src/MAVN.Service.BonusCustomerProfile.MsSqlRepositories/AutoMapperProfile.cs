using System;
using AutoMapper;
using MAVN.Service.BonusCustomerProfile.Domain.Models.Campaign;
using MAVN.Service.BonusCustomerProfile.Domain.Models.CustomerProfile;
using MAVN.Service.BonusCustomerProfile.MsSqlRepositories.Entities;

namespace MAVN.Service.BonusCustomerProfile.MsSqlRepositories
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CampaignsContributionModel, CampaignsContribution>()
                .ForMember(dest => dest.CampaignId, opt => opt.MapFrom(src => Guid.Parse(src.CampaignId)))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => Guid.Parse(src.CustomerId)));

            CreateMap<CustomerProfileModel, CustomerProfile>();
            CreateMap<CustomerProfile, CustomerProfileModel>();

            //Needed for Update property values without creating a new object
            CreateMap<CustomerProfile, CustomerProfile>();
        }
    }
}
