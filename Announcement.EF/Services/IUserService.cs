using Announcements.EF.Models;

namespace Announcements.EF.Services
{
    public interface IUserService
    {
        public Task<User> Get(Guid id, CancellationToken  cancellationToken);
    }
}
