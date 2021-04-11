using Marketplace.Domain.Events;
using MediatR;

namespace Marketplace.Application.Events
{
    public interface IDomainEventNotification<TDomainEvent> : INotification where TDomainEvent : IDomainEvent
    {
        public TDomainEvent Event { get; }
    }
}
