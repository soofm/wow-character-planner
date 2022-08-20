using CharacterPlanner.Application.Common.Interfaces;

namespace CharacterPlanner.Infrastructure.Auth;

public class BnetTokenService : IBnetTokenService
{
    public string GetToken()
    {
        return "token"; // TODO
    }
}
