
using Microsoft.EntityFrameworkCore;
using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.Domain.DataSourceType.Entities;
using SAS.ScrapingManagementService.Domain.Platforms.Entities;
using SAS.ScrapingManagementService.Domain.Scrapers.Entities;
using SAS.ScrapingManagementService.Domain.ScrapingDomains.Entities;
using SAS.ScrapingManagementService.Domain.Settings.Entities;
using SAS.ScrapingManagementService.Domain.Tasks.Entities;
using SAS.SharedKernel.DomainEvents;

namespace SAS.ScrapingManagementService.Infrastructure.Persistence.AppDataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {

        }

        // DbSets for entities
        public DbSet<DataSource> DataSources { get; set; }
        public DbSet<ScrapingDomain> ScrapingDomains { get; set; }
        public DbSet<ScrapingTask> ScrapingTasks { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Scraper> Scrapers { get; set; }
        public DbSet<DataSourceType> DataSourceTypes { get; set; }

        public DbSet<PipelineStage> PipelineStages { get; set; }

        public DbSet<PipelineConfig> PipelineConfigs { get; set; }






        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Ignore<List<IDomainEvent>>();
            base.OnModelCreating(modelBuilder);


            modelBuilder.HasAnnotation("Relational:Collation", "Arabic_CI_AS");

        }

    }
}
