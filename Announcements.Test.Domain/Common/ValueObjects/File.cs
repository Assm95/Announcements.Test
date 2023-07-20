namespace Announcements.Test.Domain.Common.ValueObjects
{
    public class File
    {
        public string Name { get; }

        public string? Extension { get; }

        public string Path { get; }

        public string NameWithExtension => Name + (!string.IsNullOrWhiteSpace(Extension) ? "." + Extension : null);

#pragma warning disable CS8618
        protected File() { }
#pragma warning restore CS8618

        public File(string name, string? extension, string path)
        {
            Name = name;
            Extension = extension;
            Path = path;
        }

        public override string ToString()
        {
            return NameWithExtension;
        }
    }
}
