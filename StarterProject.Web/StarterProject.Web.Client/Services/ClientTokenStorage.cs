using Microsoft.JSInterop;
using StarterProject.ApiClient.Abstractions;

namespace StarterProject.Web.Client.Services;

public class ClientTokenStorage : ITokenStorage
{
    private readonly IJSRuntime _jsRuntime;
    private const string TokenKey = "authToken";
    private const string RefreshTokenKey = "refreshToken";

    public ClientTokenStorage(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task StoreTokenAsync(string token)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, token);
    }

    public async Task StoreRefreshTokenAsync(string refreshToken)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", RefreshTokenKey, refreshToken);
    }

    public async Task<string?> GetTokenAsync()
    {
        return await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", TokenKey);
    }

    public async Task<string?> GetRefreshTokenAsync()
    {
        return await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", RefreshTokenKey);
    }

    public async Task ClearTokensAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", RefreshTokenKey);
    }
}