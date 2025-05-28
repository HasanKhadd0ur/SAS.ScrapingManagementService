namespace SAS.ScrapingManagementService.Application.Contracts.Providers
{
    public interface IDateTimeProvider
    {
        public DateTime UtcNow { get; }
    }

}
