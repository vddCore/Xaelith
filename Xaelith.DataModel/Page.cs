namespace Xaelith.DataModel;

using System.Text.Json.Serialization;
using Xaelith.DataModel.Abstract;

public record Page : IIdentifiable, 
                     INameable,
                     IRenderable, 
                     IAuthored,
                     IDated,
                     IRoutable
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("created_date")]
    public DateTime CreatedDate { get; set; }
    
    [JsonPropertyName("last_edit_date")]
    public DateTime? LastEditDate { get; set; }

    [JsonPropertyName("author")]
    public User Author { get; set; } = User.Nobody;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("content_directory_path")]
    public string ContentDirectoryPath { get; set; } = string.Empty;

    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;
}