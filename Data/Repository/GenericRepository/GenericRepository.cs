using Microsoft.EntityFrameworkCore;
using Portfolio.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Data.Repository.GenericRepository
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        private PortfolioDBContext _context;
        private DbSet<Entity> _dbSet;

        public GenericRepository(PortfolioDBContext context)
        {
            _context = context;
            _dbSet = context.Set<Entity>();
        }

        public async Task<List<Entity>> GetAsync(
            Expression<Func<Entity, bool>> filter = null,
            Expression<Func<Entity, string>> orderBy = null,
            Expression<Func<Entity, string>> orderByDescending = null,
            List<string> includeProperties = null)
        {
            IQueryable<Entity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }
            if (orderByDescending != null)
            {
                query = query.OrderByDescending(orderByDescending);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public virtual async Task<List<object>> GetAsync(
            Expression<Func<Entity, object>> select,
            Expression<Func<Entity, bool>> filter = null,
            Expression<Func<Entity, string>> orderBy = null,
            Expression<Func<Entity, string>> orderByDescending = null,
            List<string> includeProperties = null)
        {
            IQueryable<Entity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }
            if (orderByDescending != null)
            {
                query = query.OrderByDescending(orderByDescending);
            }

            return await query.AsNoTracking().Select(select).ToListAsync();
        }

        public async Task<Entity> GetFirstByFilter(Expression<Func<Entity, bool>> filter, List<string> includeProperties = null)
        {
            IQueryable<Entity> query = _dbSet.Where(filter);
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.FirstOrDefaultAsync(filter);
        }

        public virtual async Task<Entity> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task AddAsync(Entity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddAsync(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                await _dbSet.AddAsync(entity);
            }
        }

        public virtual async Task DeleteAsync(object id)
        {
            Entity entityToDelete = await _dbSet.FindAsync(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(Entity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public virtual void Update(Entity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
