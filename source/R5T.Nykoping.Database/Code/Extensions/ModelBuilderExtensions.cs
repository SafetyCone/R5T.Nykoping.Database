using System;

using Microsoft.EntityFrameworkCore;


namespace R5T.Nykoping.Database
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder ForEmailEndpointDbContext(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.EmailEndpoint>().HasAlternateKey(x => x.EndpointGUID);

            return modelBuilder;
        }
    }
}
