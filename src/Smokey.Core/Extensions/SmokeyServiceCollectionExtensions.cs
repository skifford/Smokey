using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Smokey.Extensions
{
    public static class SmokeyServiceCollectionExtensions
    {
        public static IServiceCollection AddSmokey(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TestRun>(testRun => configuration.GetSection("TestRun").Bind(testRun));
            services.AddTransient<Browser>();
            return services;
        }
    }
}
