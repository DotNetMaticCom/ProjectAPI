using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StarterProject.ApiClient.Abstractions;
using StarterProject.ApiClient.Extensions;
using StarterProject.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add authentication
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

// Add API client
builder.Services.AddApiClient(builder.Configuration);

// Add client-specific services
builder.Services.AddScoped<ITokenStorage, ClientTokenStorage>();
builder.Services.AddScoped<Microsoft.AspNetCore.Components.Authorization.AuthenticationStateProvider, StarterProject.Web.Client.Auth.JwtAuthenticationStateProvider>();

await builder.Build().RunAsync();
