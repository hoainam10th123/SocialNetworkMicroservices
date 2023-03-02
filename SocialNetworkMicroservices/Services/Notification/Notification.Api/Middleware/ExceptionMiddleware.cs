using Notification.Application.Exceptions;
using System.Text.Json;

namespace Notification.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //var unitOfWork = context.RequestServices.GetService<IUnitOfWork>()!;
            // hoac inject vao InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                var statusCode = GetStatusCode(ex);
                context.Response.StatusCode = statusCode;

                var response = new
                {
                    title = GetTitle(ex),
                    status = statusCode,
                    detail = ex.Message,
                    errors = GetErrors(ex)
                };

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }

        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };

        private static string GetTitle(Exception exception) =>
            exception switch
            {
                ApplicationException applicationException => applicationException.Message,
                _ => "Server Error"
            };

        private static IEnumerable<string> GetErrors(Exception exception)
        {
            IEnumerable<string> errors = null;

            if (exception is ValidationException validationException)
            {
                errors = validationException.Errors.Where(e => e.Key.Length > 0)
                .SelectMany(x => x.Value)
                .Select(x => x).ToArray();
            }

            return errors;
        }
    }
}
