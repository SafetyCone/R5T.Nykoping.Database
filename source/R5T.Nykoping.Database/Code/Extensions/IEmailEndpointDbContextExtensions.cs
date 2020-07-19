using System;
using System.Collections.Generic;
using System.Linq;

using R5T.Endalia;

using EmailEndpointEntity = R5T.Nykoping.Database.Entities.EmailEndpoint;


namespace R5T.Nykoping.Database
{
    public static class IEmailEndpointDbContextExtensions
    {
        public static IQueryable<EmailEndpointEntity> GetEmailEndpoints(this IEmailEndpointDbContext emailEndpointDbContext, IEnumerable<EndpointIdentity> endpointIdentities)
        {
            var identityValues = endpointIdentities.Select(x => x.Value).ToList();

            var queryable = emailEndpointDbContext.EmailEndpoints.Where(x => identityValues.Contains(x.EndpointGUID));
            return queryable;
        }

        public static IQueryable<EmailEndpointEntity> GetEmailEndpoint(this IEmailEndpointDbContext emailEndpointDbContext, EndpointIdentity endpointIdentity)
        {
            var queryable = emailEndpointDbContext.EmailEndpoints.Where(x => x.EndpointGUID == endpointIdentity.Value);
            return queryable;
        }
    }
}
