using MediatR;

namespace SAS.ScrapingManagementService.SharedKernel.DomainEvents
{
    public interface IDomainEventHandler<T> : INotificationHandler<T> where T : IDomainEvent
    {
    }
}
