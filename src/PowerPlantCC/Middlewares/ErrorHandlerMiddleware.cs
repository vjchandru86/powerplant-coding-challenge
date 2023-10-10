using PowerPlantCC.Models.Response;
using System.Text.Json;

namespace PowerPlantCC.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                _logger.LogError(error, error.Message);
                await response.WriteAsync(JsonSerializer.Serialize(new ErrorResponse("Some error occurred while processing your request", StatusCodes.Status500InternalServerError)));
            }
        }
    }
}
