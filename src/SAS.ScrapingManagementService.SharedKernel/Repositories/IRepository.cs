using SAS.ScrapingManagementService.SharedKernel.Entities;
using SAS.ScrapingManagementService.SharedKernel.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SAS.ScrapingManagementService.SharedKernel.Repositories
{
    /// <summary>
    /// IRepository provides a generic interface for accessing and manipulating 
    /// entities in the data source. It defines the standard CRUD operations and 
    /// supports working with specifications to query data more flexibly.
    /// </summary>
    /// <typeparam name="T">The type of entity that the repository will manage, 
    /// which must inherit from BaseEntity.</typeparam>
    public interface IRepository<T,TId> where T : BaseEntity<TId>
    {
        /// <summary>
        /// Asynchronously retrieves an entity by its unique identifier. 
        /// Optionally, a specification can be applied to modify the query behavior.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to retrieve.</param>
        /// <param name="specification">An optional specification to apply to the query.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found, or null otherwise.</returns>
        Task<T> GetByIdAsync(TId id, ISpecification<T> specification = null);

        /// <summary>
        /// Asynchronously retrieves a list of entities based on the provided specification.
        /// The specification is used to apply additional query criteria.
        /// </summary>
        /// <param name="specification">An optional specification that defines the criteria for selecting entities.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of entities that match the criteria.</returns>
        Task<IEnumerable<T>> ListAsync(ISpecification<T> specification = null);

        /// <summary>
        /// Asynchronously adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add to the repository.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added entity.</returns>
        Task<T> AddAsync(T entity);



        /// <summary>
        /// Asynchronously updates an existing entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated entity.</returns>
        Task<T> UpdateAsync(T entity);

        // <summary>
        /// Asynchronously deletes an entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);

        Task<T> FirstOrDefaultAsync(ISpecification<T> spec);
    }
}
