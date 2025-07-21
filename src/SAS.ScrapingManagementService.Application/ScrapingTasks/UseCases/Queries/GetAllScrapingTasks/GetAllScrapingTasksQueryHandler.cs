using Ardalis.Result;
using AutoMapper;
using MediatR;
using SAS.ScrapingManagementService.Application.ScrapingTasks.Common;
using SAS.ScrapingManagementService.Domain.Tasks.Entities;
using SAS.SharedKernel.CQRS.Queries;
using SAS.SharedKernel.Repositories;
using SAS.SharedKernel.Specification;

namespace SAS.ScrapingManagementService.Application.ScrapingTasks.UseCases.Queries.GetAllScrapingTasks
{
    public class GetAllScrapingTasksQueryHandler
       : IQueryHandler<GetAllScrapingTasksQuery, Result<IEnumerable<ScrapingTaskDto>>>
    {
        private readonly IRepository<ScrapingTask, Guid> _taskRepo;
        private readonly IMapper _mapper;

        public GetAllScrapingTasksQueryHandler(
            IRepository<ScrapingTask, Guid> taskRepo,
            IMapper mapper)
        {
            _taskRepo = taskRepo;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ScrapingTaskDto>>> Handle(
            GetAllScrapingTasksQuery request,
            CancellationToken cancellationToken)
        {
            var spec = new BaseSpecification<ScrapingTask>();
            spec.AddInclude(e => e.ScrapingExecutor);
            spec.AddInclude(e => e.Domain);


            spec.ApplyOptionalPagination(request.PageSize, request.PageNumber);

            var tasks = await _taskRepo.ListAsync(spec);
            var dtoList = _mapper.Map<IEnumerable<ScrapingTaskDto>>(tasks);

            return Result.Success(dtoList);
        }
    }
}
