public class CustomRateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomRateLimitingMiddleware> _logger;

    public CustomRateLimitingMiddleware(RequestDelegate next, ILogger<CustomRateLimitingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex) when (context.Response.StatusCode == 429)
        {
            _logger.LogWarning("Rate limit exceeded for IP: {IpAddress}",
                context.Connection.RemoteIpAddress);

            context.Response.ContentType = "application/json";

            var response = new
            {
                error = "Muitas requisições detectadas. Por favor, aguarde alguns minutos antes de tentar novamente.",
                retryAfter = context.Response.Headers["Retry-After"]
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}

// Extension method para facilitar o uso
public static class CustomRateLimitingMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomRateLimiting(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomRateLimitingMiddleware>();
    }
}