namespace Xaelith.DataModel;

using System.Text.Json.Serialization;
using Xaelith.DataModel.Abstract;

public record Page : IIdentifiable, 
                     IOrderable,
                     IDated,
                     IAuthored,
                     INameable,
                     IDescribable,
                     IRoutable,
                     ITemplatable
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
    public Guid Author { get; set; } = User.Nobody.Id;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;

    [JsonPropertyName("template_name")]
    public string TemplateName { get; set; } = string.Empty;
}