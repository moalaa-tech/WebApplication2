using AutoMapper;
using CRM.Domain.Entities.SupplyChainManagement;
using CRM.WebApp.DTOs.SupplyChainManagement.Procurement;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.SupplyChainManagement
{
    public class SupplierService : ISupplierService
    {
        private readonly IRepository<Supplier> SupplierRepository;

        private readonly IMapper _mapper;

        public SupplierService(IRepository<Supplier> _SupplierRepository, IMapper mapper)
        {
            _mapper = mapper;
            SupplierRepository = _SupplierRepository;
        }

        public async Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync()
        {
            var suppliers = await SupplierRepository.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
        }

        public async Task<SupplierDto> GetSupplierByIdAsync(int id)
        {
            var supplier = await SupplierRepository.GetByIdAsync(id);
            return _mapper.Map<SupplierDto>(supplier);
        }

        public async Task<SupplierDto> CreateSupplierAsync(CreateSupplierDto createSupplierDto)
        {
            var supplier = _mapper.Map<Supplier>(createSupplierDto);
            await SupplierRepository.AddAsync(supplier);
            await SupplierRepository.SaveChangesAsync();
            return _mapper.Map<SupplierDto>(supplier);
        }

        public async Task UpdateSupplierAsync(UpdateSupplierDto updateSupplierDto)
        {
            var supplier = await SupplierRepository.GetByIdAsync(updateSupplierDto.Id);
            if (supplier == null)
                throw new ArgumentException("Supplier not found");

            _mapper.Map(updateSupplierDto, supplier);
            supplier.DateModified = DateTime.UtcNow;

            SupplierRepository.Update(supplier);
            await SupplierRepository.SaveChangesAsync();
        }

        public async Task DeleteSupplierAsync(int id)
        {
            var supplier = await SupplierRepository.GetByIdAsync(id);
            if (supplier != null)
            {
                SupplierRepository.Remove(supplier);
                await SupplierRepository.SaveChangesAsync();
            }
        }

        public async Task<bool> SupplierExistsAsync(int id)
        {
            return await SupplierRepository.GetAll().AnyAsync(e => e.Id == id);
        }
    }
}
