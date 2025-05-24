namespace SAS.ScrapingManagementService.SharedKernel.CQRS.Commands
{
    public interface ILoggableCommand<out TResponse> : ICommand<TResponse>
    {
    }
}
