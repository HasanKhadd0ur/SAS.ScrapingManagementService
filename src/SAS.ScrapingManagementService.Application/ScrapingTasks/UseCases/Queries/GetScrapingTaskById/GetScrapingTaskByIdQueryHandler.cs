using Ardalis.Result;
using AutoMapper;
using SAS.ScrapingManagementService.Application.ScrapingTasks.Common;
using SAS.ScrapingManagementService.Domain.Tasks.DomainErrors;
using SAS.ScrapingManagementService.Domain.Tasks.Entities;
using SAS.SharedKernel.CQRS.Queries;
using SAS.SharedKernel.Repositories;
using SAS.SharedKernel.Specification;

namespace SAS.ScrapingManagementService.Application.ScrapingTasks.UseCases.Queries.GetScrapingTaskById
{
    public class GetScrapingTaskByIdQueryHandler
        : IQueryHandler<GetScrapingTaskByIdQuery, Result<ScrapingTaskDto>>
    {
        private readonly IRepository<ScrapingTask, Guid> _taskRepo;
        private readonly IMapper _mapper;

        public GetScrapingTaskByIdQueryHandler(
            IRepository<ScrapingTask, Guid> taskRepo,
            IMapper mapper)
        {
            _taskRepo = taskRepo;
            _mapper = mapper;
        }

        public async Task<Result<ScrapingTaskDto>> Handle(
            GetScrapingTaskByIdQuery request,
            CancellationToken cancellationToken)
        {
            var spec = new BaseSpecification<ScrapingTask>();
            spec.AddInclude(t => t.ScrapingExecutor);
            spec.AddInclude(t => t.Domain);
            spec.AddInclude(t => t.DataSources);

            var task = await _taskRepo.GetByIdAsync(request.Id,spec);

            if (task is null)
            {
                return Result.Invalid(ScrapingTaskErrors.UnExistTask);
            }

            var dto = _mapper.Map<ScrapingTaskDto>(task);
            return Result.Success(dto);
        }
    }
}
