using System.Text.Json;
using Steam.Constants;
using Steam.Models;

namespace Steam.Services;

public class UserService
{
    private readonly SteamClient _client;

    public UserService(SteamClient client)
    {
        _client = client;
    }
    
    /// <summary>
    /// Retrieves the summary of a specific Steam user.
    /// </summary>
    /// <param name="steamId">
    /// The unique Steam ID of the user whose summary is to be retrieved.
    /// </param>
    /// <returns>
    /// A <see cref="Player"/> object representing the user's summary.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided <paramref name="steamId"/> is null, empty, or consists only of whitespace.
    /// </exception>
    /// <exception cref="Exception">
    /// Thrown when the request to the Steam API fails or the response cannot be deserialized.
    /// </exception>
    public async Task<Player> GetPlayerSummaryAsync(string steamId)
    {
        if (string.IsNullOrWhiteSpace(steamId))
            throw new ArgumentException("Steam ID cannot be null or empty.", nameof(steamId));
        
        string url = $"{SteamConstants.Endpoints.GetPlayerSummaries}?{SteamConstants.Parameters.Key}={_client.ApiKey}&{SteamConstants.Parameters.SteamIds}={steamId}";
        HttpResponseMessage response = await _client.HttpClient.GetAsync(url);
        
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Failed to get player summaries: {response.ReasonPhrase}");

        using var payload = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
        
        return payload.RootElement
            .GetProperty(SteamConstants.Namespaces.Response)
            .GetProperty(SteamConstants.Namespaces.Players)
            .EnumerateArray()
            .FirstOrDefault()
            .Deserialize<Player>() ?? throw new Exception("Failed to deserialize player summaries.");
    }

    /// <summary>
    /// Retrieves the summaries of multiple Steam users.
    /// </summary>
    /// <param name="steamIds">
    /// An array of unique Steam IDs of the users whose summaries are to be retrieved. max is 100.
    /// </param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of <see cref="Player"/> objects representing the summaries of the users.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided <paramref name="steamIds"/> is null or empty or exceeds 100 IDs.
    /// </exception>
    /// <exception cref="Exception">
    /// Thrown when the request to the Steam API fails or the response cannot be deserialized.
    /// </exception>
    public async Task<IEnumerable<Player>> GetPlayersSummariesAsync(string[] steamIds)
    {
        if (steamIds == null || !steamIds.Any())
            throw new ArgumentException("Steam IDs cannot be null or empty.", nameof(steamIds));
        
        if (steamIds.Length > 100)
            throw new ArgumentException("The maximum number of Steam IDs is 100.", nameof(steamIds));
        
        string url = $"{SteamConstants.Endpoints.GetPlayerSummaries}?{SteamConstants.Parameters.Key}={_client.ApiKey}&{SteamConstants.Parameters.SteamIds}={string.Join(",", steamIds)}";
        HttpResponseMessage response = await _client.HttpClient.GetAsync(url);
        
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Failed to get player summaries: {response.ReasonPhrase}");

        using var payload = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
        
        return payload.RootElement
            .GetProperty(SteamConstants.Namespaces.Response)
            .GetProperty(SteamConstants.Namespaces.Players)
            .Deserialize<IEnumerable<Player>>() ?? throw new Exception("Failed to deserialize player summaries.");
    }
    
    /// <summary>
    /// Retrieves the list of friends for a specific Steam user.
    /// </summary>
    /// <param name="steamId">
    /// The unique Steam ID of the user whose friend list is to be retrieved.
    /// </param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of <see cref="Friend"/> objects representing the user's friends.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided <paramref name="steamId"/> is null, empty, or consists only of whitespace.
    /// </exception>
    /// <exception cref="Exception">
    /// Thrown when the request to the Steam API fails or the response cannot be deserialized.
    /// </exception>
    public async Task<IEnumerable<Friend>> GetFriendListAsync(string steamId)
    {
        if (string.IsNullOrWhiteSpace(steamId))
            throw new ArgumentException("Steam ID cannot be null or empty.", nameof(steamId));
        
        string url = $"{SteamConstants.Endpoints.GetFriendList}?{SteamConstants.Parameters.Key}={_client.ApiKey}&{SteamConstants.Parameters.SteamId}={steamId}";
        HttpResponseMessage response = await _client.HttpClient.GetAsync(url);
        
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Failed to get friend list: {response.ReasonPhrase}");

        using var payload = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
        
        return payload.RootElement
            .GetProperty(SteamConstants.Namespaces.FriendList)
            .GetProperty(SteamConstants.Namespaces.Friends)
            .Deserialize<IEnumerable<Friend>>() ?? throw new Exception("Failed to deserialize friend list.");
    }
    
    /// <summary>
    /// Retrieves the ban information for multiple Steam users.
    /// </summary>
    /// <param name="steamIds">
    /// An array of unique Steam IDs of the users whose ban information is to be retrieved. max is 100.
    /// </param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of <see cref="Ban"/> objects representing the ban information of the users.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided <paramref name="steamIds"/> is null or empty or exceeds 100 IDs.
    /// </exception>
    /// <exception cref="Exception">
    /// Thrown when the request to the Steam API fails or the response cannot be deserialized.
    /// </exception>
    public async Task<IEnumerable<Ban>> GetPlayersBansAsync(string[] steamIds)
    {
        if (steamIds == null || !steamIds.Any())
            throw new ArgumentException("Steam IDs cannot be null or empty.", nameof(steamIds));
        
        if (steamIds.Length > 100)
            throw new ArgumentException("The maximum number of Steam IDs is 100.", nameof(steamIds));
        
        string url = $"{SteamConstants.Endpoints.GetPlayerBans}?{SteamConstants.Parameters.Key}={_client.ApiKey}&{SteamConstants.Parameters.SteamIds}={string.Join(",", steamIds)}";
        HttpResponseMessage response = await _client.HttpClient.GetAsync(url);
        
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Failed to get player bans: {response.ReasonPhrase}");

        using var payload = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
        
        return payload.RootElement
            .GetProperty(SteamConstants.Namespaces.Players)
            .Deserialize<IEnumerable<Ban>>() ?? throw new Exception("Failed to deserialize player bans.");
    }
    
    /// <summary>
    /// Retrieves the ban information for a specific Steam user.
    /// </summary>
    /// <param name="steamId">
    /// The unique Steam ID of the user whose ban information is to be retrieved.
    /// </param>
    /// <returns>
    /// A <see cref="Ban"/> object representing the ban information of the user.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided <paramref name="steamId"/> is null, empty, or consists only of whitespace.
    /// </exception>
    /// <exception cref="Exception">
    /// Thrown when the request to the Steam API fails or the response cannot be deserialized.
    /// </exception>
    public async Task<Ban> GetPlayerBansAsync(string steamId)
    {
        if (string.IsNullOrWhiteSpace(steamId))
            throw new ArgumentException("Steam ID cannot be null or empty.", nameof(steamId));
        
        string url = $"{SteamConstants.Endpoints.GetPlayerBans}?{SteamConstants.Parameters.Key}={_client.ApiKey}&{SteamConstants.Parameters.SteamIds}={steamId}";
        HttpResponseMessage response = await _client.HttpClient.GetAsync(url);
        
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Failed to get player bans: {response.ReasonPhrase}");

        using var payload = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
        
        return payload.RootElement
            .GetProperty(SteamConstants.Namespaces.Players)
            .EnumerateArray()
            .FirstOrDefault()
            .Deserialize<Ban>() ?? throw new Exception("Failed to deserialize player bans.");
    }
}