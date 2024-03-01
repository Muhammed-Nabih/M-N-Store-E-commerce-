using M_N_Store.Errors;
using System.Net;
using System.Text.Json;

namespace M_N_Store.Middleware
{
    public class ExceptionMiddleware
    {

        //******************** For 500 Error ********************//

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _hostEnvironment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment hostEnvironment)
        {
            _next = next;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
                _logger.LogInformation("Success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"This Error Come From Exception Middleware {ex.Message} !");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var response = _hostEnvironment.IsDevelopment()
                    ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiException((int)HttpStatusCode.InternalServerError);
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);

            }
        }
    }
}
