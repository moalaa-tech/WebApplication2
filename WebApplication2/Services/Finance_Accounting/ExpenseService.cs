using AutoMapper;
using CRM.Domain.Entities.Accounting;
using CRM.Domain.Entities.Banking;
using CRM.WebApp.DTOs.Accounting;
using CRM.WebApp.Paging;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.Finance_Accounting
{
    public class ExpenseService : IExpenseService
    {
        private readonly IRepository<Expense> Repository;
        private readonly IMapper _mapper;

        public ExpenseService(IRepository<Expense> _repository, IMapper mapper)
        {
            Repository = _repository;
            _mapper = mapper;
        }

        public async Task<ExpenseDto> CreateAsync(CreateExpenseDto dto, int createdBy)
        {
            Expense? entity = _mapper.Map<Expense>(dto);
            entity.CreatedById = createdBy;
            await Repository.AddAsync(entity);
            await Repository.SaveChangesAsync();
            //await _db.Entry(entity).Reference(e => e.Category).LoadAsync();
            entity = await Repository.GetByIdWithIncludeAsync(a => a.Id == entity.Id, s => s.Category);
            return _mapper.Map<ExpenseDto>(entity);
        }

        public async Task<ExpenseDto?> GetByIdAsync(int id)
        {
            var entity = await Repository.GetByIdWithIncludeAsync(a => a.Id == id, s => s.Category);
            return entity == null ? null : _mapper.Map<ExpenseDto>(entity);
        }

        // Paged list with filters
        public async Task<PagedResult<ExpenseDto>> GetPagedAsync(ExpenseQuery query)
        {
            var q = Repository.GetAllAsync(a => a.Category);


            if (query.CategoryId.HasValue) q = q.Where(x => x.CategoryId == query.CategoryId);
            if (query.From.HasValue) q = q.Where(x => x.Date >= query.From.Value.Date);
            if (query.To.HasValue) q = q.Where(x => x.Date <= query.To.Value.Date);
            if (!string.IsNullOrWhiteSpace(query.Search)) q = q.Where(x => x.Title.Contains(query.Search));

            var total = await q.CountAsync();
            var items = await q.OrderByDescending(x => x.Date)
                .Skip((query.Page - 1) * query.PageSize).Take(query.PageSize)
                .ToListAsync();

            var dtos = _mapper.Map<List<ExpenseDto>>(items);
            return new PagedResult<ExpenseDto>(dtos, total, query.Page, query.PageSize);
        }

        public async Task<bool> UpdateAsync(int id, CreateExpenseDto dto)
        {
            var e = await Repository.GetByIdWithIncludeAsync(a => a.Id == id);

            if (e == null) return false;
            e.Title = dto.Title;
            e.Amount = dto.Amount;
            e.Date = dto.Date;
            e.CategoryId = dto.CategoryId;
            e.Notes = dto.Notes;
            await Repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await Repository.GetByIdWithIncludeAsync(a => a.Id == id);
            if (e == null) return false;
            Repository.Remove(e);
            await Repository.SaveChangesAsync();
            return true;
        }

        public async Task<MonthlyReport[]> GetMonthlyReportAsync(int year)
        {
            var rows = await Repository.GetByCondition(x => x.Date.Year == year)
                .GroupBy(x => x.Date.Month)
                .Select(g => new MonthlyReport { Month = g.Key, Total = g.Sum(x => x.Amount) })
                .OrderBy(r => r.Month)
                .ToArrayAsync();
            return rows;
        }
    }
}
