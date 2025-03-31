namespace Xaelith.Services;

using System.Diagnostics;
using System.Text.Json;
using Xaelith.DataModel.Configuration;
using Xaelith.ServiceModel;

public class ConfigurationService : IConfigurationService
{
    public string SettingsFilePath => Path.Combine(KnownPaths.Configuration, "xaelith.json");

    public Settings? Settings
    {
        #nullable disable
            get; private set;
        #nullable enable
    }

    public ConfigurationService()
    {
        LoadAsync().ContinueWith(_ => SubscribeToSettingsChanges());
    }

    public async Task LoadAsync()
    {
        if (Settings != null)
        {
            throw new InvalidOperationException("Settings file was already loaded.");
        }

        try
        {
            using (var fs = File.OpenRead(SettingsFilePath))
            {
                var settings = await JsonSerializer.DeserializeAsync<Settings>(fs);

                if (settings == null)
                {
                    await SaveAsync();
                }
                else
                {
                    Settings = settings;
                }
            }
        }
        catch (FileNotFoundException)
        {
            await SaveAsync();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            // todo: logging.
        }
    }

    public async Task SaveAsync()
    {
        if (Settings == null)
        {
            Settings = new Settings();
        }
        
        using (var fs = File.Create(SettingsFilePath))
        {
            await JsonSerializer.SerializeAsync(
                fs,
                Settings,
                new JsonSerializerOptions { WriteIndented = true }
            );
        }
    }

    private void SubscribeToSettingsChanges()
    {
        if (Settings == null)
            return;
        
        Settings.PropertyChanged += async (_, _) =>  await SaveAsync();
    }
}