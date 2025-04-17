﻿namespace Steam.Constants;

public static class SteamConstants
{
    public static class Endpoint
    {
        public const string GetPlayerSummaries = "ISteamUser/GetPlayerSummaries/v0002/";
        public const string GetOwnedGames = "IPlayerService/GetOwnedGames/v1/";
        public const string GetFriendList = "ISteamUser/GetFriendList/v1/";
    }

    public static class Parameters
    {
        public const string Key = "key";
        public const string SteamIds = "steamids";
        public const string SteamId = "steamid";
    }
    
    public static class Namespace
    {
        public const string Players = "players";
        public const string Response = "response";
        public const string Games = "games";
        public const string FriendList = "friendslist";
        public const string Friends = "friends";
        public const string AppNews = "appnews";
    }
}