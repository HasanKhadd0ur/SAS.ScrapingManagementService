using Ardalis.Result;
using MediatR;
using SAS.ScrapingManagementService.Domain.Scrapers.DomainErrors;
using SAS.ScrapingManagementService.Domain.Scrapers.Entities;
using SAS.ScrapingManagementService.Domain.Tasks.DomainErrors;
using SAS.ScrapingManagementService.Domain.Tasks.Entities;
using SAS.SharedKernel.Repositories;

namespace SAS.ScrapingManagementService.Application.Scrapers.UseCases.Commands.AssignScrapingExecutor
{
    public class AssignScrapingExecutorCommandHandler : IRequestHandler<AssignScrapingExecutorCommand, Result>
    {
        private readonly IRepository<ScrapingTask, Guid> _taskRepository;
        private readonly IRepository<Scraper, Guid> _scraperRepository;

        public AssignScrapingExecutorCommandHandler(
            IRepository<ScrapingTask, Guid> taskRepository,
            IRepository<Scraper, Guid> scraperRepository)
        {
            _taskRepository = taskRepository;
            _scraperRepository = scraperRepository;
        }

        public async Task<Result> Handle(AssignScrapingExecutorCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.ScrapingTaskId);
            if (task == null)
                return Result.Invalid(ScrapingTaskErrors.UnExistTask);

            var scraper = await _scraperRepository.GetByIdAsync(request.ScraperId);
            if (scraper == null)
                return Result.Invalid(ScraperErrors.UnExistScraper);

            task.ScraperId = scraper.Id;
            task.ScrapingExecutor = scraper;

            await _taskRepository.UpdateAsync(task);
            
            return Result.Success();
        }
    }

}
