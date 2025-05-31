using Ardalis.Result;
using SAS.ScrapingManagementService.SharedKernel.CQRS.Commands;

namespace SAS.ScrapingManagementService.Application.ScrapingDomains.UseCases.Commands.CreateScrapingDomain
{
    public sealed record CreateScrapingDomainCommand(
        string NormalisedName,
        string Name ,
        string Description) : ICommand<Result<Guid>>;

}