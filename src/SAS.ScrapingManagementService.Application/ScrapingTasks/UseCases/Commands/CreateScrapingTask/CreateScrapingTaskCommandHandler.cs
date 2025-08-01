
using Ardalis.Result;
using AutoMapper;
using SAS.ScrapingManagementService.Application.Contracts.Messaging;
using SAS.ScrapingManagementService.Application.DataSources.Common;
using SAS.ScrapingManagementService.Application.DataSourceTypes.Common;
using SAS.ScrapingManagementService.Application.Scrapers.Common;
using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.Domain.ScrapingDomains.Entities;
using SAS.ScrapingManagementService.Domain.Tasks.Entities;
using SAS.SharedKernel.CQRS.Commands;
using SAS.SharedKernel.Repositories;
using SAS.SharedKernel.Specification;

namespace SAS.ScrapingManagementService.Application.ScrapingTasks.UseCases.Commands.CreateScrapingTask
{
    public class CreateScrapingTaskCommandHandler : ICommandHandler<CreateScrapingTaskCommand, Result<Guid>>
    {
        private readonly IRepository<ScrapingDomain,Guid> _domainRepo;
        private readonly IRepository<DataSource, Guid> _dataSourceRepo;
        private readonly IRepository<ScrapingTask, Guid> _taskRepo;
        private readonly IMessageProducerService _producer;
        private readonly IMapper _mapper;

        public CreateScrapingTaskCommandHandler(
            IRepository<ScrapingDomain, Guid> domainRepo,
            IRepository<DataSource, Guid> dataSourceRepo,
            IRepository<ScrapingTask, Guid> taskRepo,
            IMessageProducerService kafka,
            IMapper mapper)
        {
            _domainRepo = domainRepo;
            _dataSourceRepo = dataSourceRepo;
            _taskRepo = taskRepo;
            _producer = kafka;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateScrapingTaskCommand request, CancellationToken cancellationToken)
        {
            var domain = await _domainRepo.GetByIdAsync(request.DomainId);
            var spec = new BaseSpecification<DataSource>();
            spec.AddInclude(e => e.Platform);
            spec.AddInclude(e => e.DataSourceType);



            var dataSources = await _dataSourceRepo.ListAsync();
            dataSources = dataSources.Where(d => request.DataSourceIds.Contains(d.Id));
            var task = new ScrapingTask
            {
                Id = Guid.NewGuid(),
                PublishedAt = DateTime.UtcNow,
                Domain = domain,
                DomainId=domain.Id,
                DataSources = dataSources.ToList()
            };

            await _taskRepo.AddAsync(task);
            var message = new ScrapingTaskMessage
            {
                Id = task.Id,
                Domain = domain.NormalisedName,
                Platform = dataSources.First().Platform.Name,
                DataSources = dataSources.Select(d => new DataSourceDto
                {   PlatformId=d.PlatformId,
                    DomainId=d.DomainId,
                    Name=d.Name,
                    Target = d.Target,
                    DataSourceType=_mapper.Map<DataSourceTypeDto>(d.DataSourceType),
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
