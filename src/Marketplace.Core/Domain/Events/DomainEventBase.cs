using System;

namespace Marketplace.Domain.Events
{
    public class DomainEventBase : IDomainEvent
    {
        public DateTime OccuredOn { get; set; }
    }
}
