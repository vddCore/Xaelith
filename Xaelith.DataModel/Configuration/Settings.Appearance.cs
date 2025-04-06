namespace Xaelith.DataModel.Configuration;

using System.ComponentModel;
using System.Text.Json.Serialization;

public sealed partial class Settings
{
    public static partial class Sections
    {
        public class Appearance : SettingsSection
        {
            [JsonPropertyName("front_theme")]
            public string FrontTheme { get; set; } = string.Empty;
            
            [JsonPropertyName("admin_theme")]
            public string AdminTheme { get; set; } = string.Empty;
        }
    }
}