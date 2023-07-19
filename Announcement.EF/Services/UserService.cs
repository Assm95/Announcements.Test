using Announcements.EF.Exceptions;
using Announcements.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Announcements.EF.Services
{
    public class UserService : IUserService
    {
        private readonly AnnouncementDbContext _dbContext;

        public UserService(AnnouncementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Get(Guid id, CancellationToken cancellationToken)
        {
            User? user = await FindUserAsync(id, cancellationToken);

            if (user == null)
                throw new DomainNotFoundException("User");

            return user;
        }

        private async Task<User?> FindUserAsync(Guid id, CancellationToken cancellationToken) =>
            await _dbContext.Users
                .Include(x => x.Announcements)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
