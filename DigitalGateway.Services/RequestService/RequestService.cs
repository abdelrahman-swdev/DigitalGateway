using DigitalGateway.Core.Clients;
using DigitalGateway.Core.ResponsesTypes;
using DigitalGateway.Services.DTOs;

namespace DigitalGateway.Services.RequestService;
public class RequestService : AbstractClient, IRequestService
{
    public RequestService(IHttpClientFactory clientFactory)
    {
        var client = clientFactory.CreateClient("default");
        client.Timeout = TimeSpan.FromMinutes(5);
        HttpClient = client;
    }

    public async Task<ApiResponse<string>> SendRequestAsync(RequestToSendDto requestToSendDto)
    {
        HttpClient.BaseAddress = new Uri(requestToSendDto.BaseUrl.TrimEnd('/'));
        var result = await RequestAsync(new HttpMethod(requestToSendDto.Method), requestToSendDto.EndpointUrl, requestToSendDto.Body, requestToSendDto.Headers, requestToSendDto.MediaType);
        return ApiResponse<string>.Success(result);
    }
}
