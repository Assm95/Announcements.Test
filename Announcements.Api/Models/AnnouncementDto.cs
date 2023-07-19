namespace Announcements.WebApi.Models
{
    public class AnnouncementDto
    {
        public Guid Id { get; set; }
        
        public int Number { get; set; }

        public UserDto User { get; set; } = null!;

        public string Text { get; set; } = null!;

        public FileDto? Image { get; set; }

        public int Rating { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateOnly ExpirationDate { get; set;}
    }
}
