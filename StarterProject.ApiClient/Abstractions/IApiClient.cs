namespace StarterProject.ApiClient.Abstractions;

public interface IApiClient
{
    Task<TResponse?> GetAsync<TResponse>(string endpoint, CancellationToken cancellationToken = default);
    Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest request, CancellationToken cancellationToken = default);
    Task<TResponse?> PutAsync<TRequest, TResponse>(string endpoint, TRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(string endpoint, CancellationToken cancellationToken = default);
    Task<TResponse?> SendAsync<TRequest, TResponse>(HttpMethod method, string endpoint, TRequest? request = default, CancellationToken cancellationToken = default);
}