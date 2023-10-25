using GoogleOAuth.Services.OAuth.Model;
using Microsoft.Extensions.Options;

namespace GoogleOAuth.Services.OAuth;

public class OAuthService : IOAuthService
{
    private readonly OAuthConfiguration _oauthConfig;

    public OAuthService(IOptions<OAuthConfiguration> configuration)
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
    private const string CallbackUrl = "http://localhost:5000/cb";

    public async Task<string> GetAccessToken(string authorizationCode)
    {
        string access_token = string.Empty;
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
                new KeyValuePair<string, string>(RedirectUri, CallbackUrl)
            });

            HttpResponseMessage response = await client.PostAsync("", requestContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<OAuthTokenResponse>();
                access_token = responseContent?.AccessToken;
            }
            else
            {
            }
        }

        return access_token;
    }
}