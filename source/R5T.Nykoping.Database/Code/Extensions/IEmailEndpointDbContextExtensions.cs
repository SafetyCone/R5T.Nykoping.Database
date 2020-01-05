using System;
using System.Linq;

using R5T.Endalia;

using EmailEndpointEntity = R5T.Nykoping.Database.Entities.EmailEndpoint;


namespace R5T.Nykoping.Database
{
    public static class IEmailEndpointDbContextExtensions
    {
        public static IQueryable<EmailEndpointEntity> GetEmailEndpoint(this IEmailEndpointDbContext dbContext, EndpointIdentity endpointIdentity)
        {
            var emailEndpointQueryable = dbContext.EmailEndpoints.Where(x => x.EndpointGUID == endpointIdentity.Value);
            return emailEndpointQueryable;
        }
    }
}
