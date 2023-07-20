using Announcements.Test.Application.DTO;

namespace Announcements.Test.Application.Interfaces
{
    public interface IFileStorage
    {
        Task<FileDto?> GetFileAsync(string name, byte[] data);

        Task<byte[]?> GetFileDataAsync(string name);
    }
}
