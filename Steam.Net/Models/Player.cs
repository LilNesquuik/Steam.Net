using System.Text.Json.Serialization;
using Steam.Converters;

namespace Steam.Models;

/// <summary>
/// Represents a Steam user.
/// </summary>
public class Player
{
    [JsonPropertyName("steamid")]
    public string SteamId { get; set; }

    [JsonPropertyName("communityvisibilitystate")]
    public int CommunityVisibilityState { get; set; }

    [JsonPropertyName("profilestate")]
    public int ProfileState { get; set; }

    [JsonPropertyName("personaname")]
    public string PersonaName { get; set; }

    [JsonPropertyName("commentpermission")]
    public int CommentPermission { get; set; }

    [JsonPropertyName("profileurl")]
    public string ProfileUrl { get; set; }

    [JsonPropertyName("avatar")]
    public string Avatar { get; set; }

    [JsonPropertyName("avatarmedium")]
    public string AvatarMedium { get; set; }

    [JsonPropertyName("avatarfull")]
    public string AvatarFull { get; set; }

    [JsonPropertyName("avatarhash")]
    public string AvatarHash { get; set; }

    [JsonPropertyName("lastlogoff")]
    [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
    public DateTimeOffset LastLogOff { get; set; }

    [JsonPropertyName("personastate")]
    public int PersonaState { get; set; }

    [JsonPropertyName("primaryclanid")]
    public string PrimaryClanId { get; set; }

    [JsonPropertyName("timecreated")]
    [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
    public DateTimeOffset TimeCreated { get; set; }

    [JsonPropertyName("personastateflags")]
    public int PersonaStateFlags { get; set; }

    [JsonPropertyName("loccountrycode")]
    public string LocCountryCode { get; set; }
}