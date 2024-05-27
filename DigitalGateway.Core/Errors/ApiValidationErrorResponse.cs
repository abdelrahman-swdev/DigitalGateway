using DigitalGateway.Core.Enums;
using DigitalGateway.Core.ResponsesTypes;

namespace DigitalGateway.Core.Errors;
public class ApiValidationErrorResponse : ApiResponse<string>
{
    public IEnumerable<string> Errors { get; set; }

    public static ApiValidationErrorResponse ValidationFailed(IEnumerable<string> errors)
    {
        return new ApiValidationErrorResponse
        {
            HttpErrorCode = HttpErrorCodes.InvalidInput,
            ErrorMessage = CommonErrorCodes.INVALID_INPUT.ToString(),
            Errors = errors,
            Status = OperationOutputStatus.Fail.ToString(),
        };
    }
}
