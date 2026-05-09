using AutoMapper;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.WebApp.DTOs.Automation;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.MarketingAutomation
{
    public class AutomationService : IAutomationService
    {
        private readonly IRepository<Automation> _repo;
        private readonly IMapper _mapper;

        public AutomationService(IRepository<Automation> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<AutomationDto>> GetAllAsync()
        {
            var automations = await _repo.GetAllAsync().Include(a => a.Steps).ToListAsync();
            return _mapper.Map<List<AutomationDto>>(automations);
        }

        public async Task<AutomationDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<AutomationDto>(entity);
        }

        public async Task AddAsync(CreateAutomationDto dto)
        {
            var entity = _mapper.Map<Automation>(dto);
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateAutomationDto dto)
        {
            var entity = _mapper.Map<Automation>(dto);
            _repo.Update(entity);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity != null)
            {
                _repo.Delete(entity);
                await _repo.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _repo.SaveChangesAsync();
        }
    }

}
