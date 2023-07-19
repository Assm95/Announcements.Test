using Announcements.WebApi.Models;

namespace Announcements.WebApi.Requests
{
    public class UpdateAnnouncementRequest
    {
        public string Text { get; set; } = null!;

        public FileDto Image { get; set; } = null!;

        public DateOnly ExpirationDate { get; set; }

        public int Rating { get; set; }

        public int Number { get; set; }

        public Guid UserId { get; set; }
    }
}
