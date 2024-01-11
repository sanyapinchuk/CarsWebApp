using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog.Events;
using Serilog;
using System;

namespace ProCodeGuide.Samples.IdentityServer4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args).Build();
            builder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
                  // if (IsProductionEnvironment())
                   {
                       webBuilder.UseUrls("https://*:5001");
                   }
               })
               .UseSerilog((ctx, lc) =>
                lc
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .WriteTo.File($"logs/IdentityAppLog-.log", rollingInterval:
                    RollingInterval.Day));
        }

        private static bool IsProductionEnvironment()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production";
        }
    }
}
