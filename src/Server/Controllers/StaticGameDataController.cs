using System.Net.Http.Headers;
using CharacterPlanner.Server.Interfaces;
using CharacterPlanner.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace CharacterPlanner.Server.Controllers;

[ApiController]
[Route("api/static")]
public class StaticGameDataController : ControllerBase
{
    private readonly ILogger<StaticGameDataController> _logger;
    private readonly HttpClient _httpClient;
    private readonly IBnetTokenService _bnetTokenService;

    public StaticGameDataController(
        ILogger<StaticGameDataController> logger,
        IHttpClientFactory httpClientFactory,
        IBnetTokenService bnetTokenService)
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient();
        _bnetTokenService = bnetTokenService;
    }

    [HttpGet("mounts")]
    public async Task<ActionResult<Mount[]>> ListMounts()
    {
        var region = "us"; // todo: allow other regions
        var locale = "en_US";

        var token = await _bnetTokenService.GetClientCredentialsToken();

        var builder = new UriBuilder($"https://{region}.api.blizzard.com");
        builder.Path = "/data/wow/mount/index";
        builder.Query = $"locale={locale}";
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, builder.Uri)
        {
            Headers =
            {
                { "Battlenet-Namespace", $"static-{region}" }
            }
        };
        httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);

        var content = await httpResponseMessage.Content.ReadFromJsonAsync<ListMountsResponse>();

        if (content == null)
        {
            throw new Exception("No content");
        }

        return Ok(content.Mounts);
    }


}
