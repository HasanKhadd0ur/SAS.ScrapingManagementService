using SAS.ScrapingManagementService.Application.Common;

namespace SAS.ScrapingManagementService.Application.Scrapers.Common
{
    public class ScraperDto : BaseDTO<Guid>
        {
            public String ScraperName { get; set; }
            public string Hostname { get; set; }
            public string IPAddress { get; set; } 
            public DateTime RegisteredAt { get; set; } 
            public bool IsActive { get; set; } 
            public int TasksHandled { get; set; } 
        }

    }
