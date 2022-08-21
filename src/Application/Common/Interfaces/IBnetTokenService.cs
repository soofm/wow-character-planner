namespace CharacterPlanner.Application.Common.Interfaces;

public interface IBnetTokenService
{
    public Task<string> GetToken();
}
