using CRM.WebApp.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CRM.WebApp.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAllAsync(params Expression<Func<T, object>>[] includes);
        IQueryable<T> Query();


        IQueryable<T> GetAllAsync(
                       Expression<Func<T, bool>>? filter = null,
                       Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                       Func<IQueryable<T>, IQueryable<T>>? include = null);

        Task<PaginatedList<T>> GetPaginatedAsync(int pageNumber, int pageSize, 
                        Expression<Func<T, bool>>? filter = null,
                        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                        Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null);

        //-----------------------------------------------------------------------------------

        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByIdWithIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? includes = null);


        //--------------------------------------------------------------------------------------------------------
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void RemoveRange(List<T> values);
        void Remove(T values);
        Task SaveChangesAsync();
        Task<bool> ExistsAsync(Expression<Func<T, bool>> filter);        
    }
}
