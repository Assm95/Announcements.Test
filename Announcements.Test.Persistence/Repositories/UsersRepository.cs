using Announcements.Test.Application.Interfaces.Repositories;
using Announcements.Test.Domain.Entities;

namespace Announcements.Test.Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IGenericRepository<User> _repository;

        public UsersRepository(IGenericRepository<User> repository)
        {
            _repository = repository;
        }
    }
}
