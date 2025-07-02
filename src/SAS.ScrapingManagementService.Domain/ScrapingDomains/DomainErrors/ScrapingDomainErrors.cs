using SAS.ScrapingManagementService.SharedKernel.DomainErrors;

namespace SAS.ScrapingManagementService.Domain.ScrapingDomains.DomainErrors;

public static class ScrapingDomainErrors
{
   public static readonly DomainError UnExistDomain =
        new("ScrapingDomainError.UnExistDomain", "Domain un exist.");
    public static DomainError AlreadyExists(string name) =>
        new("ScrapingDomainError.AlreadyExists", $"A scraping domain with the name '{name}' already exists.");

}
