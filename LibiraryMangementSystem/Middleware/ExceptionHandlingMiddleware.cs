using LibraryManagementSystem.Application.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;


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
        _logger.LogError("error ::", exception.Message);
        GeneralResponse<Exception> response =  exception switch
        {
            ArgumentNullException ex => new GeneralResponse<Exception>(null, false, ex.Message , HttpStatusCode.Forbidden),
            KeyNotFoundException ex => new GeneralResponse<Exception>(null, false, ex.Message, HttpStatusCode.NotFound),
            Exception ex => new GeneralResponse<Exception>(null, false, ex.Message, HttpStatusCode.ServiceUnavailable)
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.OK;
        await context.Response.WriteAsJsonAsync(response);
    }
}

