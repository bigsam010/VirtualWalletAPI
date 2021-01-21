using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace VirtualWalletApi.Data.Persistence
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Find(Guid id);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        bool Exists(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        void Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Save();
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> QueryAll(Expression<Func<TEntity, bool>> predicate = null);
        TEntity FirstOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
    }
}