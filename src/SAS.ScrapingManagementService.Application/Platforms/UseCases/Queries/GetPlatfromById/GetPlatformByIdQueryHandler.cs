using Ardalis.Result;
using AutoMapper;
using MediatR;
using SAS.ScrapingManagementService.Application.Platforms.Common;
using SAS.ScrapingManagementService.Domain.DataSources.DomainErrors;
using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.SharedKernel.Repositories;

namespace SAS.ScrapingManagementService.Application.Platforms.UseCases.Queries.GetPlatfromById
{
    public  class GetPlatformByIdQueryHandler : IRequestHandler<GetPlatformByIdQuery, Result<PlatformDto>>
    {
        private readonly IRepository<Platform, Guid> _platformRepo;
        private readonly IMapper _mapper;

        public GetPlatformByIdQueryHandler(
            IRepository<Platform, Guid> platformRepo,
            IMapper mapper)
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
        }

        public async Task<Result<PlatformDto>> Handle(GetPlatformByIdQuery request, CancellationToken cancellationToken)
        {
            var platform = await _platformRepo.GetByIdAsync(request.Id);

            if (platform is null)
                return Result.Invalid(PlatformErrors.UnExistPlatform);

            var dto = _mapper.Map<PlatformDto>(platform);

            return Result.Success(dto);
        }
    }
}