using SAS.SharedKernel.DomainErrors;

namespace SAS.ScrapingManagementService.Domain.Scrapers.DomainErrors;

public static class ScraperErrors
{
    public static readonly DomainError UnExistScraper =
         new("ScraperError.UnExistDomain", "Task un exist.");

}
