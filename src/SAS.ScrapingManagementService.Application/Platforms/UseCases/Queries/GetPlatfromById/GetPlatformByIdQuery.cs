using Ardalis.Result;
using MediatR;
using SAS.ScrapingManagementService.Application.Platforms.Common;
// Query to get a single Platform by Id
public record GetPlatformByIdQuery(Guid Id) : IRequest<Result<PlatformDto>>;
