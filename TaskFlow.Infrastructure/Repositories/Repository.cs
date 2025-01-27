using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Core.Interfaces;
using TaskFlow.Infrastructure.Data.Context;

namespace TaskFlow.Infrastructure.Data.Repositories
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class
        where TId : struct
    {
        private readonly DbSet<TEntity> _dataSet;
        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected AppDbContext DbContext => DbFactory.Init();

        public Repository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            _dataSet = DbContext.Set<TEntity>();
        }

        public Repository(DbContext context) => Context = context;

        protected readonly DbContext Context;

        public TEntity Add(TEntity entity)
        {
            _dataSet.Add(entity);
            Context.SaveChanges();
            return entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dataSet.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            _dataSet.Update(entity);
            Context.SaveChanges();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dataSet.Update(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public void Delete(TEntity entity)
        {
            _dataSet.Remove(entity);
            Context.SaveChanges();
        }

        public async Task DeleteAsync(TId id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dataSet.Remove(entity);
                await Context.SaveChangesAsync();
            }
        }

        public bool Exists(Func<TEntity, bool> where)
        {
            return _dataSet.AsQueryable().Any(where);
        }

        public IEnumerable<TEntity> AddList(IEnumerable<TEntity> entities)
        {
            _dataSet.AddRange(entities);
            Context.SaveChanges();
            return entities;
        }

        public IQueryable<TEntity> List(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dataSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IQueryable<TEntity> ListWhere(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dataSet.Where(where);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IQueryable<TEntity> ListWhereAndSortedBy<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order, bool ascending = true, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dataSet.Where(where);
            query = ascending ? query.OrderBy(order) : query.OrderByDescending(order);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IQueryable<TEntity> ListSortedBy<TKey>(Expression<Func<TEntity, TKey>> order, bool ascending = true, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dataSet;
            query = ascending ? query.OrderBy(order) : query.OrderByDescending(order);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public TEntity GetBy(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dataSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.FirstOrDefault(where);
        }

        public TEntity GetById(TId id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dataSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.FirstOrDefault(e => EF.Property<TId>(e, "Id").Equals(id));
        }

        public async Task<List<TEntity>> ListWhereAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dataSet.Where(where);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.ToListAsync();
        }

        public async Task<List<TEntity>> ListWhereAndSortedByAsync<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order, bool ascending = true, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dataSet.Where(where);
            query = ascending ? query.OrderBy(order) : query.OrderByDescending(order);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.ToListAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _dataSet.AnyAsync(where);
        }

        public async Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dataSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.FirstOrDefaultAsync(where);
        }

        public async Task<TEntity> GetByIdAsync(TId id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dataSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.FirstOrDefaultAsync(e => EF.Property<TId>(e, "Id").Equals(id));
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
