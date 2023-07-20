namespace Announcements.Test.Application.DTO
{
    public class FilterAnnouncementsDto
    {
        public int? Number { get; set; }

        public int? Rating { get; set; }

        public Guid? UserId { get; set; }

        public FilterDateDto? CreatedAt { get; set; }

        public FilterDateDto? ExpirationDate { get; set; }
    }
}
