namespace Announcements.Test.Application.DTO
{
    public class PagedList<T>
    {
        public List<T> Data { get; }

        public int PageNumber { get; } = 0;

        public int PageSize { get; } = 0;

        public long TotalCount { get; } = 0;

        public int PagesCount { get; } = 0;

        public PagedList(List<T> data, int pageNumber, int pageSize, long totalCount, int pagesCount)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            PagesCount = pagesCount;
        }
    }
}
