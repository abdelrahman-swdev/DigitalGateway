using DigitalGateway.Core.ResponsesTypes;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace DigitalGateway.API.Handlers;

internal sealed class GlobalExceptionHandler : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(
		HttpContext httpContext,
		Exception exception,
		CancellationToken cancellationToken)
	{
        // if there is an exception
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = ApiResponse<string>.ServerError(exception);
        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var json = JsonSerializer.Serialize(response, serializeOptions);

        await httpContext.Response.WriteAsync(json);

        return true;
	}
}
