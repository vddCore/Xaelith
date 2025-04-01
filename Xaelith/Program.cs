using Xaelith.Components;
using Xaelith.ServiceModel;
using Xaelith.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<IConfigurationService, ConfigurationService>()
    .AddSingleton<IStorageService, StorageService>()
    .AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection()
    .UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();