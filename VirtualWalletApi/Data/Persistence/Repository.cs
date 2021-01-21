using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace VirtualWalletApi.Data.Persistence
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public void Add(TEntity entity)
        {
            Context.Add(entity);
            Context.SaveChanges();
        }

        public async Task AddAsync(TEntity entity)
        {
            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            Context.AddRange(entities);
            await Context.SaveChangesAsync();
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            Context.UpdateRange(entities);
            Context.SaveChanges();
        }

        public bool Exists(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).FirstOrDefault() != null;

        }

        public TEntity Find(Guid id)
        {
            return Context.Find<TEntity>(id);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).ToList();
        }

        public IQueryable<TEntity> QueryAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? Context.Set<TEntity>() : Context.Set<TEntity>().Where(predicate);

        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>();
        }

        public void Remove(TEntity entity)
        {
            Context.Remove(entity);
            Context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.RemoveRange(entities);
            Context.SaveChanges();
        }

        public TEntity SingleOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public TEntity FirstOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public void Update(TEntity entity)
        {
            Context.Update(entity);
            Context.SaveChanges();
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
