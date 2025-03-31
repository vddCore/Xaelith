namespace Xaelith.DataModel;

using System.Text.Json.Serialization;
using Xaelith.DataModel.Abstract;

public record Category(string name) : IIdentifiable,
                                      IAuthored,
                                      IDated
{
    public static readonly Category None = new("None");

    [JsonPropertyName("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [JsonPropertyName("name")]
    public string Name { get; set; } = name;

    [JsonPropertyName("author")]
    public User Author { get; set; } = User.Nobody;

    [JsonPropertyName("created_date")]
    public DateTime CreatedDate { get; set; }

    [JsonPropertyName("last_edit_date")]
    public DateTime? LastEditDate { get; set; }

    public Category() : this(string.Empty)
    {
    }

}