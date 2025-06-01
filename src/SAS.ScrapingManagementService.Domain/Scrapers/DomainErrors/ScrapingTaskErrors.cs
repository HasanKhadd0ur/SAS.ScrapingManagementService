using SAS.ScrapingManagementService.SharedKernel.DomainErrors;

namespace SAS.ScrapingManagementService.Domain.Scrapers.DomainErrors;

public static class ScrapingTaskErrors
{
    public static readonly DomainError UnExistTask =
         new("ScrapingTaskError.UnExistDomain", "Task un exist.");

    public static readonly DomainError TaskAlreadyCompleted =
         new("ScrapingTask.Error.TaskAlreadyCompleted", "Scraping task is already completed.");

}
