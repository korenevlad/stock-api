using System.Reflection;

namespace Edu.StockApi.Web.Configuration.Middleware;

public class VersionMiddleware
{
    public VersionMiddleware(RequestDelegate next) { }
    
    public async Task InvokeAsync(HttpContext context)
    {
        var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "no version";
        await context.Response.WriteAsync(version);
    }
}