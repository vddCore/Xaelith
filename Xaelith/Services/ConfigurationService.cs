namespace Xaelith.Services;

using System.ComponentModel;
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
        Reload();
    }

    public void Reload()
    {
        if (Settings != null)
        {
            Settings.PropertyChanged -= OnSettingsChanged;
            Settings.Dispose();
        }

        try
        {
            using (var fs = File.OpenRead(SettingsFilePath))
            {
                Settings = JsonSerializer.Deserialize<Settings>(fs);
            }
        }
        catch
        {
            // Ignore.
        }
        
        if (Settings == null)
        {
            Settings = new Settings();
            Save();
        }
        
        Settings.PropertyChanged += OnSettingsChanged;
    }

    public void Save()
    {
        using (var fs = File.Create(SettingsFilePath))
        {
            JsonSerializer.Serialize(
                fs,
                Settings,
                new JsonSerializerOptions { WriteIndented = true }
            );
        }
    }

    private void OnSettingsChanged(object? _, PropertyChangedEventArgs e)
        => Save();
}