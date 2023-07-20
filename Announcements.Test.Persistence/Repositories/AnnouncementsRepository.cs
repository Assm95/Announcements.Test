using Announcements.Test.Application.Interfaces.Repositories;
using Announcements.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Announcements.Test.Persistence.Repositories
{
    public class AnnouncementsRepository : IAnnouncementsRepository
    {
        private readonly IGenericRepository<Announcement> _repository;

        public AnnouncementsRepository(IGenericRepository<Announcement> repository)
        {
            _repository = repository;
        }

        public async Task<Announcement?> FindByNumberAsync(int number, CancellationToken cancellationToken)
        {
            return await _repository.Entities.FirstOrDefaultAsync(x => x.Number == number, cancellationToken);
        }
    }
}
