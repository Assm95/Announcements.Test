namespace Announcements.Test.Application.DTO
{
    public class AnnouncementDto
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public UserDto User { get; set; } = null!;

        public string Text { get; set; } = null!;

        public ImageDto Image { get; set; } = null!;

        public int Rating { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
