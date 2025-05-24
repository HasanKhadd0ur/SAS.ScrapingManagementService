using MediatR;

namespace SAS.ScrapingManagementService.SharedKernel.CQRS.Commands
{
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
                         where TCommand : ICommand<TResponse>
    {
    }
}
