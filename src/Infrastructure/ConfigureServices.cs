using CharacterPlanner.Application.Common.Interfaces;
using CharacterPlanner.Infrastructure.Bnet;
using Microsoft.Extensions.DependencyInjection;

namespace CharacterPlanner.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IBnetTokenService, BnetTokenService>();
        return services;
    }
}
