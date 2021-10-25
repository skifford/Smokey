using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Smokey.Extensions
{
    public static class SmokeyServiceCollectionExtensions
    {
        public static IServiceCollection AddSmokey(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TestRun>(t => configuration.GetSection("TestRun").Bind(t));
            services.AddTransient<DriverFactory>();
            services.AddTransient<Browser>();
            return services;
        }
    }
}
