using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Presistence
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> InputQuery, ISpecifications<TEntity, Tkey> specifications) where TEntity : BaseEntity<Tkey>
        {
            var query = InputQuery;
            if (specifications.Criteria is not null)
            {
                query = query.Where(specifications.Criteria);
            }
            if (specifications.Includes is not null)
            {
                query = specifications.Includes.Aggregate(query, (current, includeExp) => current.Include(includeExp));
            }
            return query;
        }

    }
}
