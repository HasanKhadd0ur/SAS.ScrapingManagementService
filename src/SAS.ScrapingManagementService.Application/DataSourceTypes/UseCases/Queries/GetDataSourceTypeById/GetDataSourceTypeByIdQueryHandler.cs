using Ardalis.Result;
using AutoMapper;
using MediatR;
using SAS.ScrapingManagementService.Application.DataSourceTypes.Common;
using SAS.ScrapingManagementService.Domain.DataSources.DomainErrors;
using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.SharedKernel.Repositories;

namespace SAS.ScrapingManagementService.Application.DataSourceTypes.UseCases.Queries.GetDataSourceTypeById
{
    public class GetDataSourceTypeByIdQueryHandler : IRequestHandler<GetDataSourceTypeByIdQuery, Result<DataSourceTypeDto>>
    {
        private readonly IRepository<DataSourceType, Guid> _typeRepo;
        private readonly IMapper _mapper;

        public GetDataSourceTypeByIdQueryHandler(IRepository<DataSourceType, Guid> typeRepo, IMapper mapper)
        {
            _typeRepo = typeRepo;
            _mapper = mapper;
        }

        public async Task<Result<DataSourceTypeDto>> Handle(GetDataSourceTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var typeEntity = await _typeRepo.GetByIdAsync(request.Id);

            if (typeEntity is null)
                return Result.Invalid(DataSourceTypeErrors.UnExistType);

            var dto = _mapper.Map<DataSourceTypeDto>(typeEntity);

            return Result.Success(dto);
        }

    }
}