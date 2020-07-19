using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using R5T.Endalia;
using R5T.Venetia;

using EmailEndpointEntity = R5T.Nykoping.Database.Entities.EmailEndpoint;


namespace R5T.Nykoping.Database
{
    public class EmailEndpointRepository<TDbContext> : ProvidedDatabaseRepositoryBase<TDbContext>, IEmailEndpointRepository
        where TDbContext: DbContext, IEmailEndpointDbContext
    {
        public EmailEndpointRepository(DbContextOptions<TDbContext> dbContextOptions, IDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextOptions, dbContextProvider)
        {
        }

        public async Task Add(EndpointIdentity endpoint, string emailAddress)
        {
            await this.ExecuteInContextAsync(async dbContext =>
            {
                var entity = new EmailEndpointEntity()
                {
                    EndpointGUID = endpoint.Value,
                    EmailAddress = emailAddress,
                };

                dbContext.Add(entity);

                await dbContext.SaveChangesAsync();
            });
        }

        public async Task<bool> Exists(EndpointIdentity endpoint)
        {
            var exists = await this.ExecuteInContextAsync(async dbContext =>
            {
                var entity = await dbContext.GetEmailEndpoint(endpoint).SingleOrDefaultAsync();

                var output = entity is object;
                return output;
            });

            return exists;
        }

        public async Task<bool> HasEmailAddress(EndpointIdentity endpoint)
        {
            var hasEmailAddress = await this.ExecuteInContextAsync(async dbContext =>
            {
                var emailAddress = await dbContext.GetEmailEndpoint(endpoint).Select(x => x.EmailAddress).SingleOrDefaultAsync();

                var output = emailAddress is object;
                return output;
            });

            return hasEmailAddress;
        }

        public async Task<Dictionary<EndpointIdentity, string>> GetEmailAddressesByEndpointIdentity(IEnumerable<EndpointIdentity> endpointIdentities)
        {
            var emailAddressesByEndpointIdentity = await this.ExecuteInContextAsync(async dbContext =>
            {
                var output = await dbContext
                    .GetEmailEndpoints(endpointIdentities)
                    .Select(x => new { x.EndpointGUID, x.EmailAddress })
                    .ToDictionaryAsync(
                        x => EndpointIdentity.From(x.EndpointGUID),
                        x => x.EmailAddress);

                return output;
            });

            return emailAddressesByEndpointIdentity;
        }

        public async Task SetEmailAddress(EndpointIdentity endpoint, string emailAddress)
        {
            await this.ExecuteInContextAsync(async dbContext =>
            {
                var entity = await dbContext.GetEmailEndpoint(endpoint).SingleAsync();

                entity.EmailAddress = emailAddress;

                await dbContext.SaveChangesAsync();
            });
        }
    }
}
