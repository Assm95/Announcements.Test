using Announcements.Test.Application.DTO;

namespace Announcements.Test.Application.Interfaces
{
    public interface IFileStorage
    {
        Task<FileDto?> SaveFileAsync(string name, byte[] data);

        Task<bool> FileExistAsync(string path);
    }
}
