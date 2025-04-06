namespace Xaelith.DataModel;

using System.Text.Json.Serialization;

public class Translation
{
    [JsonPropertyName("lang")]
    public string Language { get; set; } = "en_US";

    [JsonPropertyName("lang_friendly")]
    public string TranslationName { get; set; } = "English (United States)";

    [JsonPropertyName("translations")]
    protected Dictionary<string, string> Translations { get; set; } = [];

    [JsonIgnore]
    public string this[string key]
    {
        get
        {
            if (Translations.TryGetValue(key, out var translation))
            {
                return translation;
            }

            return key;
        }
    }
}
