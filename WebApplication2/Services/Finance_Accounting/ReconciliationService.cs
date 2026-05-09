using AutoMapper;
using CRM.Domain.Entities.Banking;
using CRM.WebApp.DTOs.Banking;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.Finance_Accounting
{
    public class ReconciliationService : IReconciliationService
    {
        private readonly IRepository<Reconciliation> _reconciliationRepo;
        private readonly IRepository<ReconciliationItem> _itemRepo;
        private readonly IMapper _mapper;

        public ReconciliationService(
            IRepository<Reconciliation> reconciliationRepo,
            IRepository<ReconciliationItem> itemRepo,
            IMapper mapper)
        {
            _reconciliationRepo = reconciliationRepo;
            _itemRepo = itemRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReconciliationDto>> GetAllAsync()
        {
            var entities = await _reconciliationRepo.GetAllAsync(includes: q => q.BankAccount).ToListAsync();
            return _mapper.Map<IEnumerable<ReconciliationDto>>(entities);
        }

        public async Task<ReconciliationDto> GetByIdAsync(int id)
        {
            var entity = await _reconciliationRepo.GetByIdAsync(id);
            return _mapper.Map<ReconciliationDto>(entity);
        }

        public async Task CreateAsync(CreateReconciliationDto dto)
        {
            var entity = _mapper.Map<Reconciliation>(dto);
            await _reconciliationRepo.AddAsync(entity);
        }

        public async Task<IEnumerable<ReconciliationItemDto>> GetItemsAsync(int reconciliationId)
        {
            var items = await _itemRepo.GetAsync(x => x.ReconciliationId == reconciliationId, q => q.Include(t => t.BankTransaction));
            return _mapper.Map<IEnumerable<ReconciliationItemDto>>(items);
        }

        public async Task CreateItemAsync(CreateReconciliationItemDto dto)
        {
            var entity = _mapper.Map<ReconciliationItem>(dto);
            await _itemRepo.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var items = await _itemRepo.GetAsync(x => x.ReconciliationId == id, q => q.Include(t => t.BankTransaction));

            _itemRepo.Delete(items);
        }
    }
}
