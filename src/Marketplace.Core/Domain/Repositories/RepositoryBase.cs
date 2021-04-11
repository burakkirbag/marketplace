using Marketplace.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Domain.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        public abstract Task CreateAsync(TEntity entity);
        public abstract Task UpdateAsync(TEntity entity);
        public abstract Task DeleteAsync(string id);
        public abstract Task<TEntity> GetByIdAsync(string id);
        public abstract Task<IEnumerable<TEntity>> GetListAsync();
        public abstract Task<long> CountAsync();
        public abstract Task BulkCreateAsync(List<TEntity> entities);


        public virtual void Disposable()
        {

        }

        public void Dispose()
        {
            Disposable();
        }
    }
}
