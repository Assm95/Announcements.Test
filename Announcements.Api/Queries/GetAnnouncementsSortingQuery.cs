namespace Announcements.WebApi.Queries
{
    public class GetAnnouncementsSortingQuery
    {
        public string? ColumnName { get; set; }

        public string? SortOrder { get; set; }
    }
}
