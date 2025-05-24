using SAS.ScrapingManagementService.SharedKernel.Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


// Specification Pattern from : https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implemenation-entity-framework-core
namespace SAS.ScrapingManagementService.SharedKernel.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public BaseSpecification()
        {;
        }
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; set; } = new List<string>();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; set; }

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; } = false;

        public virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        public virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
        public virtual void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
        public virtual void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        public virtual void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        public void ApplyOptionalPagination(int? pageSize, int? pageNumber)
        {

            int actPageNumber = pageNumber.HasValue && pageNumber.Value > 0 ? pageNumber.Value : 1;
            int actPageSize = pageSize.HasValue && pageSize.Value > 0 && pageSize.Value <= 30 ? pageSize.Value : 30;

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                ApplyPaging((actPageNumber - 1) * actPageSize, actPageSize);
            }


        }
    }
}
