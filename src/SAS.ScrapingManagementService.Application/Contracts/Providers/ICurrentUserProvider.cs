namespace SAS.ScrapingManagementService.Application.Contracts.Providers
{
    public interface ICurrentUserProvider
    {
        Guid UserId { get; }
        string Email { get; }
        IEnumerable<string> Roles { get; }
    }
}
