using AutoMapper;
using Lykke.Service.BonusCustomerProfile.MsSqlRepositories;

namespace Lykke.Service.BonusCustomerProfile.Tests.Repositories
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
