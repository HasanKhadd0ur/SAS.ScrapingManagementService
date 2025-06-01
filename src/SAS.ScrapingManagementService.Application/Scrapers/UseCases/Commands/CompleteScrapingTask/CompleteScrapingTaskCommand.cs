using Ardalis.Result;
using MediatR;

namespace SAS.ScrapingManagementService.Application.Scrapers.UseCases.Commands.CompleteScrapingTask
{
    public record CompleteScrapingTaskCommand(Guid ScrapingTaskId) : IRequest<Result>;
}
