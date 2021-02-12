using System;

using Microsoft.EntityFrameworkCore;


namespace R5T.Nykoping.Database
{
    public interface IWebformEndpointDbContext
    {
        DbSet<Entities.WebformEndpoint> WebformEndpoints { get; }
    }
}
