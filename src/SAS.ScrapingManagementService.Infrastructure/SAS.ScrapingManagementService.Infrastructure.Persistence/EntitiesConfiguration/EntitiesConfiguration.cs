using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.Domain.Scrapers.Entities;
using SAS.ScrapingManagementService.Domain.ScrapingDomains.Entities;
using SAS.ScrapingManagementService.Domain.Tasks.Entities;

namespace SAS.ScrapingManagementService.Infrastructure.Persistence.EntitiesConfiguration
{
    public class DataSourceTypeConfiguration : IEntityTypeConfiguration<DataSourceType>
    {
        public void Configure(EntityTypeBuilder<DataSourceType> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }

    public class DataSourceConfiguration : IEntityTypeConfiguration<DataSource>
    {
        public void Configure(EntityTypeBuilder<DataSource> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Target)
                .HasMaxLength(255);

            builder.HasOne(e => e.Platform)
                .WithMany()
                .HasForeignKey(e => e.PlatformId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(e => e.DataSourceType)
                .WithMany()
                .HasForeignKey(e => e.DataSourceTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Domain)
                .WithMany(d => d.DataSources)
                .HasForeignKey(e => e.DomainId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class PlatformConfiguration : IEntityTypeConfiguration<Platform>
    {
        public void Configure(EntityTypeBuilder<Platform> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Description)
                .HasMaxLength(500);
            builder.HasData(
                    new Platform
                    {
                        Id = Guid.Parse("6835f670-2e5c-8000-b5bb-ea9b1705cede"),
                        Name = "Telegram",
                        Description = "Telegram social media platform"
                    },
                    new Platform
                    {
                        Id = Guid.Parse("6835f670-2e5c-8000-b5bb-ea9b1705cedb"),
                        Name = "Twitter",
                        Description = "Twitter/X social media platform"
                    }
                );

        }
    }

    public class ScraperConfiguration : IEntityTypeConfiguration<Scraper>
    {
        public void Configure(EntityTypeBuilder<Scraper> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.ScraperName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Hostname)
                .HasMaxLength(100);

            builder.Property(s => s.IPAddress)
                .HasMaxLength(50);

            builder.Property(s => s.RegisteredAt)
                .IsRequired();

            builder.Property(s => s.IsActive)
                .IsRequired();

            builder.Property(s => s.TasksHandled)
                .IsRequired();
        }
    }
    public class ScrapingDomainConfiguration : IEntityTypeConfiguration<ScrapingDomain>
    {
        public void Configure(EntityTypeBuilder<ScrapingDomain> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.NormalisedName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Description)
                .HasMaxLength(500);

            builder.HasMany(d => d.DataSources)
                .WithOne(ds => ds.Domain)
                .HasForeignKey(e => e.DomainId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
    public class ScrapingTaskConfiguration : IEntityTypeConfiguration<ScrapingTask>
    {
        public void Configure(EntityTypeBuilder<ScrapingTask> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.PublishedAt)
                .IsRequired();

            builder.Property(t => t.CompletedAt)
            .IsRequired(false);

            builder.HasOne(t => t.ScrapingExecutor)
                .WithMany()
                .HasForeignKey(e => e.ScraperId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasOne(t => t.Domain)
                .WithMany()
                .HasForeignKey(e => e.DomainId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.DataSources)
                .WithOne();
        }
    }

}
