using SAS.ScrapingManagementService.Application.Common;

namespace SAS.ScrapingManagementService.Application.Platforms.Common
{
    public class PlatformDto : BaseDTO<Guid>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; }

    }
}