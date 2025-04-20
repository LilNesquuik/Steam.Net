using System.Text.Json.Serialization;

namespace Steam.Models;

/// <summary>
/// Represents the ban information of a Steam user.
/// </summary>
public class Ban
{
    [JsonPropertyName("SteamId")]
    public string SteamId { get; set; }

    [JsonPropertyName("CommunityBanned")]
    public bool CommunityBanned { get; set; }

    [JsonPropertyName("VACBanned")]
    public bool VACBanned { get; set; }

    [JsonPropertyName("NumberOfVACBans")]
    public int NumberOfVACBans { get; set; }

    [JsonPropertyName("DaysSinceLastBan")]
    public int DaysSinceLastBan { get; set; }

    [JsonPropertyName("NumberOfGameBans")]
    public int NumberOfGameBans { get; set; }

    [JsonPropertyName("EconomyBan")]
    public string EconomyBan { get; set; } 
}