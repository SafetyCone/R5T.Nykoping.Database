using System;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;

using R5T.Dacia;


namespace R5T.Nykoping.Database
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="EmailEndpointRepository{TDbContext}"/> implementation of <see cref="IEmailEndpointRepository"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddEmailEndpointRepository<TDbContext>(this IServiceCollection services)
            where TDbContext: DbContext, IEmailEndpointDbContext
        {
            services.AddSingleton<IEmailEndpointRepository, EmailEndpointRepository<TDbContext>>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="EmailEndpointRepository{TDbContext}"/> implementation of <see cref="IEmailEndpointRepository"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IEmailEndpointRepository> AddEmailEndpointRepositoryAction<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext, IEmailEndpointDbContext
        {
            var serviceAction = ServiceAction.New<IEmailEndpointRepository>(() => services.AddEmailEndpointRepository<TDbContext>());
            return serviceAction;
        }
    }
}
