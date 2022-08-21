using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CharacterPlanner.Application.Common.Interfaces;
using CharacterPlanner.Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace CharacterPlanner.Infrastructure.Bnet;

public class BnetTokenService : IBnetTokenService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _memoryCache;
    private const string _tokenCacheKey = "client_token";

    public BnetTokenService(
        IOptions<ClientCredentials> credentials,
        IHttpClientFactory httpClientFactory,
        IMemoryCache memoryCache)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(Encoding.UTF8.GetBytes($"{credentials.Value.ClientId}:{credentials.Value.ClientSecret}")));
        _memoryCache = memoryCache;
    }

    public async Task<string> GetToken()
    {
        var isCached = _memoryCache.TryGetValue<string>(_tokenCacheKey, out var clientToken);
        if (isCached)
        {
            return clientToken;
        }

        var builder = new UriBuilder($"https://us.battle.net");
        builder.Path = "/oauth/token";
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, builder.Uri);
        var reqContent = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
        {
            new("grant_type", "client_credentials")
        });
        httpRequestMessage.Content = reqContent;

        var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception("Failed to get token");
        }

        var resContent = await httpResponseMessage.Content.ReadFromJsonAsync<TokenResponse>(new JsonSerializerOptions());

        if (resContent == null)
        {
            throw new Exception("Failed to parse token");
        }

        _memoryCache.Set<string>(_tokenCacheKey, resContent.AccessToken!, TimeSpan.FromSeconds(Math.Max(resContent.ExpiresIn - 60, 0)));

        return resContent.AccessToken!;
    }
}
