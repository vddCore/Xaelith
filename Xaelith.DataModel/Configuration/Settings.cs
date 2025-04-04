namespace Xaelith.DataModel.Configuration;

using System.ComponentModel;
using System.Text.Json.Serialization;

public sealed partial class Settings : INotifyPropertyChanged, IDisposable
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
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
        PropertyChanged?.Invoke(sender, args);
    }

    public void Dispose()
    {
        Appearance.PropertyChanged -= OnSectionModified;
        General.PropertyChanged -= OnSectionModified;
        Core.PropertyChanged -= OnSectionModified;
    }
}