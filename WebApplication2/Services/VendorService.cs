using AutoMapper;
using CRM.Domain.Entities.AccountsPayable;
using CRM.WebApp.DTOs.Vendor;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services
{
    public class VendorService : IVendorService
    {
        private readonly IRepository<Vendor> _repository;
        private readonly IMapper _mapper;

        public VendorService(IRepository<Vendor> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VendorDto>> GetAllAsync(string search = null)
        {
            var vendors = await _repository.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<VendorDto>>(vendors);
        }

        public async Task<VendorDto> GetByIdAsync(int id)
        {
            var vendor = await _repository.GetByIdAsync(id);
            return _mapper.Map<VendorDto>(vendor);
        }

        public async Task<bool> AddAsync(VendorDto dto)
        {
            var vendor = _mapper.Map<Vendor>(dto);
            await _repository.AddAsync(vendor);
            return true;
        }

        public async Task<bool> UpdateAsync(VendorDto dto)
        {
            var vendor = _mapper.Map<Vendor>(dto);
            _repository.Update(vendor);
            return true;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var vendor = await _repository.GetByIdAsync(id);
            _repository.Delete(vendor);
            return true;
        }
    }

}
