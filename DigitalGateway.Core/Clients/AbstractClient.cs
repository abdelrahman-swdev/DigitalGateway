using DigitalGateway.Core.Exceptions;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace DigitalGateway.Core.Clients;

public abstract class AbstractClient
{
    protected HttpClient HttpClient { get; set; }

    protected async Task<T> RequestAsync<T>(
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

            _ = rawResponse.EnsureSuccessStatusCode();

            return ReturnDeserializedResponse<T>(mediaTypeResult, responseContent);
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

    private static T ReturnDeserializedResponse<T>(string mediaTypeResult, string responseContent)
    {
        switch (mediaTypeResult)
        {
            case "application/json":
                return JsonSerializer.Deserialize<T>(responseContent);

            case "application/xml":
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StringReader reader = new StringReader(responseContent))
                {
                    return (T)serializer.Deserialize(reader);
                }

            default:
                return JsonSerializer.Deserialize<T>(responseContent);
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

