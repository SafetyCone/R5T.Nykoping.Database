using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using R5T.Endalia;
using R5T.Venetia;

using WebformEndpointEntity = R5T.Nykoping.Database.Entities.WebformEndpoint;


namespace R5T.Nykoping.Database
{
    public class WebformEndpointRepository<TDbContext> : ProvidedDatabaseRepositoryBase<TDbContext>, IWebformEndpointRepository
        where TDbContext: DbContext, IWebformEndpointDbContext
    {
        public WebformEndpointRepository(DbContextOptions<TDbContext> dbContextOptions, IDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextOptions, dbContextProvider)
        {
        }

        public async Task Add(EndpointIdentity endpoint, string webformUrl)
        {
            await this.ExecuteInContextAsync(async dbContext =>
            {
                var entity = new WebformEndpointEntity()
                {
                    EndpointGUID = endpoint.Value,
                    WebformUrl = webformUrl,
                };

                dbContext.Add(entity);

                await dbContext.SaveChangesAsync();
            });
        }

        public async Task<bool> Exists(EndpointIdentity endpoint)
        {
            var exists = await this.ExecuteInContextAsync(async dbContext =>
            {
                var entity = await dbContext.GetWebformEndpoint(endpoint).SingleOrDefaultAsync();

                var output = entity is object;
                return output;
            });

            return exists;
        }

        public async Task<bool> HasWebformUrl(EndpointIdentity endpoint)
        {
            var hasWebformUrl = await this.ExecuteInContextAsync(async dbContext =>
            {
                var webformUrl = await dbContext.GetWebformEndpoint(endpoint).Select(x => x.WebformUrl).SingleOrDefaultAsync();

                var output = webformUrl is object;
                return output;
            });

            return hasWebformUrl;
        }

        public async Task<Dictionary<EndpointIdentity, string>> GetWebformUrlsByEndpointIdentity(IEnumerable<EndpointIdentity> endpointIdentities)
        {
            var webformUrlsByEndpointIdentity = await this.ExecuteInContextAsync(async dbContext =>
            {
                var output = await dbContext
                    .GetWebformEndpoints(endpointIdentities)
                    .Select(x => new { x.EndpointGUID, x.WebformUrl })
                    .ToDictionaryAsync(
                        x => EndpointIdentity.From(x.EndpointGUID),
                        x => x.WebformUrl);

                return output;
            });

            return webformUrlsByEndpointIdentity;
        }

        public async Task SetWebformUrl(EndpointIdentity endpoint, string webformUrl)
        {
            await this.ExecuteInContextAsync(async dbContext =>
            {
                var entity = await dbContext.GetWebformEndpoint(endpoint).SingleAsync();

                entity.WebformUrl = webformUrl;

                await dbContext.SaveChangesAsync();
            });
        }

        public async Task<List<EndpointIdentity>> GetEndpointIdentitiesByWebformUrl(string webformUrl)
        {
            var endpointIdentities = await this.ExecuteInContextAsync(async dbContext =>
            {
                var output = await dbContext.WebformEndpoints
                    .Where(x => x.WebformUrl == webformUrl)
                    .Select(x => EndpointIdentity.From(x.EndpointGUID))
                    .ToListAsync();
                return output;
            });

            return endpointIdentities;
        }
    }
}
