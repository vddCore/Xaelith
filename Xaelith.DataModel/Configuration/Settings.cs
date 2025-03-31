namespace Xaelith.DataModel.Configuration;

using System.ComponentModel;
using System.Text.Json.Serialization;

public sealed class Settings : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    [JsonPropertyName("core")]
    public CoreSection Core { get; set; } = new();

    [JsonPropertyName("general")]
    public GeneralSection General { get; set; } = new();

    public Settings()
    {
        Core.PropertyChanged += (sender, args) => PropertyChanged?.Invoke(sender, args);
        General.PropertyChanged += (sender, args) => PropertyChanged?.Invoke(sender, args);
    }

    public class CoreSection : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        [JsonPropertyName("plugins_directory")]
        public string PluginsDirectory { get; set; } = "xa-plugins";
        
        [JsonPropertyName("data_directory")]
        public string DataDirectory { get; set; } = "xa-data";

        internal CoreSection()
        {
        }
    }

    public class GeneralSection : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        [JsonPropertyName("title")]
        public string Title { get; set; } = "Xaelith Blog";
    }
}