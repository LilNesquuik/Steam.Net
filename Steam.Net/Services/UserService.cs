﻿using System.Text.Json;
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

    public async Task<IEnumerable<Player>> GetPlayersSummariesAsync(string[] steamIds)
    {
        if (steamIds == null || !steamIds.Any())
            throw new ArgumentException("Steam IDs cannot be null or empty.", nameof(steamIds));
        
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
    
    public async Task<IEnumerable<Ban>> GetPlayersBansAsync(string[] steamIds)
    {
        if (steamIds == null || !steamIds.Any())
            throw new ArgumentException("Steam IDs cannot be null or empty.", nameof(steamIds));
        
        string url = $"{SteamConstants.Endpoints.GetPlayerBans}?{SteamConstants.Parameters.Key}={_client.ApiKey}&{SteamConstants.Parameters.SteamIds}={string.Join(",", steamIds)}";
        HttpResponseMessage response = await _client.HttpClient.GetAsync(url);
        
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Failed to get player bans: {response.ReasonPhrase}");

        using var payload = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
        
        return payload.RootElement
            .GetProperty(SteamConstants.Namespaces.Players)
            .Deserialize<IEnumerable<Ban>>() ?? throw new Exception("Failed to deserialize player bans.");
    }
    
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