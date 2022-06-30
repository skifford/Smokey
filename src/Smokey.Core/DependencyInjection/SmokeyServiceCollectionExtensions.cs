using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Smokey.DependencyInjection
{
    public static class SmokeyServiceCollectionExtensions
    {
        public static IServiceCollection AddSmokey(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TestRun>(testRun => configuration.GetSection(nameof(TestRun)).Bind(testRun));
            services.AddTransient<IBrowser,Browser>();
            return services;
        }
    }
}
