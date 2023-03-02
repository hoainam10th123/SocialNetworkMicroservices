using Post.Core.Entities;
using Post.Core.Interfaces;
using System;
using System.Collections.Concurrent;


namespace Post.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        protected ConcurrentDictionary<Type, object> repositories;

        public UnitOfWork(DataContext context)
        {
            _context = context;

            if (repositories == null)
                repositories = new ConcurrentDictionary<Type, object>();
        }

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var typeOfEntity = typeof(TEntity);
            if (!repositories.ContainsKey(typeOfEntity))
                repositories[typeOfEntity] = new GenericRepository<TEntity>(_context);

            return (IGenericRepository<TEntity>) repositories[typeOfEntity];
        }
    }
}
