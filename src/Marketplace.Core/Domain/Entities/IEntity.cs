using Marketplace.Domain.Events;
using System.Collections.Generic;

namespace Marketplace.Domain.Entities
{
    public interface IEntity
    {
        public string Id { get; set; }

        public IReadOnlyCollection<IDomainEvent> Events { get; }

        public void AddEvent(IDomainEvent @event);

        public void ClearEvents();
    }
}
