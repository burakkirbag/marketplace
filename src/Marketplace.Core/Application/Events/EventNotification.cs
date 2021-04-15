using Marketplace.Domain.Events;

namespace Marketplace.Application.Events
{
    public class EventNotification<TEvent> : IEventNotification<TEvent> where TEvent : IDomainEvent
    {
        public TEvent Event { get; }

        public EventNotification(TEvent @event)
        {
            Event = @event;
        }
    }
}
