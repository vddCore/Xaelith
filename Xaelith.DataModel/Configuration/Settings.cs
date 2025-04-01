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

    public Settings()
    {
        Core.PropertyChanged += OnSectionPropertyChanged;
        General.PropertyChanged += OnSectionPropertyChanged;
    }

    private void OnSectionPropertyChanged(object? sender, PropertyChangedEventArgs args)
    {
        PropertyChanged?.Invoke(sender, args);
    }

    public void Dispose()
    {
        Core.PropertyChanged -= OnSectionPropertyChanged;
        General.PropertyChanged -= OnSectionPropertyChanged;
    }
}