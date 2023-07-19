namespace Announcements.WebApi.Models
{
    public class ListDto<T>
    {
        public long TotalCount { get; }

        public T[] Values { get; }

        public ListDto(IEnumerable<T> values, long totalCount)
        {
            Values = values.ToArray();
            TotalCount = totalCount;
        }
    }
}
