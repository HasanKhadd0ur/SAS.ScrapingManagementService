using Ardalis.Result;
using SAS.ScrapingManagementService.Application.ScrapingTasks.Common;
using SAS.SharedKernel.CQRS.Queries;

namespace SAS.ScrapingManagementService.Application.ScrapingTasks.UseCases.Queries.GetScrapingTaskById
{
    public record GetScrapingTaskByIdQuery(Guid Id)
     : IQuery<Result<ScrapingTaskDto>>;
}
