using System.Net;
using System.Text.Json;
using CodigoHincha.Domain.Exceptions;

namespace CodigoHincha.API.Middleware;

public class GlobalExceptionHandlerMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionHandlerMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger = logger;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException exception)
        {
            _logger.LogWarning(
                exception,
                "Validation error: {Message}",
                exception.Message);

            await WriteErrorResponse(
                context,
                HttpStatusCode.BadRequest,
                "Bad request",
                exception.Message);
        }
        catch (InvalidCredentialsException exception)
        {
            _logger.LogWarning(
                exception,
                "Invalid credentials: {Message}",
                exception.Message);

            await WriteErrorResponse(
                context,
                HttpStatusCode.Unauthorized,
                "Unauthorized",
                exception.Message);
        }
        catch (RateLimitExceededException exception)
        {
            _logger.LogWarning(
                exception,
                "Rate limit exceeded: {Message}",
                exception.Message);

            await WriteErrorResponse(
                context,
                HttpStatusCode.TooManyRequests,
                "Too many requests",
                exception.Message);
        }
        catch (BusinessNotFoundException exception)
        {
            _logger.LogWarning(
                exception,
                "Business not found: {Message}",
                exception.Message);

            await WriteErrorResponse(
                context,
                HttpStatusCode.NotFound,
                "Not found",
                exception.Message);
        }
        catch (BusinessConflictException exception)
        {
            _logger.LogWarning(
                exception,
                "Business conflict: {Message}",
                exception.Message);

            await WriteErrorResponse(
                context,
                HttpStatusCode.Conflict,
                "Conflict",
                exception.Message);
        }
        catch (BusinessException exception)
        {
            _logger.LogWarning(
                exception,
                "Business error: {Message}",
                exception.Message);

            await WriteErrorResponse(
                context,
                HttpStatusCode.BadRequest,
                "Bad request",
                exception.Message);
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "An unhandled exception occurred: {Message}",
                exception.Message);

            await WriteErrorResponse(
                context,
                HttpStatusCode.InternalServerError,
                "Internal server error",
                "Error interno del servidor.");
        }
    }

    private static async Task WriteErrorResponse(
        HttpContext context,
        HttpStatusCode statusCode,
        string message,
        string detail)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            success = false,
            message = detail,
            code = (int)statusCode,
            payload = new
            {
                detail
            }
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}
