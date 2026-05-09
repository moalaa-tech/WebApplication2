using AutoMapper;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.WebApp.DTOs.AutomationStep;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.MarketingAutomation
{
    public class AutomationStepService : IAutomationStepService
    {
        private readonly IRepository<AutomationStep> _repository;
        private readonly IMapper _mapper;

        public AutomationStepService(IRepository<AutomationStep> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AutomationStepDto>> GetAllAsync()
        {
            var steps = await _repository.GetAllAsync().ToListAsync();
            return _mapper.Map<IEnumerable<AutomationStepDto>>(steps);
        }

        public async Task<AutomationStepDto> GetByIdAsync(int id)
        {
            var step = await _repository.GetByIdAsync(id);
            return _mapper.Map<AutomationStepDto>(step);
        }

        public async Task CreateAsync(CreateAutomationStepDto dto)
        {
            var entity = _mapper.Map<AutomationStep>(dto);
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateAutomationStepDto dto)
        {
            var entity = _mapper.Map<AutomationStep>(dto);
            _repository.Update(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
            {
                _repository.Delete(entity);
                await _repository.SaveChangesAsync();
            }
        }
    }

}
