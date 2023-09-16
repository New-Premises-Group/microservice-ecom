﻿using System.Net;
using System.Text.Json;

namespace IW.Exceptions;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

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

            switch (error)
            {
                case AppException:
                    // custom application error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException:
                    // not found error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    // unhandled error
                    _logger.LogError(error, error.Message);
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new { message = error?.Message });
            await response.WriteAsync(result);
        }
    }
}
