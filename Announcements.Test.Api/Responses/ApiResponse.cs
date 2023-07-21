using Microsoft.AspNetCore.WebUtilities;

namespace Announcements.Test.WebApi.Responses
{
    public class ApiResponse
    {
        public int StatusCode { get; }

        public string Message { get; }

        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? ReasonPhrases.GetReasonPhrase(statusCode);
        }
    }
}
