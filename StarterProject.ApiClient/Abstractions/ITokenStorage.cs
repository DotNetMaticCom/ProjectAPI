namespace StarterProject.ApiClient.Abstractions;

public interface ITokenStorage
{
    Task StoreTokenAsync(string token);
    Task StoreRefreshTokenAsync(string refreshToken);
    Task<string?> GetTokenAsync();
    Task<string?> GetRefreshTokenAsync();
    Task ClearTokensAsync();
}