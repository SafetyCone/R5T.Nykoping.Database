using System;
using System.Collections.Generic;
using System.Linq;

using R5T.Endalia;

using PhoneEndpointEntity = R5T.Nykoping.Database.Entities.PhoneEndpoint;


namespace R5T.Nykoping.Database
{
    public static class IPhoneEndpointDbContextExtensions
    {
        public static IQueryable<PhoneEndpointEntity> GetPhoneEndpoints(this IPhoneEndpointDbContext phoneEndpointDbContext, IEnumerable<EndpointIdentity> endpointIdentities)
        {
            var identityValues = endpointIdentities.Select(x => x.Value).ToList();

            var queryable = phoneEndpointDbContext.PhoneEndpoints.Where(x => identityValues.Contains(x.EndpointGUID));
            return queryable;
        }

        public static IQueryable<PhoneEndpointEntity> GetPhoneEndpoint(this IPhoneEndpointDbContext phoneEndpointDbContext, EndpointIdentity endpointIdentity)
        {
            var queryable = phoneEndpointDbContext.PhoneEndpoints.Where(x => x.EndpointGUID == endpointIdentity.Value);
            return queryable;
        }
    }
}
