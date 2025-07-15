using SAS.ScrapingManagementService.Domain.Settings.Entities;
using SAS.SharedKernel.Repositories;

namespace SAS.ScrapingManagementService.Domain.Settings.Repositories
{
    public interface IPipelineRepository : IRepository<PipelineConfig, Guid>
    {
        Task<PipelineConfig?> GetByKeyAsync(string pipelineKey, CancellationToken cancellationToken);
        Task<List<PipelineConfig>> GetAllWithStagesAsync(CancellationToken cancellationToken);
        Task<PipelineConfig?> GetByIdWithStagesAsync(Guid id, CancellationToken cancellationToken);
    }
}
