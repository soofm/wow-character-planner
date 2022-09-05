using CharacterPlanner.Server.Interfaces;

namespace CharacterPlanner.Server.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddSingleton<IBnetTokenService, BnetTokenService>();
        
        return services;
    }
}
