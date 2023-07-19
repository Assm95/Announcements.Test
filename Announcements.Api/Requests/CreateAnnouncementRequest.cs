using Announcements.WebApi.Models;

namespace Announcements.WebApi.Requests
{
    public class CreateAnnouncementRequest
    {
        public Guid UserId { get; set; }

        public int Number { get; set; }

        public string Text { get; set; } = null!;

        public int Rating { get; set; }

        public FileDto Image { get; set; } = null!;

        public DateOnly ExpirationDate { get; set; }
    }
}
