using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Text.Json;

namespace Talabat.APIs.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger , IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await InvokeAsync(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "Application/Json";
                var response = _env.IsDevelopment()?
                     new Errors.ApiExceptionErrorResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                     : new Errors.ApiExceptionErrorResponse((int)HttpStatusCode.InternalServerError);
                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var JsonResponse=JsonSerializer.Serialize(response, options );
                await context.Response.WriteAsync(JsonResponse);       
            }
        }
    }
}
