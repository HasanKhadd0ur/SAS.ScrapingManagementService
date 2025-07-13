using SAS.ScrapingManagementService.Domain.Tasks.Entities;
using SAS.SharedKernel.Repositories;

namespace SAS.ScrapingManagementService.Domain.Tasks.Repositories
{
    public interface IScrapingTaskRepository : IRepository<ScrapingTask, Guid>
    {

    }
}
