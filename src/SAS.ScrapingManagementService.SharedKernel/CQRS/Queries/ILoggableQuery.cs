namespace SAS.ScrapingManagementService.SharedKernel.CQRS.Queries
{
    public interface ILoggableQuery<out TResponse> : IQuery<TResponse>
    {
    }

}
