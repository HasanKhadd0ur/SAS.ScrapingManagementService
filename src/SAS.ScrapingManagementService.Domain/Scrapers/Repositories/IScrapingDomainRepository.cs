using SAS.ScrapingManagementService.Domain.Scrapers.Entities;
using SAS.ScrapingManagementService.SharedKernel.Repositories;

namespace SAS.ScrapingManagementService.Domain.Scrapers.Repositories
{
    public interface IScrapingDomainRepository : IRepository<ScrapingDomain, Guid>
    {

    }
}
