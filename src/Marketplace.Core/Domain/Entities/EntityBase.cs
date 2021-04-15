using Marketplace.Domain.Events;
using Marketplace.Domain.Rules;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Marketplace.Domain.Entities
{
    [Serializable]
    public abstract class EntityBase : IEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }

        public override string ToString() => $"[{GetType().Name} {Id}]";

        private List<IDomainEvent> _events = new List<IDomainEvent>();

        public IReadOnlyCollection<IDomainEvent> Events => _events?.AsReadOnly();

        public bool HasEvents() => _events.Any();

        public IEnumerable<IDomainEvent> GetEvents() => _events;

        public void ClearEvents() => _events.Clear();

        protected void AddEvent(IDomainEvent @event)
        {
            _events ??= new List<IDomainEvent>();
            _events.Add(@event);
        }

        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
                throw new BusinessRuleValidationException(rule);
        }
    }
}