using Post.Core.Entities;

namespace Post.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<bool> Complete();
        bool HasChanges();
    }
}
