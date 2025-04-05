namespace Xaelith.DataModel;

using System.Text.Json.Serialization;
using Xaelith.DataModel.Abstract;

public record Category : IIdentifiable,
                         IOrderable,
                         IDated,
                         IAuthored,
                         INameable,
                         IDescribable
{
    public static readonly Category None = new("None");

    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("ordinal")]
    public uint Ordinal { get; set; }
    
    [JsonPropertyName("created_date")]
    public DateTime CreatedDate { get; set; }

    [JsonPropertyName("last_edit_date")]
    public DateTime? LastEditDate { get; set; }

    [JsonPropertyName("author")]
    public Guid Author { get; set; } = User.Nobody.Id;
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
    

    public Category(string name)
    {
        Name = name;
    }
    
    public Category() : this(string.Empty)
    {
    }
}