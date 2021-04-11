using Marketplace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Domain.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : IEntity
    {
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(string id);
        Task<TEntity> GetByIdAsync(string id);
        Task<IEnumerable<TEntity>> GetListAsync();
        Task<long> CountAsync();
        Task BulkCreateAsync(List<TEntity> entities);
    }
}
