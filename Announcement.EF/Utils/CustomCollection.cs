namespace Announcements.EF.Utils
{
    public class CustomCollection<T>
    {
        public List<T> Items { get; set; } = new List<T>();

        public long TotalCount { get; set; }
    }
}
