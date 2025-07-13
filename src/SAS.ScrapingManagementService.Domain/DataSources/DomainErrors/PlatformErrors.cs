using SAS.SharedKernel.DomainErrors;

namespace SAS.ScrapingManagementService.Domain.DataSources.DomainErrors;

public static class PlatformErrors
{
    public static readonly DomainError UnExistPlatform =
         new("PlatformError.UnExistPlatform", "Platform un exist.");

}
