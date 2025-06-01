using Ardalis.Result;
using MediatR;

namespace SAS.ScrapingManagementService.Application.Scrapers.UseCases.Commands.ConnectScraper
{
    public record ConnectScraperCommand(string ScraperName, string Hostname, string IPAddress) : IRequest<Result<Guid>>;
}
