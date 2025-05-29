
using Microsoft.EntityFrameworkCore;
using SAS.ScrapingManagementService.SharedKernel.Entities;
using SAS.ScrapingManagementService.SharedKernel.Utilities;


namespace SAS.ScrapingManagementService.Infrastructure.Persistence.Repositories.Base
{
    public class SpecificationEvaluator<T, TId> where T : BaseEntity<TId>
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;
            if (specification is not null)
            {
                // modify the IQueryable using the specification's criteria expression
                if (specification.Criteria != null)
                {
                    query = query.Where(specification.Criteria);
                }

                // Includes all expression-based includes
                query = specification.Includes.Aggregate(
                    query,
                    (current, include) => current.Include(include));

                // Include any string-based include statements
                query = specification.IncludeStrings.Aggregate(
                    query,
                    (current, include) => current.Include(include));

                // Apply ordering if expressions are set
                if (specification.OrderBy != null)
                {
                    query = query.OrderBy(specification.OrderBy);
                }
                else if (specification.OrderByDescending != null)
                {
                    query = query.OrderByDescending(specification.OrderByDescending);
                }

                //if (specification.GroupBy is not null)
                //{
                //    query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
                //}

                // Apply paging if enabled
                if (specification.IsPagingEnabled)
                {
                    query = query.Skip(specification.Skip)
                                 .Take(specification.Take);
                }
            }
            return query;
        }

    }
}
