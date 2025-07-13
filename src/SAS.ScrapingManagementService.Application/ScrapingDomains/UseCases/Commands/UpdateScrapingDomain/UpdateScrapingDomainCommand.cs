using Ardalis.Result;
using SAS.SharedKernel.CQRS.Commands;

namespace SAS.ScrapingManagementService.Application.ScrapingDomains.UseCases.Commands.UpdateScrapingDomain
{
    public sealed record UpdateScrapingDomainCommand(
        Guid Id,
        string NormalisedName,
        string Name,
        string Description) : ICommand<Result>;

}
