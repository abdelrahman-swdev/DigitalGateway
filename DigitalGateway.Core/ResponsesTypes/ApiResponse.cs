using DigitalGateway.Core.Enums;
using DigitalGateway.Core.Errors;

namespace DigitalGateway.Core.ResponsesTypes;
public class ApiResponse<T>
{
    public T Data { get; set; }

    public string Status { get; set; }

    public HttpErrorCodes HttpErrorCode { get; set; }

    public bool IsSucceeded => Status == OperationOutputStatus.Success.ToString();

    public string ErrorMessage { get; set; }

    public static ApiResponse<T> Success(T result)
    {
        return new ApiResponse<T>()
        {
            Data = result,
            Status = OperationOutputStatus.Success.ToString()
        };
    }

    public static ApiResponse<T> Fail(HttpErrorCodes httpErrorCode = HttpErrorCodes.InvalidInput, string description = "")
    {
        return new ApiResponse<T>
        {
            HttpErrorCode = httpErrorCode,
            ErrorMessage = description,
            Status = OperationOutputStatus.Fail.ToString(),
        };
    }

    public static ApiResponse<T> ServerError(Exception ex)
    {
        return new ApiResponse<T>
        {
            HttpErrorCode = HttpErrorCodes.ServerError,
            ErrorMessage = ex.Message,
            Status = OperationOutputStatus.ServerError.ToString()
        };
    }
}