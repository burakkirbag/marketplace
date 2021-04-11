using Marketplace.Domain.Events;
using Marketplace.Domain.Rules;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Marketplace.Domain.Entities
{
    [Serializable]
    public abstract class EntityBase : IEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }

        private List<IDomainEvent> _events;

        public IReadOnlyCollection<IDomainEvent> Events => _events?.AsReadOnly();

        public void AddEvent(IDomainEvent @event)
        {
            _events ??= new List<IDomainEvent>();
            _events.Add(@event);
        }

        public void ClearEvents()
        {
            _events?.Clear();
        }

        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken()) throw new BusinessRuleValidationException(rule);
        }
    }
}
