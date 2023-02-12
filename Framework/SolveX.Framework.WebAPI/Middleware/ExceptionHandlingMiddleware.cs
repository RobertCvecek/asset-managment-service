using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using SolveX.Framework.Domain.Exceptions;
using SolveX.Framework.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SolveX.Framework.WebAPI.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        this.next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception e)
        {
            _logger.LogError($"Exception occurred with message [{e.Message}], trace [{Activity.Current?.Id ?? context?.TraceIdentifier}] and status code [{GetStatusCode(e)}]");

            var response = context?.Response;
            if (response.HasStarted)
            {
                throw;
            }

            response.StatusCode = GetStatusCode(e);
            response.ContentType = "application/json; charset=utf-8";
            response.Headers[HeaderNames.CacheControl] = "no-cache";
            response.Headers[HeaderNames.Pragma] = "no-cache";
            response.Headers[HeaderNames.Expires] = "-1";
            response.Headers.Remove(HeaderNames.ETag);

            await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResponse
            {
                Message = e.Message,
                Code = response.StatusCode
            },
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));
        }
    }

    private int GetStatusCode(Exception exception)
    {
        return exception.GetType().Name switch
        {
            nameof(BadDataException) => (int)HttpStatusCode.BadRequest,
            nameof(UnauthorizedException) => (int)HttpStatusCode.Unauthorized,
            _ => (int)HttpStatusCode.InternalServerError,
        };
    }
}
