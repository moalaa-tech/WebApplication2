using AutoMapper;
using CRM.Domain.Entities.Accounting;
using CRM.WebApp.DTOs.Accounting;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.Finance_Accounting
{
    public class ExpenseCategoryService : IExpenseCategoryService
    {
        private readonly IRepository<ExpenseCategory> _repo;
        private readonly IMapper _mapper;

        public ExpenseCategoryService(IRepository<ExpenseCategory> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExpenseCategoryDto>> GetAllAsync()
        {
            var items = await _repo.GetAll().AsNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<ExpenseCategoryDto>>(items);
        }

        public async Task<ExpenseCategoryDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<ExpenseCategoryDto>(entity);
        }

        public async Task<ExpenseCategoryDto> CreateAsync(ExpenseCategoryCreateDto dto)
        {
            ExpenseCategory? entity = _mapper.Map<ExpenseCategory>(dto);
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
            return _mapper.Map<ExpenseCategoryDto>(entity);
        }

        public async Task<ExpenseCategoryDto?> UpdateAsync(int id, ExpenseCategoryUpdateDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
                return null;

            _mapper.Map(dto, entity);
            _repo.Update(entity);
            await _repo.SaveChangesAsync();

            return _mapper.Map<ExpenseCategoryDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
                return false;

            _repo.Delete(entity);
            return true;
        }

    }
}
