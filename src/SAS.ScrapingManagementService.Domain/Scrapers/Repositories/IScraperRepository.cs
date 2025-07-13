using SAS.ScrapingManagementService.Domain.Scrapers.Entities;
using SAS.SharedKernel.Repositories;

namespace SAS.ScrapingManagementService.Domain.Scrapers.Repositories
{
    public interface IScraperRepository : IRepository<Scraper, Guid>
    {

    }
}
