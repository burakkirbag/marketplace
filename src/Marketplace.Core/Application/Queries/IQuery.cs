using MediatR;

namespace Marketplace.Application.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
