using Ardalis.Result;
using AutoMapper;
using MediatR;
using SAS.ScrapingManagementService.Application.DataSourceTypes.Common;
using SAS.ScrapingManagementService.Domain.DataSourceTypes.Entities;
using SAS.SharedKernel.Repositories;
using SAS.SharedKernel.Specification;

namespace SAS.ScrapingManagementService.Application.DataSourceTypes.UseCases.Queries.GetAllDataSourceTypes
{
    public class GetAllDataSourceTypesQueryHandler : IRequestHandler<GetAllDataSourceTypesQuery, Result<IEnumerable<DataSourceTypeDto>>>
    {
        private readonly IRepository<DataSourceType, Guid> _typeRepo;
        private readonly IMapper _mapper;

        public GetAllDataSourceTypesQueryHandler(IRepository<DataSourceType, Guid> typeRepo, IMapper mapper)
        {
            _typeRepo = typeRepo;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<DataSourceTypeDto>>> Handle(GetAllDataSourceTypesQuery request, CancellationToken cancellationToken)
        {
            var spec = new BaseSpecification<DataSourceType>();
            spec.ApplyOptionalPagination(request.PageSize, request.PageNumber);

            var types = await _typeRepo.ListAsync(spec);
            var dtoList = _mapper.Map<IEnumerable<DataSourceTypeDto>>(types);

            return Result.Success(dtoList);
        }
    }
}
