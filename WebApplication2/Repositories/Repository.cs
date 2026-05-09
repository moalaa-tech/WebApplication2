using CRM.WebApp.DbContext;
using CRM.WebApp.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CRM.WebApp.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual IQueryable<T> Query() => _dbSet.AsQueryable();
        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<T> GetAllAsync(
                        Expression<Func<T, bool>>? filter = null,
                        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                        Func<IQueryable<T>, IQueryable<T>>? include = null)
                        {
                            IQueryable<T> query = _dbSet;

                            if (filter != null)
                                query = query.Where(filter);

                            if (include != null)
                                query = include(query);

                            if (orderBy != null)
                                query = orderBy(query);

            return query.AsNoTracking();
         }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).AsNoTracking();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            // Detach any existing instance to avoid tracking conflicts
            var existingEntry = _dbContext.ChangeTracker.Entries<T>().FirstOrDefault(e => e.Entity == entity);
            if (existingEntry != null)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                _dbSet.Update(entity);
            }
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T?> GetByIdWithIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public IQueryable<T> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }



        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
                query = includes(query);

            return await query.FirstOrDefaultAsync(predicate);
        }

        public void RemoveRange(List<T> values)
        {
            _dbSet.RemoveRange(values);
        }

        public void Remove(T value)
        {
            _dbSet.Remove(value);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.AnyAsync(filter);
        }

        public Task<PaginatedList<T>> GetPaginatedAsync(int pageNumber, int pageSize, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null)
        {
            throw new NotImplementedException();
        }
    }
}
