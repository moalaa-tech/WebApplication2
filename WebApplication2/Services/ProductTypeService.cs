using AutoMapper;
using CRM.Domain.Entities.InventoryManagement;
using CRM.WebApp.DTOs.InventoryManagement.ProductType;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IRepository<ProductType> _repository;
        private readonly IMapper _mapper;

        public ProductTypeService(IRepository<ProductType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductTypeDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync().ToListAsync();
            return _mapper.Map<IEnumerable<ProductTypeDto>>(entities);
        }

        public async Task<ProductTypeDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<ProductTypeDto>(entity);
        }

        public async Task<bool> CreateAsync(CreateProductTypeDto dto)
        {
            var entity = _mapper.Map<ProductType>(dto);
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(UpdateProductTypeDto dto)
        {
            var entity = await _repository.GetByIdAsync(dto.Id);

            entity.Name = dto.Name;
            entity.Description = dto.Description;
            //entity.DateCreated = DateTime.Now;
            entity.DateModified = DateTime.Now;

            _repository.Update(entity);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
            {
                _repository.Delete(entity);
                await _repository.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }

}
