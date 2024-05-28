using DigitalGateway.Services.DTOs;
using DigitalGateway.Services.RequestService;
using Microsoft.AspNetCore.Mvc;

namespace DigitalGateway.API.Controllers;

public class RequestController : BaseApiController
{
    private readonly IRequestService _requestService;

    public RequestController(IRequestService requestService)
    {
        _requestService = requestService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RequestToSendDto requestToSendDto)
    {
        return ProcessResponse(await _requestService.SendRequestAsync(requestToSendDto));
    }
}
