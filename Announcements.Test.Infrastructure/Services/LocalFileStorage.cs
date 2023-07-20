using Announcements.Test.Application.DTO;
using Announcements.Test.Application.Interfaces;
using Announcements.Test.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace Announcements.Test.Infrastructure.Services
{
    public class LocalFileStorage : IFileStorage
    {
        private readonly LocalFileStorageOptions _options;

        public LocalFileStorage(IOptions<LocalFileStorageOptions> options)
        {
            _options = options.Value;
        }
        public async Task<FileDto?> GetFileAsync(string name, byte[] data)
        {
            string fileName = Path.Combine(_options.Path, name);
            await File.WriteAllBytesAsync(fileName, data);

            if (!File.Exists(fileName))
                return null;

            return new FileDto
            {
                Path = fileName,
                Name = name,
                Extension = Path.GetExtension(name).TrimStart('.')
            };
        }
    }
}
