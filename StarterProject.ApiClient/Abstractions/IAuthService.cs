using StarterProject.ApiClient.Models.Auth;

namespace StarterProject.ApiClient.Abstractions;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task<LoginResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
    Task<TokenResponse> RefreshTokenAsync(CancellationToken cancellationToken = default);
    Task LogoutAsync(CancellationToken cancellationToken = default);
    Task<bool> IsAuthenticatedAsync();
    Task<string?> GetCurrentUserIdAsync();
}