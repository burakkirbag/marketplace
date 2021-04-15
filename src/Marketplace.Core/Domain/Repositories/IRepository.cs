using Marketplace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Marketplace.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IQueryable<TEntity> AsQueryable();

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity GetById(string id);

        Task<TEntity> GetByIdAsync(string id);

        void Insert(TEntity entity);

        Task InsertAsync(TEntity entity);

        void InsertMany(ICollection<TEntity> entities);

        Task InsertManyAsync(ICollection<TEntity> entities);

        void Update(TEntity entity);

        Task UpdateAsync(TEntity entity);

        void InsertOrUpdate(TEntity entity);

        Task InsertOrUpdateAsync(TEntity entity);
        
        void Delete(Expression<Func<TEntity, bool>> predicate);

        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        void DeleteById(string id);

        Task DeleteByIdAsync(string id);

        void DeleteMany(Expression<Func<TEntity, bool>> predicate);

        Task DeleteManyAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
