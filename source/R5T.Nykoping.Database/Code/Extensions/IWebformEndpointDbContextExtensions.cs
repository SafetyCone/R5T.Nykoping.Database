using System;
using System.Collections.Generic;
using System.Linq;

using R5T.Endalia;

using WebformEndpointEntity = R5T.Nykoping.Database.Entities.WebformEndpoint;


namespace R5T.Nykoping.Database
{
    public static class IWebformEndpointDbContextExtensions
    {
        public static IQueryable<WebformEndpointEntity> GetWebformEndpoints(this IWebformEndpointDbContext phoneEndpointDbContext, IEnumerable<EndpointIdentity> endpointIdentities)
        {
            var identityValues = endpointIdentities.Select(x => x.Value).ToList();

            var queryable = phoneEndpointDbContext.WebformEndpoints.Where(x => identityValues.Contains(x.EndpointGUID));
            return queryable;
        }

        public static IQueryable<WebformEndpointEntity> GetWebformEndpoint(this IWebformEndpointDbContext phoneEndpointDbContext, EndpointIdentity endpointIdentity)
        {
            var queryable = phoneEndpointDbContext.WebformEndpoints.Where(x => x.EndpointGUID == endpointIdentity.Value);
            return queryable;
        }
    }
}
