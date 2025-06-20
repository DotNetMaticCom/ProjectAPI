using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using StarterProject.ApiClient.Abstractions;

namespace StarterProject.Web.Server.Services;

public class ServerTokenStorage : ITokenStorage
{
    private readonly ProtectedLocalStorage _localStorage;
    private const string TokenKey = "authToken";
    private const string RefreshTokenKey = "refreshToken";

    public ServerTokenStorage(ProtectedLocalStorage localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task StoreTokenAsync(string token)
    {
        await _localStorage.SetAsync(TokenKey, token);
    }

    public async Task StoreRefreshTokenAsync(string refreshToken)
    {
        await _localStorage.SetAsync(RefreshTokenKey, refreshToken);
    }

    public async Task<string?> GetTokenAsync()
    {
        try
        {
            var result = await _localStorage.GetAsync<string>(TokenKey);
            return result.Success ? result.Value : null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<string?> GetRefreshTokenAsync()
    {
        try
        {
            var result = await _localStorage.GetAsync<string>(RefreshTokenKey);
            return result.Success ? result.Value : null;
        }
        catch
        {
            return null;
        }
    }

    public async Task ClearTokensAsync()
    {
        await _localStorage.DeleteAsync(TokenKey);
        await _localStorage.DeleteAsync(RefreshTokenKey);
    }
}