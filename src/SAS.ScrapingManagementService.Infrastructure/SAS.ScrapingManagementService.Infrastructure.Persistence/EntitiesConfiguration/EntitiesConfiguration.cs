using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAS.ScrapingManagementService.Domain.DataSources.Entities;

namespace SAS.ScrapingManagementService.Infrastructure.Persistence.EntitiesConfiguration
{
    // Event Entity Configuration
    public class EventEntityConfiguration : IEntityTypeConfiguration<DataSource>
    {
        public void Configure(EntityTypeBuilder<DataSource> builder)
        {
            builder.HasKey(e => e.Id);  // Set primary key
        }
    }
}
