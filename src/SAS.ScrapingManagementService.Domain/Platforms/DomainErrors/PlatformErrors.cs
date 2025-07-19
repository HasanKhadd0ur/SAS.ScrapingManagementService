using SAS.SharedKernel.DomainErrors;

namespace SAS.ScrapingManagementService.Domain.Platforms.DomainErrors;

public static class PlatformErrors
{
    public static readonly DomainError UnExistPlatform =
         new("PlatformError.UnExistPlatform", "Platform un exist.");

}
