using Marketplace.Domain.Events;
using MediatR;

namespace Marketplace.Application.Events
{
    public interface IEventHandler<TEvent> : INotificationHandler<EventNotification<TEvent>> where TEvent : IDomainEvent
    {
    }
}
