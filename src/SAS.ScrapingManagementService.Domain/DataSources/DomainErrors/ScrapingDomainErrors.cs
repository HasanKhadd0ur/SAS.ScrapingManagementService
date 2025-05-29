using SAS.ScrapingManagementService.SharedKernel.DomainErrors;

namespace SAS.ScrapingManagementService.Domain.DataSources.DomainErrors;

public static class ScrapingDomainErrors
{
   public static readonly DomainError UnExistDomain =
        new("ScrapingDomainError.UnExistDomain", "Domain un exist.");

}
