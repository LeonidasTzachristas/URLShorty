using Microsoft.Extensions.Primitives;

namespace URLShortener.Middleware;

public class ApiKeyMiddleware
{
    private const string ApiKeyHeaderName = "x-api-key";
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        var requiresApiKey = endpoint?.Metadata.GetMetadata<ApiKeyAttribute>() != null;
        
        if (!requiresApiKey)
        {
            await _next(context);
            return;
        }
        
        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("API Key is missing");
            return;
        }

        var allKeys = _configuration
                          .GetSection("ApiKeys").Get<List<string>>() ?? [];

        if (!allKeys.Contains(apiKey.ToString()))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Invalid API Key");
            return;
        }

        await _next(context);
    }
}