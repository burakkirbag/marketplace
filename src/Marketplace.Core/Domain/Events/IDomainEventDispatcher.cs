using System.Threading.Tasks;

namespace Marketplace.Domain.Events
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IDomainEvent @event);
    }
}
