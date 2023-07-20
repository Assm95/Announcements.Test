using Announcements.Test.Domain.Entities;

namespace Announcements.Test.Application.Interfaces.Repositories
{
    public interface IAnnouncementsRepository
    {
        Task<Announcement?> FindByNumberAsync(int number, CancellationToken cancellationToken);
    }
}
