namespace SAS.ScrapingManagementService.Application.Contracts.Providers
{
    public interface IIdProvider
    {
        Guid GenerateId<T>(string uniqueKey);
        Guid GenerateId<T>(params object[] keyParts);
        Guid GenerateNewId();
    }
}
