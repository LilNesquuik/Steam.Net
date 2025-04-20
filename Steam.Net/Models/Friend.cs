using System.Text.Json.Serialization;
using Steam.Converters;

namespace Steam.Models;

/// <summary>
/// Represents a friend of a Steam user.
/// </summary>
public class Friend
{
    [JsonPropertyName("steamid")]
    public string SteamId { get; set; }
    
    [JsonPropertyName("relationship")]
    public string Relationship { get; set; }
    
    [JsonPropertyName("friend_since")]
    [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
    public DateTimeOffset FriendSince { get; set; }
}