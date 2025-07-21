using Ardalis.Result;
using SAS.ScrapingManagementService.Application.ScrapingTasks.Common;
using SAS.SharedKernel.CQRS.Queries;

namespace SAS.ScrapingManagementService.Application.ScrapingTasks.UseCases.Queries.GetAllScrapingTasks
{
    public record GetAllScrapingTasksQuery(int? PageNumber, int? PageSize)
           : IQuery<Result<IEnumerable<ScrapingTaskDto>>>;
}
