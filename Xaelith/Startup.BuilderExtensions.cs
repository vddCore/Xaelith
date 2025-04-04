namespace Xaelith;

using Xaelith.ServiceModel;
using Xaelith.Services;

internal static class BuilderExtensions
{
    public static IServiceCollection RegisterCoreXaelithComponents(this IServiceCollection services)
    {
        services.AddSingleton<IConfigurationService, ConfigurationService>()
                .AddSingleton<IStorageService, StorageService>();
        
        return services;
    }
}