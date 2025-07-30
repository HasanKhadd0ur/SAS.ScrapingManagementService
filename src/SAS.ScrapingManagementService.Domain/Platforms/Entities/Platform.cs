using SAS.SharedKernel.Entities;

namespace SAS.ScrapingManagementService.Domain.Platforms.Entities
{
    public class Platform : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

}
