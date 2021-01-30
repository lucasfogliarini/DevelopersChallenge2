using NiboBankConciliator.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace NiboBankConciliator.Core
{
    public interface IRepository
    {
        IQueryable<TEntity> Query<TEntity>() where TEntity : class, IEntity;
        void Add<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity;
        void Update<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void Commit();
    }
}
