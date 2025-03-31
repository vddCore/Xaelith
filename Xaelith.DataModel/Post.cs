namespace Xaelith.DataModel;

using System.Text.Json;
using System.Text.Json.Serialization;
using Xaelith.DataModel.Abstract;

public class Post : IIdentifiable,
                    IDated, 
                    IRenderable
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [JsonPropertyName("created_date")]
    public DateTime CreatedDate { get; set; }
    
    [JsonPropertyName("last_edit_date")]
    public DateTime? LastEditDate { get; set; }
    
    [JsonPropertyName("author")]
    public string Author { get; set; } = string.Empty;
    
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
    
    [JsonPropertyName("content_directory_path")]
    public string ContentDirectoryPath { get; set; } = string.Empty;
    
    [JsonPropertyName("category")]
    public Category Category { get; set; } = Category.None;

    [JsonPropertyName("tags")]
    public List<Tag> Tags { get; set; } = [];

    public static async Task<Post> FromFile(string filePath)
    {
        using (var fs = File.OpenRead(filePath))
        {
            return await JsonSerializer.DeserializeAsync<Post>(fs)
                ?? throw new IOException($"The file '{filePath}' could not be deserialized.");
        }
    }
}