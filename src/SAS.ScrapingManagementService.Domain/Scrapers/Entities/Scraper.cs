
using SAS.ScrapingManagementService.SharedKernel.Entities;

namespace SAS.ScrapingManagementService.Domain.Scrapers.Entities
{
    public class Scraper  : BaseEntity<Guid>
    {
        public String ScraperName { get; set; }
        //public DateTime ScraperName { get; set; }


    }

}
