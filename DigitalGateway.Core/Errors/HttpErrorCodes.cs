namespace DigitalGateway.Core.Errors
{
    public enum HttpErrorCodes
    {
        None,
        InvalidInput = 400,
        NotAuthenticated = 401,
        NotAuthorized = 403,
        NotFound = 404,
        BusinessRuleViolation = 422,
        Conflict = 409,
        IntegrationCommunicationError = 502,
        ServerError = 500
    }
}
