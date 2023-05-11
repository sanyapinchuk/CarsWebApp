using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Persistence.Data;
using Applicaton.Interfaces;
using Persistence.Repository;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<DataContext>(options =>
            {
                //options.UseSqlite(connectionString);
                options.UseLazyLoadingProxies();
                options.UseSqlServer(connectionString);
                //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            services.AddSingleton<IRepositoryManager>(provider =>
            provider.GetService<RepositoryManager>());

            services.AddScoped<IDataContext>(provider =>
                provider.GetService<DataContext>());

            return services;
        }
    }
}
