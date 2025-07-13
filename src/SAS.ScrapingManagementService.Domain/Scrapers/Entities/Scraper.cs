
using SAS.SharedKernel.Entities;

namespace SAS.ScrapingManagementService.Domain.Scrapers.Entities
{
    public class Scraper  : BaseEntity<Guid>
    {
        public String ScraperName { get; set; }
        public string Hostname { get; set; } // Useful to identify the host or node

        public string IPAddress { get; set; } // Optional, for monitoring or auditing

        public DateTime RegisteredAt { get; set; } // When it registered with the master
        //public DateTime LastHeartbeat { get; set; } // Last time the agent sent a heartbeat (or pinged the master)

        public bool IsActive { get; set; } // Whether it's currently connected/available

        public int TasksHandled { get; set; } // Number of tasks this agent handled

    }

}
