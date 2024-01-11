using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Persistence.Data;
using Applicaton.Interfaces;
using Persistence.Repository;
using Microsoft.EntityFrameworkCore;

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
                options.UseLazyLoadingProxies();
                options.UseNpgsql(connectionString);
            });
            services.AddScoped<IDataContext, DataContext>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            return services;
        }
    }
}
