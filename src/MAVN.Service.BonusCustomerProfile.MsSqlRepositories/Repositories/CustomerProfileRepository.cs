using System;
using System.Threading.Tasks;
using AutoMapper;
using MAVN.Persistence.PostgreSQL.Legacy;
using MAVN.Service.BonusCustomerProfile.Domain.Models.CustomerProfile;
using MAVN.Service.BonusCustomerProfile.Domain.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace MAVN.Service.BonusCustomerProfile.MsSqlRepositories.Repositories
{
    public class CustomerProfileRepository : ICustomerProfileRepository
    {
        private const int PrimaryKeyViolationErrorCode = 2627;

        private readonly PostgreSQLContextFactory<BonusCustomerProfileContext> _msSqlContextFactory;
        private readonly IMapper _mapper;

        public CustomerProfileRepository(
            PostgreSQLContextFactory<BonusCustomerProfileContext> msSqlContextFactory,
            IMapper mapper)
        {
            _msSqlContextFactory = msSqlContextFactory ?? throw new ArgumentNullException(nameof(msSqlContextFactory));
            _mapper = mapper;
        }

        public async Task<CustomerProfileModel> GetCustomerProfileAsync(Guid customerId)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                var customerProfile = await context.CustomerProfiles.FirstOrDefaultAsync(c => c.CustomerId == customerId);

                return _mapper.Map<CustomerProfileModel>(customerProfile);
            }
        }

        public async Task<bool> InsertAsync(CustomerProfileModel customerProfile)
        {
            var customerProfileEntity = _mapper.Map<Entities.CustomerProfile>(customerProfile);

            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                context.CustomerProfiles.Add(customerProfileEntity);

                try
                {
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException is PostgresException sqlException &&
                        sqlException.SqlState == PostgresErrorCodes.UniqueViolation)
                    {
                        return false;
                    }

                    throw;
                }

                return true;
            }
        }

        public async Task UpdateAsync(CustomerProfileModel customerProfile)
        {
            var customerProfileEntity = _mapper.Map<Entities.CustomerProfile>(customerProfile);

            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                context.CustomerProfiles.Update(customerProfileEntity);

                await context.SaveChangesAsync();
            }
        }

        public async Task CreateOrUpdateAsync(CustomerProfileModel customerProfile)
        {
            var customerProfileEntity = _mapper.Map<Entities.CustomerProfile>(customerProfile);

            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                await context.CustomerProfiles.AddAsync(customerProfileEntity);

                try
                {
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException is SqlException sqlException &&
                        sqlException.Number == PrimaryKeyViolationErrorCode)
                    {
                        context.CustomerProfiles.Update(customerProfileEntity);

                        await context.SaveChangesAsync();
                    }

                    else throw;
                }
            }
        }
    }
}
