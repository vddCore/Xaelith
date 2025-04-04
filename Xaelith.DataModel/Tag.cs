namespace Xaelith.DataModel;

using System.Text.Json.Serialization;
using Xaelith.DataModel.Abstract;

public record Tag : IIdentifiable,
                    IOrderable,
                    INameable,
                    IDated,
                    IAuthored,
                    IRoutable
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("ordinal")]
    public uint Ordinal { get; set; }

    [JsonPropertyName("created_date")]
    public DateTime CreatedDate { get; set; }

    [JsonPropertyName("last_edit_date")]
    public DateTime? LastEditDate { get; set; }

    [JsonPropertyName("author")]
    public User Author { get; set; } = User.Nobody;

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;

    public Tag(string name)
    {
        Name = name;
    }
    
    public Tag() : this(string.Empty)
    {
    }
}