using Marketplace.Domain.Events;
using MediatR;

namespace Marketplace.Application.Events
{
    public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<DomainEventNotification<TDomainEvent>> where TDomainEvent : IDomainEvent
    {
    }
}
