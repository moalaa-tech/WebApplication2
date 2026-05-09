using AutoMapper;
using CRM.Domain.Entities.InventoryManagement;
using CRM.WebApp.DTOs.InventoryManagement;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.Inventory
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IRepository<Warehouse> _repo;
        private readonly IMapper _mapper;


        public WarehouseService(IRepository<Warehouse> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        public async Task<IEnumerable<WarehouseDto>> GetAllAsync()
        {
            var warehouses = await _repo.GetAllAsync().ToListAsync();
            return _mapper.Map<IEnumerable<WarehouseDto>>(warehouses);
        }


        public async Task<WarehouseDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetAllAsync(w => w.Zones).FirstOrDefaultAsync(w => w.Id == id);
            return _mapper.Map<WarehouseDto?>(entity);
        }


        public async Task CreateAsync(WarehouseDto dto)
        {
            var entity = _mapper.Map<Warehouse>(dto);
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

        }


        public async Task UpdateAsync(WarehouseDto dto)
        {
            var entity = _mapper.Map<Warehouse>(dto);
            _repo.Update(entity);
            await _repo.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            _repo.Delete(entity);
        }
    }
}
