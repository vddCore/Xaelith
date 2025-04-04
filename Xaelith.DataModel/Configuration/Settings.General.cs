namespace Xaelith.DataModel.Configuration;

using System.ComponentModel;
using System.Text.Json.Serialization;

public sealed partial class Settings
{
    public static partial class Sections
    {
        public class General : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler? PropertyChanged;
        
            [JsonPropertyName("title")]
            public string Title { get; set; } = "Xaelith Blog";

            [JsonPropertyName("description")]
            public string Description { get; set; } = "A brand new Xaelith Blog!";
            
            [JsonPropertyName("home_url")]
            public string HomeUrl { get; set; } = string.Empty;
            
            
        }
    }
}