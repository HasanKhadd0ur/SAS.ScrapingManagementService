using Ardalis.Result;
using SAS.ScrapingManagementService.Application.Scrapers.Common;
using SAS.ScrapingManagementService.SharedKernel.CQRS.Commands;

namespace SAS.ScrapingManagementService.Application.Scrapers.UseCases.Commands.CreateScrapingTask
{
    public class CreateScrapingTaskCommand : ICommand<Result<Guid>>
    {
        public Guid DomainId { get; set; }
        public List<Guid> DataSourceIds { get; set; }
        public ScrapingApproachDto ScrapingApproach { get; set; }
    }
}