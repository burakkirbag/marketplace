using System;
using Marketplace.Application.Dto;
using Marketplace.Domain.Entities;

namespace Marketplace.Mapper
{
    public static class MappingExtensions
    {
        public static TDestination Map<TDestination>(this object source)
        {
            return AutoMapperConfiguration.Mapper.Map<TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return AutoMapperConfiguration.Mapper.Map(source, destination);
        }

        public static TModel ToModel<TModel>(this IEntity entity) where TModel : EntityDto
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return entity.Map<TModel>();
        }

        public static TModel ToModel<TEntity, TModel>(this TEntity entity, TModel model)
            where TEntity : IEntity where TModel : EntityDto
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return entity.MapTo(model);
        }

        public static TEntity ToEntity<TEntity>(this EntityDto model) where TEntity : IEntity
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return model.Map<TEntity>();
        }

        public static TEntity ToEntity<TEntity, TModel>(this TModel model, TEntity entity)
            where TEntity : IEntity where TModel : EntityDto
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return model.MapTo(entity);
        }
    }
}