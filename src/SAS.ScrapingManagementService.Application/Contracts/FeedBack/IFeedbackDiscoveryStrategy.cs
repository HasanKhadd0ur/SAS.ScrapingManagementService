using SAS.ScrapingManagementService.Domain.DataSources.Entities;

namespace SAS.ScrapingManagementService.Application.Contracts.FeedBack
{
    public interface IFeedbackDiscoveryStrategy
    {
        bool SupportsSource(string origin);
        Task<List<DataSource>> DiscoverAsync(DiscoveryFeedbackDto feedback, CancellationToken cancellationToken);
    }


}
