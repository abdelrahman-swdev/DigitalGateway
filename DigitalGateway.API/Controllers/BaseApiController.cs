using DigitalGateway.Core.ResponsesTypes;
using Microsoft.AspNetCore.Mvc;

namespace DigitalGateway.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
    protected ActionResult ProcessResponse<T>(ApiResponse<T> response)
    {
        return response.IsSucceeded
            ? Ok(response.Data)
            : StatusCode((int)response.HttpErrorCode, new
            {
                response.HttpErrorCode,
                response.IsSucceeded,
                response.ErrorMessage,
            });
    }
}
