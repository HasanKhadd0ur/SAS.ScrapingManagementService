
using Ardalis.Result;
using SAS.ScrapingManagementService.Application.Contracts.Messaging;
using SAS.ScrapingManagementService.Application.DataSources.Common;
using SAS.ScrapingManagementService.Application.Scrapers.Common;
using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.Domain.Scrapers.Entities;
using SAS.ScrapingManagementService.SharedKernel.CQRS.Commands;
using SAS.ScrapingManagementService.SharedKernel.Repositories;

namespace SAS.ScrapingManagementService.Application.Scrapers.UseCases.Commands.CreateScrapingTask
{
    public class CreateScrapingTaskCommandHandler : ICommandHandler<CreateScrapingTaskCommand, Result<Guid>>
    {
        private readonly IRepository<ScrapingDomain,Guid> _domainRepo;
        private readonly IRepository<DataSource, Guid> _dataSourceRepo;
        private readonly IRepository<ScrapingTask, Guid> _taskRepo;
        private readonly IMessageProducerService _producer;

        public CreateScrapingTaskCommandHandler(
            IRepository<ScrapingDomain, Guid> domainRepo,
            IRepository<DataSource, Guid> dataSourceRepo,
            IRepository<ScrapingTask, Guid> taskRepo,
            IMessageProducerService kafka)
        {
            _domainRepo = domainRepo;
            _dataSourceRepo = dataSourceRepo;
            _taskRepo = taskRepo;
            _producer = kafka;
        }

        public async Task<Result<Guid>> Handle(CreateScrapingTaskCommand request, CancellationToken cancellationToken)
        {
            var domain = await _domainRepo.GetByIdAsync(request.DomainId);
            var dataSources = await _dataSourceRepo.ListAsync();
            dataSources = dataSources.Where(d => request.DataSourceIds.Contains(d.Id));
            var task = new ScrapingTask
            {
                Id = Guid.NewGuid(),
                PublishedAt = DateTime.UtcNow,
                Domain = domain,
                DataSources = dataSources.ToList()
            };

            await _taskRepo.AddAsync(task);
            var message = new ScrapingTaskMessage
            {
                Id = task.Id,
                Domain = domain.Name,
                Platform = dataSources.First().Platform.Name,
                DataSources = dataSources.Select(d => new DataSourceDto
                {
                    Target = d.Traget,
                    Limit = 1
                }).ToList(),
                Limit = 5,
                ScrapingApproach =request.ScrapingApproach
            };

            await _producer.ProduceAsync("scraping-tasks", message);

            return task.Id;
        }
    }
}