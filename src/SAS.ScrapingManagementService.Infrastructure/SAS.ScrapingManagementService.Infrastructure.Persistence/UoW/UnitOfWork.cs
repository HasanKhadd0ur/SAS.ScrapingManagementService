using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using SAS.ScrapingManagementService.SharedKernel.DomainEvents;
using SAS.ScrapingManagementService.SharedKernel.Entities;
using SAS.ScrapingManagementService.SharedKernel.Utilities;
using SAS.ScrapingManagementService.Infrastructure.Persistence.AppDataContext;

namespace SAS.ScrapingManagementService.Infrastructure.Persistence.UoW

{
    public class UnitOfWork : IUnitOfWork
    {
        #region Dependencies

        private readonly AppDbContext _dbContext;
        private readonly IMediator _mediator;
        private IDbContextTransaction _transaction;

        #endregion Dependencies

        #region Constructor
        public UnitOfWork(AppDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        #endregion Constructor

        #region UOW Operations
        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //await DispatchEventsAsync<Guid>();

            await _dbContext.SaveChangesAsync(cancellationToken);

            try
            {
                _transaction?.Commit();
            }
            catch
            {
                await Rollback();
                // throw;
            }
            finally
            {
                DisposeTransaction();
            }
        }
        public void BeginTransaction()
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
        }
        private void DisposeTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }
        #endregion UOW Operations
        #region Process  Events 
        public async Task DispatchEventsAsync<TId>()
        {
            // list for the processed event 
            var processedDomainEvents = new List<IDomainEvent>();
            // list of un processed events 
            var unprocessedDomainEvents = GetDomainEvents<TId>().AsEnumerable();

            // this is needed incase another DomainEvent is published from a DomainEventHandler
            while (unprocessedDomainEvents.Any())
            {
                // publish domain events 
                await DispatchDomainEventsAsync(unprocessedDomainEvents);

                // move the un processed to the processed 
                processedDomainEvents.AddRange(unprocessedDomainEvents);

                // get the newest un processed events 
                unprocessedDomainEvents = GetDomainEvents<TId>()
                                            .Where(e => !processedDomainEvents.Contains(e))
                                            .ToList();
            }

            // clear the events 
            ClearDomainEvents<TId>();
        }
        #endregion Process Events 

        #region Get Events 
        private List<IDomainEvent> GetDomainEvents<TId>()
        {
            // change tracker to the base entity 
            var aggregateRoots = GetTrackedEntites<TId>();

            //  get the events list 
            return aggregateRoots
                .SelectMany(x => x.Events)
                .ToList();
        }

        #endregion Get Events 

        #region Get Tracked
        private List<BaseEntity<TId>> GetTrackedEntites<TId>()
        {
            // change tracker to the base enties that has an events 
            return _dbContext.ChangeTracker
                .Entries<BaseEntity<TId>>()
                .Where(x => x.Entity.Events != null && x.Entity.Events.Any())
                .Select(e => e.Entity)
                .ToList();
        }
        #endregion Get Tracked 

        #region Dispatch Events 
        private async Task DispatchDomainEventsAsync(IEnumerable<IDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
        }

        #endregion Dispatch Events 

        #region Clear And Dispose 
        private void ClearDomainEvents<TId>()
        {
            var aggregateRoots = GetTrackedEntites<TId>();
            aggregateRoots.ForEach(aggregate => aggregate.ClearDomainEvents());
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        #endregion Clear And Dispose 
    }
}
