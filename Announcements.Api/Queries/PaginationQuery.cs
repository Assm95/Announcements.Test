namespace Announcements.WebApi.Queries
{
    public class PaginationQuery
    {
        public int PageStart { get; set; } = 0;

        public int PageCount { get; set; } = 10;
    }
}
