namespace Announcements.Test.WebApi.Responses
{
    public class ApiOk201Response<T> : ApiResponse
    {
        public T Result { get; }

        public ApiOk201Response(T result)
            : base(StatusCodes.Status201Created)
        {
            Result = result;
        }
    }
}
