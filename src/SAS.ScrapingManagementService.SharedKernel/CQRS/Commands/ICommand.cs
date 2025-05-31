using MediatR;

namespace SAS.ScrapingManagementService.SharedKernel.CQRS.Commands
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
