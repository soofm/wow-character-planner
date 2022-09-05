using CharacterPlanner.Server.Interfaces;
using CharacterPlanner.Shared.Models;
using IdentityModel.Client;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace CharacterPlanner.Server.Services;

public class BnetTokenService : IBnetTokenService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _memoryCache;
    private readonly ClientCredentials _clientCredentials;
    private const string _tokenCacheKey = "client_token";

    public BnetTokenService(
        IHttpClientFactory httpClientFactory,
        IMemoryCache memoryCache,
        IOptions<ClientCredentials> credentials)
    {
        _httpClient = httpClientFactory.CreateClient();
        _memoryCache = memoryCache;
        _clientCredentials = credentials.Value;
    }

    public async Task<string> GetClientCredentialsToken()
    {
        var isCached = _memoryCache.TryGetValue<string>(_tokenCacheKey, out var clientToken);
        if (isCached)
        {
            return clientToken;
        }

        var uri = new Uri($"https://us.battle.net/oauth/token");
        
        var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = uri.ToString(),
            ClientId = _clientCredentials.ClientId,
            ClientSecret = _clientCredentials.ClientSecret
        });

        if (tokenResponse.IsError || (int)tokenResponse.HttpStatusCode < 200 || (int)tokenResponse.HttpStatusCode > 299)
        {
            throw new Exception("Error getting token");
        }

        _memoryCache.Set<string>(_tokenCacheKey, tokenResponse.AccessToken, TimeSpan.FromSeconds(Math.Max(tokenResponse.ExpiresIn - 60, 0)));

        return tokenResponse.AccessToken;
    }

    public async Task<string> GetAuthorizationCodeToken(string userId)
    {
        var isCached = _memoryCache.TryGetValue<string>($"user:{userId}", out var authToken);
        if (isCached)
        {
            return authToken;
        }

        var uri = new Uri($"https://us.battle.net/oauth/token");

        var tokenResponse = await _httpClient.RequestAuthorizationCodeTokenAsync(new AuthorizationCodeTokenRequest
        {
            Address = uri.ToString(),
            ClientId = _clientCredentials.ClientId,
            ClientSecret = _clientCredentials.ClientSecret,
            Parameters =
            {
                { "scope", "wow.profile" }
            }
        });

        if (tokenResponse.IsError)
        {
            throw new Exception("Error getting token");
        }

        _memoryCache.Set<string>($"user:{userId}", tokenResponse.AccessToken, TimeSpan.FromSeconds(Math.Max(tokenResponse.ExpiresIn - 60, 0)));


        return tokenResponse.AccessToken;
    }
}
