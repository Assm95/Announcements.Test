namespace Announcements.EF.Filters
{
    public class AnnouncementsFilter
    {
        public int? Number { get; set; }

        public Guid? UserId { get; set; }

        public int? Rating { get; set; }

        public string? CreatedAtStart { get; set; }

        public string? CreatedAtEnd { get; set; }

        public string? ExpirationDateStart { get; set; }

        public string? ExpirationDateEnd { get; set; }
    }
}
