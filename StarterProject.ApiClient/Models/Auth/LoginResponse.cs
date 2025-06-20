using System.Text.Json.Serialization;

namespace StarterProject.ApiClient.Models.Auth;

public class LoginResponse
{
    public TokenResponse AccessToken { get; set; } = new();
    
    [JsonIgnore]
    public string? RefreshToken { get; set; }
    
    public AuthenticatorType? RequiredAuthenticatorType { get; set; }
}

public class TokenResponse
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; }
}

public enum AuthenticatorType
{
    None = 0,
    Email = 1,
    Otp = 2
}