using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Smokey.Features.ValuesCollecting;
using Smokey.Models;

namespace Smokey.DependencyInjection
{
    public static class SmokeyServiceCollectionExtensions
    {
        public static IServiceCollection AddSmokey(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TestRun>(testRun => configuration.GetSection(nameof(TestRun)).Bind(testRun));
            services.AddTransient<IBrowser,Browser>();
            services.AddSingleton<ValuesStorage>();
            return services;
        }
    }
}
