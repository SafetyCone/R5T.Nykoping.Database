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

        public static ModelBuilder ForPhoneEndpointDbContext(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.PhoneEndpoint>().HasAlternateKey(x => x.EndpointGUID);

            return modelBuilder;
        }

        public static ModelBuilder ForWebformEndpointDbContext(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.WebformEndpoint>().HasAlternateKey(x => x.EndpointGUID);

            return modelBuilder;
        }
    }
}
