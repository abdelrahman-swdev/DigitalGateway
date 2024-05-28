namespace DigitalGateway.Services.DTOs
{
    public class RequestToSendDto
    {
        public string BaseUrl { get; set; }
        public string EndpointUrl { get; set; }
        public string Method { get; set; }
        public string Body { get; set; }
        public string MediaType { get; set; }
        public Dictionary<string, string> Headers { get; set; }
    }
}
