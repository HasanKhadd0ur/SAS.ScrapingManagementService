using Ardalis.Result;
using SAS.ScrapingManagementService.Application.Scrapers.Common;
using SAS.SharedKernel.CQRS.Commands;

namespace SAS.ScrapingManagementService.Application.ScrapingTasks.UseCases.Commands.CreateScrapingTask
{
    public class CreateScrapingTaskCommand : ICommand<Result<Guid>>
    {
        public Guid DomainId { get; set; }
        public List<Guid> DataSourceIds { get; set; }
        public ScrapingApproachDto ScrapingApproach { get; set; }
    }

}
