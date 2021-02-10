using System;

using Microsoft.EntityFrameworkCore;


namespace R5T.Nykoping.Database
{
    public interface IPhoneEndpointDbContext
    {
        DbSet<Entities.PhoneEndpoint> PhoneEndpoints { get; }
    }
}
