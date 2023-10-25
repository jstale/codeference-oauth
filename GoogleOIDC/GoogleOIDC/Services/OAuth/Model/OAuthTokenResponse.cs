using System.Text.Json.Serialization;

namespace GoogleOAuth.Services.Model;

public class OAuthTokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } 
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } 
    [JsonPropertyName("error")]
    public string Error { get; set; } 
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }
    [JsonPropertyName("id_token")]
    public string IdToken { get; set; } 
    
}