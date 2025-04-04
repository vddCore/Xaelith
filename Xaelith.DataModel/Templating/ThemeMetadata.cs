namespace Xaelith.DataModel.Templating;

using System.Text.Json.Serialization;

public class ThemeMetadata
{
    public sealed class ThemeProvides
    {
        [JsonPropertyName("admin")]
        public bool AdminTheme { get; set; }

        [JsonPropertyName("front")]
        public bool FrontTheme { get; set; }
    }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("version")]
    public string Version { get; set; } = "0.0.0";

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("author")]
    public string Author { get; set; } = string.Empty;

    [JsonPropertyName("provides")]
    public ThemeProvides Provides { get; set; } = new();
}