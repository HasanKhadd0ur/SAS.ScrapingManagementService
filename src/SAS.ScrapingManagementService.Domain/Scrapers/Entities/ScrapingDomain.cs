﻿using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.SharedKernel.Entities;

namespace SAS.ScrapingManagementService.Domain.Scrapers.Entities
{
    public class ScrapingDomain :BaseEntity<Guid> 
    {
        public string NormalisedName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<DataSource> DataSources { get; set; }
    }

}
