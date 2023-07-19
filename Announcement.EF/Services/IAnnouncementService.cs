using Announcements.EF.Filters;
using Announcements.EF.Models;
using Announcements.EF.Utils;
using File = Announcements.EF.Models.File;

namespace Announcements.EF.Services
{
    public interface IAnnouncementService
    {
        public Task<Announcement> CreateAsync(User user, int number, string text, File image, int rating, DateTime expirationDate, CancellationToken cancellationToken);

        public Task UpdateAsync(Guid id, User user, int number, string text, File image, int rating, DateTime expirationDate, CancellationToken cancellationToken);

        public Task RemoveAsync(Guid id, User user, CancellationToken cancellationToken);

        public Task<Announcement> GetAsync(Guid id, CancellationToken cancellationToken);

        public Task<CustomCollection<Announcement>> GetAllAsync(PaginationFilter? pagination,
            AnnouncementsFilter? filter, string? searchString, SortingFilter? sorting, CancellationToken cancellationToken);
    }
}
