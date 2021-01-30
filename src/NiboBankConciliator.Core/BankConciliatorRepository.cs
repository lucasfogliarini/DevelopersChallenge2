using Microsoft.EntityFrameworkCore;
using NiboBankConciliator.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace NiboBankConciliator.Core
{
    internal class BankConciliatorRepository : IBankConciliatorRepository
    {
        readonly DbContext _dbContext;
        public BankConciliatorRepository(BankConciliatorDbContext bankConciliatorDbContext)
        {
            _dbContext = bankConciliatorDbContext;
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class, IEntity
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            _dbContext.Add(entity);
        }

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity
        {
            _dbContext.AddRange(entities);
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            _dbContext.Update(entity);
        }
    }
}
