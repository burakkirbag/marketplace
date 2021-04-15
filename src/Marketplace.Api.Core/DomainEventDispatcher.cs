using System;
using System.Threading.Tasks;
using Marketplace.Application.Events;
using Marketplace.Domain.Events;
using MediatR;

namespace Marketplace.Api
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;

        public DomainEventDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task Dispatch(IDomainEvent @event)
        {
            var domainEventNotification = CreateDomainEventNotification(@event);
            
            return Task.Run(() => _mediator.Publish(domainEventNotification));
        }

        private INotification CreateDomainEventNotification(IDomainEvent domainEvent)
        {
            var genericDispatcherType = typeof(EventNotification<>).MakeGenericType(domainEvent.GetType());
            return (INotification) Activator.CreateInstance(genericDispatcherType, domainEvent);
        }
    }
}