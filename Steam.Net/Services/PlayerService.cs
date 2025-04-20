using System.Text.Json;
using Steam.Constants;
using Steam.Models;

namespace Steam.Services;

public class PlayerService
{
    private readonly SteamClient _client;

    public PlayerService(SteamClient client)
    {
        _client = client;
    }
    
    /// <summary>
    /// Retrieves the list of games owned by a specific Steam user.
    /// </summary>
    /// <param name="steamId">
    /// The unique Steam ID of the user whose owned games are to be retrieved.
    /// </param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of <see cref="Game"/> objects representing the games owned by the user.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided <paramref name="steamId"/> is null, empty, or consists only of whitespace.
    /// </exception>
    /// <exception cref="Exception">
    /// Thrown when the request to the Steam API fails or the response cannot be deserialized.
    /// </exception>
    public async Task<IEnumerable<Game>> GetOwnedGamesAsync(string steamId)
    {
        if (string.IsNullOrWhiteSpace(steamId))
            throw new ArgumentException("Steam ID cannot be null or empty.", nameof(steamId));
        
        string url = $"{SteamConstants.Endpoints.GetOwnedGames}?{SteamConstants.Parameters.Key}={_client.ApiKey}&{SteamConstants.Parameters.SteamId}={steamId}";
        HttpResponseMessage response = await _client.HttpClient.GetAsync(url);
        
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Failed to get owned games: {response.ReasonPhrase}");

        using var payload = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
        
        return payload.RootElement
            .GetProperty(SteamConstants.Namespaces.Response)
            .GetProperty(SteamConstants.Namespaces.Games)
            .Deserialize<IEnumerable<Game>>() ?? throw new Exception("Failed to deserialize owned games.");
    }
    
    /// <summary>
    /// Retrieves the list of games recently played by a specific Steam user.
    /// </summary>
    /// <param name="steamId">
    /// The unique Steam ID of the user whose recently played games are to be retrieved.
    /// </param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of <see cref="Game"/> objects representing the games recently played by the user.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided <paramref name="steamId"/> is null, empty, or consists only of whitespace.
    /// </exception>
    /// <exception cref="Exception">
    /// Thrown when the request to the Steam API fails or the response cannot be deserialized.
    /// </exception>
    public async Task<IEnumerable<Game>> GetRecentlyPlayedGamesAsync(string steamId)
    {
        if (string.IsNullOrWhiteSpace(steamId))
            throw new ArgumentException("Steam ID cannot be null or empty.", nameof(steamId));
        
        string url = $"{SteamConstants.Endpoints.GetRecentlyPlayedGames}?{SteamConstants.Parameters.Key}={_client.ApiKey}&{SteamConstants.Parameters.SteamId}={steamId}";
        HttpResponseMessage response = await _client.HttpClient.GetAsync(url);
        
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Failed to get recently played games: {response.ReasonPhrase}");

        using var payload = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
        
        return payload.RootElement
            .GetProperty(SteamConstants.Namespaces.Response)
            .GetProperty(SteamConstants.Namespaces.Games)
            .Deserialize<IEnumerable<Game>>() ?? throw new Exception("Failed to deserialize recently played games.");
    }
}