using Announcements.Test.Application.Interfaces.Repositories;
using Announcements.Test.Domain.Common;
using Announcements.Test.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Announcements.Test.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Entities => _dbContext.Set<T>();
        
        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => 
            await _dbContext.Set<T>()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken) =>
            await _dbContext.Set<T>()
                .ToListAsync(cancellationToken);


        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbContext.Set<T>().AddAsync(entity, cancellationToken);

            return entity;
        }

        public Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            T? exist = _dbContext.Set<T>().Find(entity.Id);
#pragma warning disable CS8634
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
#pragma warning restore CS8634
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }
    }
}
