namespace Xaelith.DataModel.Configuration;

using System.ComponentModel;
using System.Text.Json.Serialization;

public sealed partial class Settings : IDisposable
{
    public event EventHandler<SectionChangedEventArgs>? SectionChanged;
    
    [JsonPropertyName("core")]
    public Sections.Core Core { get; set; } = new();

    [JsonPropertyName("general")]
    public Sections.General General { get; set; } = new();

    [JsonPropertyName("appearance")]
    public Sections.Appearance Appearance { get; set; } = new();

    public Settings()
    {
        Core.PropertyChanged += OnSectionModified;
        General.PropertyChanged += OnSectionModified;
        Appearance.PropertyChanged += OnSectionModified;
    }

    private void OnSectionModified(object? sender, PropertyChangedEventArgs args)
    {
        if (sender is not SettingsSection section || string.IsNullOrEmpty(args.PropertyName))
            return;

        var jsonPropertyKey = section.GetJsonPropertyKey(args.PropertyName);

        SectionChanged?.Invoke(
            this,
            new SectionChangedEventArgs(
                section,
                args.PropertyName,
                jsonPropertyKey,
                section[jsonPropertyKey]
            )
        );
    }

    public void Dispose()
    {
        Appearance.PropertyChanged -= OnSectionModified;
        General.PropertyChanged -= OnSectionModified;
        Core.PropertyChanged -= OnSectionModified;
    }
}