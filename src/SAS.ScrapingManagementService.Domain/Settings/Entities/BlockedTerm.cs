using SAS.SharedKernel.Entities;

namespace SAS.ScrapingManagementService.Domain.Settings.Entities
{
    public class BlockedTerm : BaseEntity<Guid>
    {
        public string Term { get; set; } = default!;
    }
}
