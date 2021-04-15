using Marketplace.Domain.Events;
using MediatR;

namespace Marketplace.Application.Events
{
    public interface IEventNotification<TEvent> : INotification where TEvent : IDomainEvent
    {
        public TEvent Event { get; }
    }
}
