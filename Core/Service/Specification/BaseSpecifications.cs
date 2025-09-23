using DomainLayer.Contracts;
using DomainLayer.Models;
using System.Linq.Expressions;

namespace Service.Specification
{
    abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteriaExpression)
        {
            Criteria = criteriaExpression;

        }

        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        #region Includes
        public List<Expression<Func<TEntity, object>>> Includes { get; private set; } = [];
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) => Includes.Add(includeExpression);
        #endregion

        #region Sorting
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }
        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpr) => OrderBy = orderByExpr;
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpr) => OrderByDescending = orderByDescExpr;
        #endregion

    }
}
