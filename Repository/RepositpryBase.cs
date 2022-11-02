using Entities;
using HumanResource.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository
{
    public class RepositoryBase<TEntity, K> :
        HumanResource.Infrastructure.IRepositoryBase<TEntity, K> where TEntity: DomainEntity<K>
    {

        private readonly AppDbContext _context;

        public RepositoryBase(AppDbContext context)
        {
            _context = context;
        }

       

        public IQueryable<TEntity> FindAll(bool trackChange, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> items = _context.Set<TEntity>();
            if (!trackChange) items.AsNoTracking();

            if (includeProperties == null) return items;
            return includeProperties.Aggregate(items, (current, includeProperties) => current.Include(includeProperties));
        }

        public IQueryable<TEntity> FindAll(bool trackChange, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> items = _context.Set<TEntity>();
            if (!trackChange) items.AsNoTracking();

            if (includeProperties == null) return items.Where(predicate);
            items = includeProperties.Aggregate(items, (current, includeProperties) => current.Include(includeProperties));
            return items.Where(predicate);
        }

        public TEntity FindById(K id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return FindAll(false, includeProperties).SingleOrDefault(x => x.Id.Equals(id));
        }

        public async Task<TEntity> FindByIdAsync(K id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await FindAll(false, includeProperties).SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public TEntity FindSingle(bool trackChange, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return FindAll(trackChange, includeProperties).SingleOrDefault(predicate);
        }

        public Task<TEntity> FindSingleAsync(bool trackChange, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return FindAll(trackChange, includeProperties).SingleOrDefaultAsync(predicate);
        }

        public void Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}