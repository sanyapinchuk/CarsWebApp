using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CarsServer.Data;
using Applicaton.Interfaces;

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
                options.UseSqlServer(connectionString);
                //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped<IDataContext>(provider =>
                provider.GetService<DataContext>());

            return services;
        }
    }
}
