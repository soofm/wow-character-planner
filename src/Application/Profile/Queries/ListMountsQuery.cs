using CharacterPlanner.Application.Common.Interfaces;
using MediatR;

namespace CharacterPlanner.Application.Profile.Queries;

public class ListMountsQuery : IRequest<string>
{
}

public class ListMountsQueryHandler : IRequestHandler<ListMountsQuery, string>
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IBnetTokenService _bnetTokenService;

    public ListMountsQueryHandler(IHttpClientFactory httpClientFactory, IBnetTokenService bnetTokenService)
    {
        _httpClientFactory = httpClientFactory;
        _bnetTokenService = bnetTokenService;
    }

    public async Task<string> Handle(ListMountsQuery request, CancellationToken cancellationToken)
    {
        var region = "us"; // todo: allow other regions
        var locale = "en_US";

        var builder = new UriBuilder($"https://{region}.api.blizzard.com");
        builder.Path = "/data/wow/mount/index";
        builder.Query = $"locale={locale}";
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, builder.Uri)
        {
            Headers =
            {
                { "Battlenet-Namespace", $"static-{region}" },
                { "Bearer", _bnetTokenService.GetToken() }
            }
        };

        var httpClient = _httpClientFactory.CreateClient();
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

        var content = await httpResponseMessage.Content.ReadAsStringAsync();

        return content;
    }
}
