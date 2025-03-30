namespace Xaelith.DataModel;

using System.Text.Json.Serialization;
using Xaelith.DataModel.Abstract;

public class Tag(string name) : IIdentifiable,
                                IDated,
                                IAuthored,
                                IRoutable
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [JsonPropertyName("created_date")]
    public DateTime CreatedDate { get; set; }
    
    [JsonPropertyName("last_edit_date")]
    public DateTime? LastEditDate { get; set; }
    
    [JsonPropertyName("author")]
    public User Author { get; set; } = User.Nobody;
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = name;
    
    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;
    
    public Tag() : this(string.Empty)
    {
    }
}