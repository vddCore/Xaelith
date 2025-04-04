namespace Xaelith.DataModel;

using System.Text.Json.Serialization;
using Xaelith.DataModel.Abstract;

public record Category : IIdentifiable,
                         IOrderable,
                         INameable,
                         IAuthored,
                         IDated
{
    public static readonly Category None = new("None");

    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("ordinal")]
    public uint Ordinal { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("author_user_id")]
    public Guid AuthorUserId { get; set; } = User.Nobody.Id;
    
    [JsonPropertyName("created_date")]
    public DateTime CreatedDate { get; set; }

    [JsonPropertyName("last_edit_date")]
    public DateTime? LastEditDate { get; set; }

    public Category(string name)
    {
        Name = name;
    }
    
    public Category() : this(string.Empty)
    {
    }
}