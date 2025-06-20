using System.Net.Http.Headers;
using StarterProject.ApiClient.Abstractions;

namespace StarterProject.ApiClient.Http;

public class AuthenticationHttpMessageHandler : DelegatingHandler
{
    private readonly ITokenStorage _tokenStorage;

    public AuthenticationHttpMessageHandler(ITokenStorage tokenStorage)
    {
        _tokenStorage = tokenStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _tokenStorage.GetTokenAsync();
        
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}