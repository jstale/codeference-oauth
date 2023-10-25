using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GoogleOAuth.Services;
using GoogleOAuth.Services.Model;
using Microsoft.Extensions.Options;

namespace GoogleOIDC.Services.OAuth;

public class OidcService : IOidcService
{
    private readonly OAuthConfiguration _oauthConfig;

    public OidcService(IOptions<OAuthConfiguration> configuration)
    {
        _oauthConfig = configuration.Value;
    }

    private const string GrantType = "grant_type";
    private const string Code = "code";
    private const string ClientId = "client_id";
    private const string ClientSecret = "client_secret";
    private const string AuthorizationCodeGrant = "authorization_code";
    private const string RedirectUri = "redirect_uri";
    private const string ApplicationXWwwFormUrlencoded = "application/x-www-form-urlencoded";
    private const string GoogleOAuthTokenUrl = "https://oauth2.googleapis.com/token";

    public async Task<ClaimsPrincipal> GetUser(string authorizationCode)
    {
        ClaimsPrincipal user = null;
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(GoogleOAuthTokenUrl);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(ApplicationXWwwFormUrlencoded));

            var requestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>(GrantType, AuthorizationCodeGrant),
                new KeyValuePair<string, string>(Code, authorizationCode),
                new KeyValuePair<string, string>(ClientId, _oauthConfig.ClientId),
                new KeyValuePair<string, string>(ClientSecret, _oauthConfig.ClientSecret),
                new KeyValuePair<string, string>(RedirectUri, _oauthConfig.RedirectUrl)
            });

            HttpResponseMessage response = await client.PostAsync("", requestContent);

            if (response.IsSuccessStatusCode)
            {
                var responses = await response.Content.ReadAsStringAsync();
                var responseContent = await response.Content.ReadFromJsonAsync<OAuthTokenResponse>();
                var token = responseContent?.IdToken;
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var name = jwtSecurityToken.Claims.First(claim => claim.Type == "name").Value;
                var email = jwtSecurityToken.Claims.First(claim => claim.Type == "email").Value;
                user = CreateClaimsPrincipal(name, email);
            }
            else
            {
                //THROW ERROR
            }
        }

        return user;
    }
    
    private ClaimsPrincipal CreateClaimsPrincipal(string name, string email)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, name), 
            new(ClaimTypes.Email, email), 
        };

        var identity = new ClaimsIdentity(claims, "CookieAuthScheme");
        return new ClaimsPrincipal(identity);
    }
}