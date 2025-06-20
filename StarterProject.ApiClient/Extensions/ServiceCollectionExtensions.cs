using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StarterProject.ApiClient.Abstractions;
using StarterProject.ApiClient.Configuration;
using StarterProject.ApiClient.Http;
using StarterProject.ApiClient.Services;

namespace StarterProject.ApiClient.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiClientConfiguration>(configuration.GetSection("ApiClient"));
        
        services.AddTransient<AuthenticationHttpMessageHandler>();
        
        services.AddHttpClient<IApiClient, Http.ApiClient>()
            .ConfigureHttpClient((serviceProvider, client) =>
            {
                var config = configuration.GetSection("ApiClient").Get<ApiClientConfiguration>() 
                    ?? new ApiClientConfiguration();
                client.BaseAddress = new Uri(config.BaseUrl);
            })
            .AddHttpMessageHandler<AuthenticationHttpMessageHandler>();
        
        services.AddScoped<IAuthService, AuthService>();
        
        return services;
    }
}