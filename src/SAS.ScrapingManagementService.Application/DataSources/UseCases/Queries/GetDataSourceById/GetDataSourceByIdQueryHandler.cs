﻿using Ardalis.Result;
using AutoMapper;
using MediatR;
using SAS.ScrapingManagementService.Application.DataSources.Common;
using SAS.ScrapingManagementService.Domain.DataSources.DomainErrors;
using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.SharedKernel.Repositories;

namespace SAS.ScrapingManagementService.Application.DataSources.UseCases.Queries
{
    public class GetDataSourceByIdQueryHandler : IRequestHandler<GetDataSourceByIdQuery, Result<DataSourceDto>>
    {
        private readonly IRepository<DataSource, Guid> _dataSourceRepo;
        private readonly IMapper _mapper;

        public GetDataSourceByIdQueryHandler(
            IRepository<DataSource, Guid> dataSourceRepo,
            IMapper mapper)
        {
            _dataSourceRepo = dataSourceRepo;
            _mapper = mapper;
        }

        public async Task<Result<DataSourceDto>> Handle(GetDataSourceByIdQuery request, CancellationToken cancellationToken)
        {
            var dataSource = await _dataSourceRepo.GetByIdAsync(request.Id);

            if (dataSource is null)
                return Result.Invalid(DataSourceErrors.UnExistDataSource);

            var dto = _mapper.Map<DataSourceDto>(dataSource);

            return Result.Success(dto);
        }
    }
}
