namespace Announcements.EF.Models
{
    public class File
    {
        public string Name { get; }

        public string? Extension { get; }

        public byte[] Data { get; }

        public string NameWithExtension => Name + (!string.IsNullOrWhiteSpace(Extension) ? "." + Extension : null);

#pragma warning disable CS8618
        protected File() { }
#pragma warning restore CS8618

        public File(string name, string? extension, byte[] data)
        {
            Name = name;
            Extension = extension;
            Data = data;
        }

        public override string ToString()
        {
            return NameWithExtension;
        }
    }
}
