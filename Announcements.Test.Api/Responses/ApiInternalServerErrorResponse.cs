namespace Announcements.Test.WebApi.Responses
{
    public class ApiInternalServerErrorResponse : ApiResponse
    {
        public string? Details { get; }

        public ApiInternalServerErrorResponse(string? message = null, string? details = null) : base(StatusCodes.Status500InternalServerError, message)
        {
            Details = details;
        }
    }
}
