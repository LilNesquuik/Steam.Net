using System.Text.Json.Serialization;
using Steam.Converters;

namespace Steam.Models;

public class Game
{
    [JsonPropertyName("appid")]
    public int AppId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("playtime_forever")]
    public int PlaytimeForever { get; set; }

    [JsonPropertyName("playtime_2weeks")]
    public int? Playtime2Weeks { get; set; }

    [JsonPropertyName("img_icon_url")]
    public string ImgIconUrl { get; set; }

    [JsonPropertyName("has_community_visible_stats")]
    public bool? HasCommunityVisibleStats { get; set; }

    [JsonPropertyName("has_leaderboards")]
    public bool? HasLeaderboards { get; set; }

    [JsonPropertyName("playtime_windows_forever")]
    public int PlaytimeWindowsForever { get; set; }

    [JsonPropertyName("playtime_mac_forever")]
    public int PlaytimeMacForever { get; set; }

    [JsonPropertyName("playtime_linux_forever")]
    public int PlaytimeLinuxForever { get; set; }

    [JsonPropertyName("playtime_deck_forever")]
    public int PlaytimeDeckForever { get; set; }

    [JsonPropertyName("rtime_last_played")]
    [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
    public DateTimeOffset RTimeLastPlayed { get; set; }

    [JsonPropertyName("playtime_disconnected")]
    public int PlaytimeDisconnected { get; set; }

    [JsonPropertyName("content_descriptorids")]
    public List<int>? ContentDescriptorIds { get; set; }
}