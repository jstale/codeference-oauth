using System.Security.Claims;

namespace GoogleOIDC.Services.OAuth;

public interface IOidcService
{
    Task<ClaimsPrincipal> GetUser(string authorizationCode);
}