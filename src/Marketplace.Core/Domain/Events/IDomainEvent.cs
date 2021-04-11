using System;

namespace Marketplace.Domain.Events
{
    public interface IDomainEvent
    {
        DateTime OccuredOn { get; }
    }
}
