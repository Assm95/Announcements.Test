namespace Announcements.Test.WebApi.Responses
{
    public class ApiPagedOkResponse<T> : ApiResponse
    {
        public T Result { get; }

        public int PageNumber { get; }

        public int PageSize { get; }

        public long TotalCount { get; }

        public int PagesCount { get; }

        public ApiPagedOkResponse(T result, int pageNumber, int pageSize, long totalCount, int pagesCount) : base(StatusCodes.Status200OK)
        {
            Result = result;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            PagesCount = pagesCount;
        }
    }
}
