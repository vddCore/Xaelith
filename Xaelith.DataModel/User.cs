namespace Xaelith.DataModel;

using System.Text.Json.Serialization;
using Xaelith.DataModel.Abstract;

public record User : IIdentifiable,
                     IOrderable,
                     IDated,
                     INameable
{
    public static readonly User Nobody = new()
    {
        Id = Guid.Empty,
        Enabled = false,
        CreatedDate = DateTime.UnixEpoch,
        LastEditDate = DateTime.UnixEpoch,
        LastLoginDate = DateTime.UnixEpoch,
        Name = "nobody",
        DisplayName = string.Empty,
        Email = "nobody@nowhere.com",
        PasswordHash = string.Empty,
        PasswordSalt = string.Empty,
        Role = UserRole.Unspecified
    };

    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("ordinal")]
    public uint Ordinal { get; set; }

    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }
    
    [JsonPropertyName("created_date")]
    public DateTime CreatedDate { get; set; }
    
    [JsonPropertyName("last_edit_date")]
    public DateTime? LastEditDate { get; set; }
    
    [JsonPropertyName("last_login_date")]
    public DateTime? LastLoginDate { get; set; }
    
    [JsonPropertyName("login_name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    
    [JsonPropertyName("password_hash")]
    public string PasswordHash { get; set; } = string.Empty;
    
    [JsonPropertyName("password_salt")]
    public string PasswordSalt { get; set; } = string.Empty;

    [JsonPropertyName("role")]
    public UserRole Role { get; set; } = UserRole.Unspecified;
}