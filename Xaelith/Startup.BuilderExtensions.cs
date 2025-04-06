namespace Xaelith;

using Xaelith.ServiceModel;
using Xaelith.Services;
using Xaelith.Services.Localization;

internal static class BuilderExtensions
{
    public static IServiceCollection RegisterCoreXaelithComponents(this IServiceCollection services)
    {
        services
            .AddSingleton<IConfigurationService, ConfigurationService>()
            .AddSingleton<ILanguageService, LanguageService>()
            .AddSingleton<IStorageService, StorageService>()
            .AddSingleton<IContentService, ContentService>();
        
        return services;
    }
}