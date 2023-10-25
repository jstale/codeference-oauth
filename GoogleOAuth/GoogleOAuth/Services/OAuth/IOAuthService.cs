namespace GoogleOAuth.Services.OAuth;

public interface IOAuthService
{
    Task<string> GetAccessToken(string authorizationCode);
}