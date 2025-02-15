namespace CarSalesApplication.Presentation.Middleware;

using Microsoft.AspNetCore.Http;
using Serilog;
using System.Threading.Tasks;
using System.Diagnostics;

public class LogMiddleware
{
    private readonly RequestDelegate _next;

    public LogMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Log.Information("Middleware - Request: {Method} {Path}", context.Request.Method, context.Request.Path);
        var stopwatch = Stopwatch.StartNew();

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();
            var responseTime = stopwatch.ElapsedMilliseconds;

            Log.Information(
                "Middleware - Response {Method} {Path} responded {StatusCode} in {ResponseTime}ms",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                responseTime
            );
        }
    }
}
