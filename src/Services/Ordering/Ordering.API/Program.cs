using Common.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ordering.API.Extensions;
using Ordering.Infrustructure.Persistence;
using Serilog;

namespace Ordering.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
          CreateHostBuilder(args)
              .Build()
              .MigrateDatabase<OrderContext>((context, provider) =>
              {
                  var logger = provider.GetService<ILogger<OrderContextSeed>>();
                  OrderContextSeed.SeedAsync(context, logger).Wait();
              })
              .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog(SeriLogger.Configure)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
