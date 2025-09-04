using Domain.Exceptions;
using Services.Exceptions_Implementation;
using Web_Api_Application.ErrorModels;

namespace Web_Api_Application.CustomMiddlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;
        public CustomExceptionMiddleware(ILogger<CustomExceptionMiddleware> logger, RequestDelegate next)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                await NotFoundResourceHandler(context);
            }
            catch (Exception ex)
            {
                await ExceptionsHandler(context, ex);
            }
        }

        private async Task ExceptionsHandler(HttpContext context, Exception ex)
        {
            var response = new ErrorModel()
            {
                message = ex.Message
            };

            switch (ex)
            {
                case NotFoundException notFoundException:
                    _logger.LogError(ex, "Not Found Exception: {Message}", ex.Message);
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    break;
                case BadRequestException badRequestException: 
                    _logger.LogError($"BadRequest: {ex.Message}");
                    context.Response.StatusCode = BadRequestErrorsInit(badRequestException , response);
                    break;
                default:
                    _logger.LogError(ex, "An unexpected error occurred: {Message}", ex.Message);
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            response.statusCode = context.Response.StatusCode;
            await context.Response.WriteAsJsonAsync(response);
        }

        private int BadRequestErrorsInit(BadRequestException badRequestException , ErrorModel response)
        {
            response.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }

        private async Task NotFoundResourceHandler(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                _logger.LogWarning("404 Not Found: {Path}", context.Request.Path);
                var response = new ErrorModel()
                {
                    statusCode = context.Response.StatusCode,
                    message = $"Resource {context.Request.Path} not found."
                };
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
