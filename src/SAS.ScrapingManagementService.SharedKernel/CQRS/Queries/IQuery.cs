using MediatR;

namespace SAS.ScrapingManagementService.SharedKernel.CQRS.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }

}
