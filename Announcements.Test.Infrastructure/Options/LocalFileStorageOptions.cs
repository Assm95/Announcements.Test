namespace Announcements.Test.Infrastructure.Options
{
    public class LocalFileStorageOptions
    {
        public const string Position = "LocalFileStorage";

        public string Path { get; set; } = null!;
    }
}
