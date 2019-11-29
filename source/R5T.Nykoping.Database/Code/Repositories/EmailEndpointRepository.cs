using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using R5T.Endalia;
using R5T.Venetia;

using EmailEndpointEntity = R5T.Nykoping.Database.Entities.EmailEndpoint;


namespace R5T.Nykoping.Database
{
    public class EmailEndpointRepository : DatabaseRepositoryBase<EmailEndpointDbContext>, IEmailEndpointRepository
    {
        public EmailEndpointRepository(DbContextOptions<EmailEndpointDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public override EmailEndpointDbContext GetNewDbContext()
        {
            var dbContext = new EmailEndpointDbContext(this.DbContextOptions);
            return dbContext;
        }

        public void Add(EndpointIdentity endpoint, string emailAddress)
        {
            this.ExecuteInContext(dbContext =>
            {
                var entity = new EmailEndpointEntity()
                {
                    EndpointGUID = endpoint.Value,
                    EmailAddress = emailAddress,
                };

                dbContext.Add(entity);

                dbContext.SaveChanges();
            });
        }

        public bool Exists(EndpointIdentity endpoint)
        {
            var exists = this.ExecuteInContext(dbContext =>
            {
                var entity = dbContext.EmailEndpoints.Where(x => x.EndpointGUID == endpoint.Value).SingleOrDefault();

                var output = entity is object;
                return output;
            });

            return exists;
        }

        public bool HasEmailAddress(EndpointIdentity endpoint)
        {
            var hasEmailAddress = this.ExecuteInContext(dbContext =>
            {
                var emailAddress = dbContext.EmailEndpoints.Where(x => x.EndpointGUID == endpoint.Value).Select(x => x.EmailAddress).Single();

                var output = emailAddress is object;
                return output;
            });

            return hasEmailAddress;
        }

        public string GetEmailAddress(EndpointIdentity endpoint)
        {
            var emailAddress = this.ExecuteInContext(dbContext =>
            {
                var output = dbContext.EmailEndpoints.Where(x => x.EndpointGUID == endpoint.Value).Select(x => x.EmailAddress).Single();
                return output;
            });

            return emailAddress;
        }

        public void SetEmailAddress(EndpointIdentity endpoint, string emailAddress)
        {
            this.ExecuteInContext(dbContext =>
            {
                var entity = dbContext.EmailEndpoints.Where(x => x.EndpointGUID == endpoint.Value).Single();

                entity.EmailAddress = emailAddress;

                dbContext.SaveChanges();
            });
        }
    }
}
