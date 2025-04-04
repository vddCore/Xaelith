namespace Xaelith.DataModel.Configuration;

using System.ComponentModel;
using System.Text.Json.Serialization;

public sealed partial class Settings
{
    public static partial class Sections
    {
        public class Appearance : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler? PropertyChanged;

            [JsonPropertyName("theme")]
            public string Theme { get; set; } = string.Empty;
        }
    }
}