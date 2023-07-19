namespace Announcements.WebApi.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public bool IsAdmin { get; set; }
    }
}
