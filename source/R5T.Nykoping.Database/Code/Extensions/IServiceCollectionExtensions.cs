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

        /// <summary>
        /// Adds the <see cref="PhoneEndpointRepository{TDbContext}"/> implementation of <see cref="IPhoneEndpointRepository"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddPhoneEndpointRepository<TDbContext>(this IServiceCollection services)
            where TDbContext: DbContext, IPhoneEndpointDbContext
        {
            services.AddSingleton<IPhoneEndpointRepository, PhoneEndpointRepository<TDbContext>>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="PhoneEndpointRepository{TDbContext}"/> implementation of <see cref="IPhoneEndpointRepository"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IPhoneEndpointRepository> AddPhoneEndpointRepositoryAction<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext, IPhoneEndpointDbContext
        {
            var serviceAction = ServiceAction.New<IPhoneEndpointRepository>(() => services.AddPhoneEndpointRepository<TDbContext>());
            return serviceAction;
        }
    }
}
