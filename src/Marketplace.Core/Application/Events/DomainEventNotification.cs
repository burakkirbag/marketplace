using Marketplace.Domain.Events;

namespace Marketplace.Application.Events
{
    public class DomainEventNotification<TDomainEvent> : IDomainEventNotification<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        public TDomainEvent Event { get; }

        public DomainEventNotification(TDomainEvent @event)
        {
            Event = @event;
        }
    }
}
