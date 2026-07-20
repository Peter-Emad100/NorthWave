using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace NorthWave.Middlewares
{
    
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"An unhandled exception occurred while processing {Method} {Path}",
                httpContext.Request.Method,httpContext.Request.Path);

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = GetStatusCode(ex);
                var response = new { 
                    statusCode = httpContext.Response.StatusCode,
                    message = httpContext.Response.StatusCode == (int)HttpStatusCode.InternalServerError
                ? "An unexpected error occurred.": ex.Message
                };
                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }
        private static int GetStatusCode(Exception ex)
        {
            return ex switch
            {
                ArgumentException => (int)HttpStatusCode.BadRequest,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };
        }
    }


    
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
