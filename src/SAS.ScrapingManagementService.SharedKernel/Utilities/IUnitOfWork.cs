using System.Threading;
using System.Threading.Tasks;

namespace SAS.ScrapingManagementService.SharedKernel.Utilities
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        void BeginTransaction();
        Task Rollback();
        Task DispatchEventsAsync<TId>();
    }

}
