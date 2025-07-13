using Ardalis.Result;
using MediatR;

namespace SAS.ScrapingManagementService.Application.Scrapers.UseCases.Commands.AssignScrapingExecutor
{
    public record AssignScrapingExecutorCommand(Guid ScrapingTaskId, Guid ScraperId) : IRequest<Result>;
}
