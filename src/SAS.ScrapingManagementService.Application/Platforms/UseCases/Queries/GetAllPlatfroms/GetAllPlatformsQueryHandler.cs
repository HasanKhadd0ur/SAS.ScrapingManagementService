using Ardalis.Result;
using AutoMapper;
using MediatR;
using SAS.ScrapingManagementService.Application.Platforms.Common;
using SAS.ScrapingManagementService.Application.Platforms.UseCases.Queries.GetAllPlatfroms;
using SAS.ScrapingManagementService.Domain.Platforms.Entities;
using SAS.SharedKernel.Repositories;
using SAS.SharedKernel.Specification;

public  class GetAllPlatformsQueryHandler : IRequestHandler<GetAllPlatformsQuery, Result<IEnumerable<PlatformDto>>>
{
    private readonly IRepository<Platform, Guid> _platformRepo;
    private readonly IMapper _mapper;

    public GetAllPlatformsQueryHandler(IRepository<Platform, Guid> platformRepo, IMapper mapper)
    {
        _platformRepo = platformRepo;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<PlatformDto>>> Handle(GetAllPlatformsQuery request, CancellationToken cancellationToken)
    {
        var spec = new BaseSpecification<Platform>();
        spec.ApplyOptionalPagination(request.PageSize, request.PageNumber);

        var entities = await _platformRepo.ListAsync(spec);

        var dtoList = _mapper.Map<IEnumerable<PlatformDto>>(entities);

        return Result.Success(dtoList);
    }
}
