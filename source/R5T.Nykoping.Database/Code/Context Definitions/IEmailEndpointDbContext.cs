using System;

using Microsoft.EntityFrameworkCore;


namespace R5T.Nykoping.Database
{
    public interface IEmailEndpointDbContext
    {
        DbSet<Entities.EmailEndpoint> EmailEndpoints { get; }
    }
}
