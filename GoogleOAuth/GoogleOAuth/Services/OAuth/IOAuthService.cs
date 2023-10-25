namespace GoogleOAuth.Services;

public interface IOAuthService
{
    Task<string> GetAccessToken(string authorizationCode);
}