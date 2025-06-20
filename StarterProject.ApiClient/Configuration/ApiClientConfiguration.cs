namespace StarterProject.ApiClient.Configuration;

public class ApiClientConfiguration
{
    public string BaseUrl { get; set; } = "http://localhost:5278/api";
    public int TimeoutSeconds { get; set; } = 30;
    public bool UseAuthentication { get; set; } = true;
    public string? ApiKey { get; set; }
}