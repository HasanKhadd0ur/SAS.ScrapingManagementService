using Ardalis.Result;
using MediatR;

namespace SAS.ScrapingManagementService.Application.ScrapingTasks.UseCases.Commands.CompleteScrapingTask
{
    public record CompleteScrapingTaskCommand(Guid ScrapingTaskId) : IRequest<Result>;
}
