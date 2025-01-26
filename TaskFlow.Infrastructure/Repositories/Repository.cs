using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;
using TaskFlow.Domain.Core.Entities;
using TaskFlow.Domain.Core.Interfaces;

namespace TaskFlow.Infrastructure.Data.Repositories
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class
        where TId : struct
    {
        private readonly ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        public TEntity Add(TEntity entity)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(entity);
                transaction.Commit();
            }
            return entity;
        }

         public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _session.SaveAsync(entity);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Update(entity);
                transaction.Commit();
            }
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Update(entity);
                await transaction.CommitAsync();
            }
            return entity;
        }

        public void Delete(TEntity entity)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Delete(entity);
                transaction.Commit();
            }
        }

        public async Task DeleteAsync(TId id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                await _session.DeleteAsync(entity);
            }
        }

        public bool Exists(Func<TEntity, bool> where)
        {
            return _session.Query<TEntity>().Any(where);
        }

        public IEnumerable<TEntity> AddList(IEnumerable<TEntity> entities)
        {
            using (var transaction = _session.BeginTransaction())
            {
                foreach (var entity in entities)
                {
                    _session.Save(entity);
                }
                transaction.Commit();
            }
            return entities;
        }

        public IQueryable<TEntity> List(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _session.Query<TEntity>();
            return query;
        }

        public IQueryable<TEntity> ListWhere(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _session.Query<TEntity>().Where(where);
            return query;
        }

        public IQueryable<TEntity> ListWhereAndSortedBy<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order, bool ascending = true, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _session.Query<TEntity>().Where(where);
            query = ascending ? query.OrderBy(order) : query.OrderByDescending(order);
            return query;
        }

        public IQueryable<TEntity> ListSortedBy<TKey>(Expression<Func<TEntity, TKey>> order, bool ascending = true, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _session.Query<TEntity>();
            query = ascending ? query.OrderBy(order) : query.OrderByDescending(order);
            return query;
        }

        public TEntity GetBy(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return _session.Query<TEntity>().FirstOrDefault(where);
        }

        public TEntity GetById(TId id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return _session.Get<TEntity>(id);
        }

        public async Task<List<TEntity>> ListWhereAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _session.Query<TEntity>().Where(where);
            return await query.ToListAsync();
        }

        public async Task<List<TEntity>> ListWhereAndSortedByAsync<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order, bool ascending = true, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _session.Query<TEntity>().Where(where);
            query = ascending ? query.OrderBy(order) : query.OrderByDescending(order);
            return await query.ToListAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _session.Query<TEntity>().AnyAsync(where);
        }

        public async Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await _session.Query<TEntity>().FirstOrDefaultAsync(where);
        }

        public async Task<TEntity> GetByIdAsync(TId id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await _session.GetAsync<TEntity>(id);
        }

        public void Dispose()
        {
            _session.Dispose();
        }
    }
}
