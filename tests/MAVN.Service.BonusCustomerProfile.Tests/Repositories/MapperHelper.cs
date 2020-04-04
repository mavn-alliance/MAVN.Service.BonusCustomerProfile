using AutoMapper;
using MAVN.Service.BonusCustomerProfile.MsSqlRepositories;

namespace MAVN.Service.BonusCustomerProfile.Tests.Repositories
{
    public static class MapperHelper
    {
        public static IMapper CreateAutoMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddMaps(typeof(AutoMapperProfile)));

            return config.CreateMapper();
        }
    }
}
