namespace CharacterPlanner.Application.Common.Interfaces;

public interface IBnetTokenService
{
    public Task<string> GetClientCredentialsToken();
    public Task<string> GetAuthorizationCodeToken(string userId);
}
