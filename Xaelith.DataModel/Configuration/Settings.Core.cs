namespace Xaelith.DataModel.Configuration;

using System.ComponentModel;
using System.Text.Json.Serialization;

public sealed partial class Settings
{
    public static partial class Sections
    {
        public class Core : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler? PropertyChanged;

            [JsonPropertyName("user_db")]
            public string UserDatabase { get; set; } = "users.json";
            
            [JsonPropertyName("tag_db")]
            public string TagDatabase { get; set; } = "tags.json";
            
            [JsonPropertyName("category_db")]
            public string CategoryDatabase { get; set; } = "categories.json";
            
            [JsonPropertyName("page_db")]
            public string PageDatabase { get; set; } = "pages.json";
            
            [JsonPropertyName("post_db")]
            public string PostDatabase { get; set; } = "posts.json";
            
            [JsonPropertyName("content_body_file_name")]
            public string ContentBodyFileName { get; set; } = "body.md";
        }
    }
}