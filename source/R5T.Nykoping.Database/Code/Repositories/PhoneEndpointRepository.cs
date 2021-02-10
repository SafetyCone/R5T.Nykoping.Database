using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using R5T.Endalia;
using R5T.Venetia;

using PhoneEndpointEntity = R5T.Nykoping.Database.Entities.PhoneEndpoint;


namespace R5T.Nykoping.Database
{
    public class PhoneEndpointRepository<TDbContext> : ProvidedDatabaseRepositoryBase<TDbContext>, IPhoneEndpointRepository
        where TDbContext: DbContext, IPhoneEndpointDbContext
    {
        public PhoneEndpointRepository(DbContextOptions<TDbContext> dbContextOptions, IDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextOptions, dbContextProvider)
        {
        }

        public async Task Add(EndpointIdentity endpoint, string phoneNumber)
        {
            await this.ExecuteInContextAsync(async dbContext =>
            {
                var entity = new PhoneEndpointEntity()
                {
                    EndpointGUID = endpoint.Value,
                    PhoneNumber = phoneNumber,
                };

                dbContext.Add(entity);

                await dbContext.SaveChangesAsync();
            });
        }

        public async Task<bool> Exists(EndpointIdentity endpoint)
        {
            var exists = await this.ExecuteInContextAsync(async dbContext =>
            {
                var entity = await dbContext.GetPhoneEndpoint(endpoint).SingleOrDefaultAsync();

                var output = entity is object;
                return output;
            });

            return exists;
        }

        public async Task<bool> HasPhoneNumber(EndpointIdentity endpoint)
        {
            var hasPhoneNumber = await this.ExecuteInContextAsync(async dbContext =>
            {
                var phoneNumber = await dbContext.GetPhoneEndpoint(endpoint).Select(x => x.PhoneNumber).SingleOrDefaultAsync();

                var output = phoneNumber is object;
                return output;
            });

            return hasPhoneNumber;
        }

        public async Task<Dictionary<EndpointIdentity, string>> GetPhoneNumbersByEndpointIdentity(IEnumerable<EndpointIdentity> endpointIdentities)
        {
            var phoneNumbersByEndpointIdentity = await this.ExecuteInContextAsync(async dbContext =>
            {
                var output = await dbContext
                    .GetPhoneEndpoints(endpointIdentities)
                    .Select(x => new { x.EndpointGUID, x.PhoneNumber })
                    .ToDictionaryAsync(
                        x => EndpointIdentity.From(x.EndpointGUID),
                        x => x.PhoneNumber);

                return output;
            });

            return phoneNumbersByEndpointIdentity;
        }

        public async Task SetPhoneNumber(EndpointIdentity endpoint, string phoneNumber)
        {
            await this.ExecuteInContextAsync(async dbContext =>
            {
                var entity = await dbContext.GetPhoneEndpoint(endpoint).SingleAsync();

                entity.PhoneNumber = phoneNumber;

                await dbContext.SaveChangesAsync();
            });
        }

        public async Task<List<EndpointIdentity>> GetEndpointIdentitiesByPhoneNumber(string phoneNumber)
        {
            var endpointIdentities = await this.ExecuteInContextAsync(async dbContext =>
            {
                var output = await dbContext.PhoneEndpoints
                    .Where(x => x.PhoneNumber == phoneNumber)
                    .Select(x => EndpointIdentity.From(x.EndpointGUID))
                    .ToListAsync();
                return output;
            });

            return endpointIdentities;
        }
    }
}
