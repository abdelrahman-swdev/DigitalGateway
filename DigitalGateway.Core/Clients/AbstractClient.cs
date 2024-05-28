using DigitalGateway.Core.Exceptions;
using System.Text;

namespace DigitalGateway.Core.Clients;

public abstract class AbstractClient
{
    protected HttpClient HttpClient { get; set; }

    protected async Task<string> RequestAsync(
        HttpMethod httpMethod,
        string endPoint,
        string content = null,
        IDictionary<string, string> headers = null,
        string mediaType = null,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(httpMethod, endPoint);

        try
        {
            var mediaTypeResult = string.IsNullOrWhiteSpace(mediaType) ? "application/json" : mediaType;
            if ((httpMethod == HttpMethod.Post || httpMethod == HttpMethod.Put) && content != null)
            {
                request.Content = new StringContent(content, Encoding.UTF8, mediaTypeResult);
            }

            AddRequestHeaders(headers, request);

            var rawResponse = await HttpClient.SendAsync(request, cancellationToken);
            var responseContent = await rawResponse.Content.ReadAsStringAsync(cancellationToken);

            rawResponse.EnsureSuccessStatusCode();

            return responseContent;
        }
        catch (HttpRequestException ex)
        {
            throw new IntegrationException($"Integration Api error, BaseUrl: {HttpClient.BaseAddress}, ex: {ex.Message}", ex.InnerException);
        }
        finally
        {
            request.Dispose();
        }
    }

    private static void AddRequestHeaders(IDictionary<string, string>? headers, HttpRequestMessage request)
    {
        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }
    }
}

