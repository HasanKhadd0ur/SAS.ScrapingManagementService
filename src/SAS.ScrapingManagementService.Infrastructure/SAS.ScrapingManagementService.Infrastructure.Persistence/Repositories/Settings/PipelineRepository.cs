using Microsoft.EntityFrameworkCore;
using SAS.ScrapingManagementService.Domain.Settings.Entities;
using SAS.ScrapingManagementService.Domain.Settings.Repositories;
using SAS.ScrapingManagementService.Infrastructure.Persistence.AppDataContext;
using SAS.ScrapingManagementService.Infrastructure.Persistence.Repositories.Base;

namespace SAS.ScrapingManagementService.Infrastructure.Persistence.Repositories.DataSources
{
    public class PipelineRepository : BaseRepository<PipelineConfig, Guid>, IPipelineRepository
    {
        private readonly AppDbContext _context;

        public PipelineRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PipelineConfig?> GetByKeyAsync(string pipelineKey, CancellationToken cancellationToken)
        {
            return await _context.Set<PipelineConfig>()
                .Include(p => p.Stages.OrderBy(s => s.Order))
                .FirstOrDefaultAsync(p => p.PipelineKey == pipelineKey, cancellationToken);
        }

        public async Task<List<PipelineConfig>> GetAllWithStagesAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<PipelineConfig>()
                .Include(p => p.Stages.OrderBy(s => s.Order))
                .ToListAsync(cancellationToken);
        }

        public async Task<PipelineConfig?> GetByIdWithStagesAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Set<PipelineConfig>()
                .Include(p => p.Stages.OrderBy(s => s.Order))
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
    }
}
