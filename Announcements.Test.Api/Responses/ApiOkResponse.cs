namespace Announcements.Test.WebApi.Responses
{
    public class ApiOkResponse<T> : ApiResponse
    {
        public T Result { get; }

        public ApiOkResponse(T result)
            : base(StatusCodes.Status200OK)
        {
            Result = result;
        }
    }
}
