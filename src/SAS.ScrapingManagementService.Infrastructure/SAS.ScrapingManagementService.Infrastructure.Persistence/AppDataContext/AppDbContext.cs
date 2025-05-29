
using Microsoft.EntityFrameworkCore;
using SAS.ScrapingManagementService.SharedKernel.DomainEvents;

namespace SAS.ScrapingManagementService.Infrastructure.Persistence.AppDataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {

        }

        // DbSets for entities



        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Ignore<List<IDomainEvent>>();
            base.OnModelCreating(modelBuilder);


            modelBuilder.HasAnnotation("Relational:Collation", "Arabic_CI_AS");

        }

    }
}
