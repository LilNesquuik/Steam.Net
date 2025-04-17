using Steam.Services;

namespace Steam;

public class SteamClient :
    IDisposable
{
    internal readonly string ApiKey;
    internal readonly HttpClient HttpClient;
    
    public UserService User { get; set; }
    public PlayerService Player { get; set; }
    
    public SteamClient(string apiKey)
    {
        ApiKey = apiKey;
        HttpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://api.steampowered.com/")
        };
        
        HttpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        HttpClient.DefaultRequestHeaders.Add("User-Agent", "Steam.Net v1.0");
        
        User = new UserService(this);
        Player = new PlayerService(this);
    }
    
    public void Dispose()
    {
        HttpClient.Dispose();
    }
}