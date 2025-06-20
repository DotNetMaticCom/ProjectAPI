using StarterProject.ApiClient.Abstractions;
using StarterProject.ApiClient.Models.Auth;

namespace StarterProject.ApiClient.Services;

public class AuthService : IAuthService
{
    private readonly IApiClient _apiClient;
    private readonly ITokenStorage _tokenStorage;

    public AuthService(IApiClient apiClient, ITokenStorage tokenStorage)
    {
        _apiClient = apiClient;
        _tokenStorage = tokenStorage;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _apiClient.PostAsync<LoginRequest, LoginResponse>("Auth/Login", request, cancellationToken);
        
        if (response != null && response.AccessToken != null)
        {
            await _tokenStorage.StoreTokenAsync(response.AccessToken.Token);
            if (!string.IsNullOrEmpty(response.RefreshToken))
            {
                await _tokenStorage.StoreRefreshTokenAsync(response.RefreshToken);
            }
        }
        
        return response ?? throw new InvalidOperationException("Login failed");
    }

    public async Task<LoginResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _apiClient.PostAsync<RegisterRequest, LoginResponse>("Auth/Register", request, cancellationToken);
        
        if (response != null && response.AccessToken != null)
        {
            await _tokenStorage.StoreTokenAsync(response.AccessToken.Token);
            if (!string.IsNullOrEmpty(response.RefreshToken))
            {
                await _tokenStorage.StoreRefreshTokenAsync(response.RefreshToken);
            }
        }
        
        return response ?? throw new InvalidOperationException("Registration failed");
    }

    public async Task<TokenResponse> RefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        var response = await _apiClient.GetAsync<RefreshTokenResponse>("Auth/RefreshToken", cancellationToken);
        
        if (response != null && response.AccessToken != null)
        {
            await _tokenStorage.StoreTokenAsync(response.AccessToken.Token);
            if (!string.IsNullOrEmpty(response.RefreshToken))
            {
                await _tokenStorage.StoreRefreshTokenAsync(response.RefreshToken);
            }
            return response.AccessToken;
        }
        
        throw new InvalidOperationException("Token refresh failed");
    }

    public async Task LogoutAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var refreshToken = await _tokenStorage.GetRefreshTokenAsync();
            if (!string.IsNullOrEmpty(refreshToken))
            {
                await _apiClient.PutAsync<object?, object>("Auth/RevokeToken", null, cancellationToken);
            }
        }
        finally
        {
            await _tokenStorage.ClearTokensAsync();
        }
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var token = await _tokenStorage.GetTokenAsync();
        return !string.IsNullOrEmpty(token);
    }

    public async Task<string?> GetCurrentUserIdAsync()
    {
        var token = await _tokenStorage.GetTokenAsync();
        if (string.IsNullOrEmpty(token))
            return null;

        // TODO: Decode JWT to get user ID
        return null;
    }
}

public class RefreshTokenResponse
{
    public TokenResponse AccessToken { get; set; } = new();
    public string RefreshToken { get; set; } = string.Empty;
}