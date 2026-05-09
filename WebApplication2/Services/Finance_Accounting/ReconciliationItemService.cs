using AutoMapper;
using CRM.Domain.Entities.Banking;
using CRM.WebApp.DTOs.Banking;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.Finance_Accounting
{
    public class ReconciliationItemService : IReconciliationItemService
    {
        private readonly IRepository<ReconciliationItem> _repository;
        private readonly IMapper _mapper;

        public ReconciliationItemService(IRepository<ReconciliationItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ReconciliationItemDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<ReconciliationItemDto>(entity);
        }

        public async Task CreateAsync(CreateReconciliationItemDto dto)
        {
            var entity = _mapper.Map<ReconciliationItem>(dto);
            await _repository.AddAsync(entity);

        }

        public async Task UpdateAsync(UpdateReconciliationItemDto vm)
        {
            var entity = await _repository.GetByIdAsync(vm.Id);
            entity.AdjustedAmount = vm.AdjustedAmount;
            entity.Status = vm.Status;
            entity.Notes = vm.Notes;
            _repository.Update(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            _repository.Delete(entity);
        }

        public async Task<IEnumerable<ReconciliationItemDto>> GetAllByReconciliationIdAsync(int reconciliationId)
        {
            var entity = await _repository.GetByCondition(a => a.ReconciliationId == reconciliationId).ToListAsync();
            return _mapper.Map<IEnumerable<ReconciliationItemDto>>(entity);
        }


    }
}
