using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Travel.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            ProblemDetails problemDetails;

            switch (exception)
            {
                case KeyNotFoundException keyNotFoundEx:
                    problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status404NotFound,
                        Title = "Resource not found",
                        Detail = keyNotFoundEx.Message
                    };
                    break;

                case UnauthorizedAccessException _:
                    problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Title = "Unauthorized",
                        Detail = "Access denied."
                    };
                    break;

                case ArgumentNullException argNullEx:
                    problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Missing argument",
                        Detail = argNullEx.ParamName != null
                            ? $"Parameter '{argNullEx.ParamName}' is required."
                            : argNullEx.Message
                    };
                    break;

                case ArgumentException argEx:
                    problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Invalid argument",
                        Detail = argEx.Message
                    };
                    break;

                case InvalidOperationException invalidOpEx:
                    problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status409Conflict,
                        Title = "Operation conflict",
                        Detail = invalidOpEx.Message
                    };
                    break;

                case NotImplementedException _:
                    problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status501NotImplemented,
                        Title = "Not implemented",
                        Detail = "This functionality is not implemented."
                    };
                    break;

                default:
                    problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Title = "An error occurred",
                        Detail = "An unexpected error occurred."
                    };
                    break;
            }

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
