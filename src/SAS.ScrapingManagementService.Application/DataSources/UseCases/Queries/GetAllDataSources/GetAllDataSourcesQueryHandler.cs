using Ardalis.Result;
using AutoMapper;
using MediatR;
using SAS.ScrapingManagementService.Application.Common;
using SAS.ScrapingManagementService.Application.DataSources.Common;
using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.SharedKernel.Repositories;
using SAS.ScrapingManagementService.SharedKernel.Specification;

namespace SAS.ScrapingManagementService.Application.DataSources.UseCases.Queries.GetAllDataSources
{
    public class GetAllDataSourcesQueryHandler : IRequestHandler<GetAllDataSourcesQuery, Result<IEnumerable<DataSourceDto>>>
    {
        private readonly IRepository<DataSource, Guid> _dataSourceRepo;
        private readonly IMapper _mapper;

        public GetAllDataSourcesQueryHandler(IRepository<DataSource, Guid> dataSourceRepo, IMapper mapper)
        {
            _dataSourceRepo = dataSourceRepo;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<DataSourceDto>>> Handle(GetAllDataSourcesQuery request, CancellationToken cancellationToken)
        {
            
            // Build spec inline
            var spec = new BaseSpecification<DataSource>();
            spec.AddInclude(ds => ds.Platform);
            spec.AddInclude(ds => ds.Domain);
            spec.AddInclude(ds => ds.DataSourceType);
            spec.ApplyOptionalPagination(request.PageSize, request.PageNumber);

            var entities = await _dataSourceRepo.ListAsync(spec);

            var dtoList = _mapper.Map<IEnumerable<DataSourceDto>>(entities);


            return Result.Success(dtoList);
        }
    }
}
