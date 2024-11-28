using System.Text;

namespace Edu.StockApi.Web.Configuration.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await LogRequest(context);
        await _next(context);
    }

    private async Task LogRequest(HttpContext context)
    {
        try
        {
            if (context.Request.ContentLength > 0)
            {
                context.Request.EnableBuffering();
            
                var buffer = new byte[context.Request.ContentLength.Value];
                await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
                var bodyAsText = Encoding.UTF8.GetString(buffer);
                _logger.LogInformation($"Request Logged {bodyAsText}");

                context.Request.Body.Position = 0;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Couldn't log request body");
        }
    }
}