using MediatR;

namespace Marketplace.Application.Commands
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }

    public interface ICommand : IRequest
    {

    }
}
