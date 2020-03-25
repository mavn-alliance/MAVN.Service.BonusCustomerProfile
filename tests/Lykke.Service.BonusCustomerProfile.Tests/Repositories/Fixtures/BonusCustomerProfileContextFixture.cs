using Microsoft.EntityFrameworkCore;
using System;
using Lykke.Service.BonusCustomerProfile.MsSqlRepositories;

namespace Lykke.Service.BonusCustomerProfile.Tests.Repositories.Fixtures
{
    public class BonusCustomerProfileContextFixture : IDisposable
    {
        internal DbContextOptions DbContextOptions { get; private set; }

        public BonusCustomerProfileContext BonusCustomerProfileContext => GetInMemoryContextWithSeededData();

        private BonusCustomerProfileContext GetInMemoryContextWithSeededData()
        {
            var context = CreateDataContext();
            BonusCustomerProfileContextSeed.Seed(context);
            return context;
        }

        private BonusCustomerProfileContext CreateDataContext()
        {
            DbContextOptions = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(nameof(BonusCustomerProfileContext))
                .Options;

            var context = new BonusCustomerProfileContext(DbContextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }

        public void Dispose()
        {
            BonusCustomerProfileContext?.Dispose();
        }
    }
}
