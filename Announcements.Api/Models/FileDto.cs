namespace Announcements.WebApi.Models
{
    public class FileDto
    {
        public string Name { get; set; } = null!;

        public byte[] Data { get; set; } = null!;
    }
}
