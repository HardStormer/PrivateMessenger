using AutoMapper.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GMD.PrivateMessenger.PL.API.Middlewares;
/// <summary>
/// Класс ExceptionHandlingMiddleware представляет промежуточное ПО (middleware) для обработки исключений в запросах HTTP.
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    /// <summary>
    /// Конструктор класса, который принимает ссылку на следующий делегат запроса и интерфейс ILogger. Он сохраняет ссылку на следующий делегат и интерфейс ILogger для использования в методе InvokeAsync.
    /// </summary>
    /// <param name="next"></param>
    /// <param name="logger"></param>
    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    /// <summary>
    /// Метод, который обрабатывает запросы, перехватывает исключения и обрабатывает их в соответствии с определенными правилами. Метод вызывает следующий делегат в конвейере middleware (httpContext), и если происходит исключение, оно перехватывается и обрабатывается соответствующим обработчиком.
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ValidationException ex)
        {
            HandleValidationException(
                httpContext,
                ex,
                HttpStatusCode.Forbidden);
        }
        catch (ExpectedException ex)
        {
            HandleExpectedException(
                httpContext,
                ex,
                ex.httpStatusCode);
        }
        catch (NotFoundException ex)
        {
            HandleExpectedException(
                httpContext,
                ex,
                HttpStatusCode.NotFound);
        }
        catch (Exception ex)
        {
            HandleException(
                httpContext,
                ex,
                HttpStatusCode.InternalServerError,
                "Internal server error");
        }
    }
    private void HandleValidationException(HttpContext context, ValidationException ex, HttpStatusCode httpStatusCode)
    {

    }
    private void HandleExpectedException(HttpContext context, Exception ex, HttpStatusCode httpStatusCode)
    {
        _logger.LogError(JsonConvert.SerializeObject(new
        {
            ex.Message,
            ex.Source,
            ex.StackTrace
        }));

        HttpResponse response = context.Response;

        response.ContentType = "application/json";
        response.StatusCode = (int)httpStatusCode;
    }
    private void HandleException(HttpContext context, Exception ex, HttpStatusCode httpStatusCode, string message)
    {
        _logger.LogError(JsonConvert.SerializeObject(new
        {
            ex.Message,
            ex.Source,
            ex.StackTrace
        }));

        HttpResponse response = context.Response;

        response.ContentType = "application/json";
        response.StatusCode = (int)httpStatusCode;
    }
}
