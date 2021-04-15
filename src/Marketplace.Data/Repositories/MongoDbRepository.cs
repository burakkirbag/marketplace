using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Marketplace.Domain.Entities;
using Marketplace.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Marketplace.Data.Repositories
{
    public class MongoDbRepository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        protected readonly IMongoDbContext _dbContext;
        protected IMongoCollection<TEntity> _dbCollection;

        public MongoDbRepository(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbCollection = _dbContext.GetCollection<TEntity>();
        }

        public virtual IQueryable<TEntity> AsQueryable()
        {
            return _dbCollection.AsQueryable();
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbCollection.Find(predicate).FirstOrDefault();
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.Run(() => _dbCollection.Find(predicate).FirstOrDefaultAsync());
        }

        public virtual TEntity GetById(string id)
        {
            return _dbCollection.Find(x => x.Id == id).FirstOrDefault();
        }

        public virtual Task<TEntity> GetByIdAsync(string id)
        {
            return Task.Run(() => { return _dbCollection.Find(x => x.Id == id).FirstOrDefaultAsync(); });
        }

        public virtual void Insert(TEntity entity)
        {
            DispatchEvents(entity);

            _dbCollection.InsertOne(entity);
        }

        public virtual Task InsertAsync(TEntity entity)
        {
            DispatchEvents(entity);

            return Task.Run(() => _dbCollection.InsertOneAsync(entity));
        }

        public void InsertMany(ICollection<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                DispatchEvents(entity).GetAwaiter();
            }

            _dbCollection.InsertMany(entities);
        }

        public virtual Task InsertManyAsync(ICollection<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                DispatchEvents(entity);
            }

            return Task.Run(() => _dbCollection.InsertManyAsync(entities));
        }

        public void Update(TEntity entity)
        {
            DispatchEvents(entity);

            _dbCollection.FindOneAndReplace(x => x.Id == entity.Id, entity);
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            DispatchEvents(entity);

            return Task.Run(() => _dbCollection.FindOneAndReplaceAsync(x => x.Id == entity.Id, entity));
        }

        public void InsertOrUpdate(TEntity entity)
        {
            DispatchEvents(entity);

            if (!string.IsNullOrEmpty(entity.Id) && entity.Id != Guid.Empty.ToString())
                _dbCollection.FindOneAndReplace(x => x.Id == entity.Id, entity);
            else
                _dbCollection.InsertOne(entity);
        }

        public virtual async Task InsertOrUpdateAsync(TEntity entity)
        {
            DispatchEvents(entity);

            if (!string.IsNullOrEmpty(entity.Id) && entity.Id != Guid.Empty.ToString())
                await _dbCollection.FindOneAndReplaceAsync(x => x.Id == entity.Id, entity);
            else
                await _dbCollection.InsertOneAsync(entity);
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            _dbCollection.FindOneAndDelete(predicate);
        }

        public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.Run(() => _dbCollection.FindOneAndDeleteAsync(predicate));
        }

        public void DeleteById(string id)
        {
            _dbCollection.FindOneAndDelete(x => x.Id == id);
        }

        public Task DeleteByIdAsync(string id)
        {
            return Task.Run(() => { _dbCollection.FindOneAndDeleteAsync(x => x.Id == id); });
        }

        public void DeleteMany(Expression<Func<TEntity, bool>> predicate)
        {
            _dbCollection.DeleteMany(predicate);
        }

        public Task DeleteManyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.Run(() => _dbCollection.DeleteManyAsync(predicate));
        }

        protected virtual Task DispatchEvents(IEntity entity)
        {
            return Task.Run(() =>
            {
                var events = entity.GetEvents();
                foreach (var @event in events)
                {
                    _dbContext.EventDispatcher.Dispatch(@event);
                }
            });
        }
    }
}