using System;

using Microsoft.EntityFrameworkCore;


namespace R5T.Nykoping.Database
{
    public class EmailEndpointDbContext : DbContext
    {
        public DbSet<Entities.EmailEndpoint> EmailEndpoints { get; set; }


        public EmailEndpointDbContext(DbContextOptions<EmailEndpointDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entities.EmailEndpoint>().HasAlternateKey(x => x.EndpointGUID);
        }
    }
}
