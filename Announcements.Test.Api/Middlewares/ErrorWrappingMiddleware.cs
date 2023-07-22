using Announcements.Test.WebApi.Responses;
using Newtonsoft.Json;

namespace Announcements.Test.WebApi.Middlewares
{
    public class ErrorWrappingMiddleware
    {
        private readonly RequestDelegate _next;
        readonly ILogger<ErrorWrappingMiddleware> _logger;

        public ErrorWrappingMiddleware(RequestDelegate next, ILogger<ErrorWrappingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            if (!context.Response.HasStarted && context.Response.StatusCode != StatusCodes.Status204NoContent)
            {
                context.Response.ContentType = "application/json";

                var response = new ApiResponse(context.Response.StatusCode);

                var json = JsonConvert.SerializeObject(response);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
