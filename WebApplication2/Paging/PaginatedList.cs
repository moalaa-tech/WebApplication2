using AutoMapper.QueryableExtensions;
using AutoMapper;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Paging
{
    public record PagedResult<T>(IEnumerable<T> Items, int TotalCount, int Page, int PageSize);

    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; }
        public int TotalPages { get; }
        public int TotalCount { get; internal set; }
        public bool HasNextPage => PageIndex < TotalPages;
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public static PaginatedList<T> Create(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }


        // ✅ For use with AutoMapper projection (DTO)
        public static async Task<PaginatedList<TDestination>> CreateAsync<TSource, TDestination>(
            IQueryable<TSource> source, int pageIndex, int pageSize, IConfigurationProvider configuration)
        {
            var count = await source.CountAsync();
            var items = await source
                .ProjectTo<TDestination>((AutoMapper.IConfigurationProvider)configuration)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedList<TDestination>(items, count, pageIndex, pageSize);
        }
    }

}
