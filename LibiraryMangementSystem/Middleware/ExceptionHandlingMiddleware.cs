using System.Net;
using LibraryManagementSystem.Application.DTOs;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An unexpected error occurred.");



        ExceptionResponse response = exception switch
        {
            ArgumentNullException ex => new ExceptionResponse(HttpStatusCode.Forbidden, ex.Message),
            KeyNotFoundException ex => new ExceptionResponse(HttpStatusCode.NotFound, ex.Message),
            Exception ex => new ExceptionResponse(HttpStatusCode.InternalServerError, ex.Message)
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)response.StatusCode;
        await context.Response.WriteAsJsonAsync(response);
    }
}

