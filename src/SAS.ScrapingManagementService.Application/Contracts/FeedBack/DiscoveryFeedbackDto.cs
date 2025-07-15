using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAS.ScrapingManagementService.Application.Contracts.FeedBack
{
    public class DiscoveryFeedbackDto
    {
        public string Origin { get; set; } = default!; // e.g., "Agent", "Newsletter"
        public string Domain { get; set; } = default!;
        public DateTime Timestamp { get; set; }
        public List<NamedEntityStatDto> Entities { get; set; } = new();
    }


}
