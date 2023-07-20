using Announcements.Test.Domain.Common;

namespace Announcements.Test.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : BaseEntity;

        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
