using DomainLayer.Contracts;
using DomainLayer.Models;
using Presistence.Data;

namespace Presistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            // get Type Name of TEntity
            var typeName = typeof(TEntity).Name;
            if (_repositories.TryGetValue(typeName, out object? repository))
                return (IGenericRepository<TEntity, TKey>)repository;
            else
            {
                // Create object
                var Repo = new GenericRepository<TEntity, TKey>(_dbContext);
                // Store object in the dictionary
                _repositories.Add(typeName, Repo);
                // return object
                return Repo;
            }
        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
