using DigitalGateway.Core.ResponsesTypes;
using DigitalGateway.Services.DTOs;

namespace DigitalGateway.Services.RequestService;
public interface IRequestService
{
    Task<ApiResponse<string>> SendRequestAsync(RequestToSendDto requestToSendDto);
}
