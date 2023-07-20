using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Announcements.Test.Application.Interfaces.Repositories;
using Announcements.Test.Domain.Common;
using Announcements.Test.Persistence.Contexts;

namespace Announcements.Test.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private Hashtable? _repositories;
        private bool _disposed;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            _repositories ??= new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbContext);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<T>)_repositories[type]!;
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken) =>
            await _dbContext.SaveChangesAsync(cancellationToken);

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                    _dbContext.Dispose();
                }
            }
            //dispose unmanaged resources
            _disposed = true;
        }

    }
}
