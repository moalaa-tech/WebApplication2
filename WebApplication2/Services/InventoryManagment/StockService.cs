using AutoMapper;
using CRM.Domain.Entities.InventoryManagement;
using CRM.WebApp.DTOs.InventoryManagement;
using CRM.WebApp.DTOs.InventoryManagement.Product;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.InventoryManagment
{
    public class StockService : IStockService
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<StockTransaction> _transRepo;
        private readonly IMapper _mapper;

        public StockService(
            IRepository<Product> productRepo,
            IRepository<StockTransaction> transRepo,
            IMapper mapper)
        {
            _productRepo = productRepo;
            _transRepo = transRepo;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            var items = await _productRepo.GetAll().Include(x => x.ProductType).ToListAsync();
            return _mapper.Map<List<ProductDto>>(items);
        }

        public async Task<ProductDto?> GetProductAsync(int id)
        {
            var entity = await _productRepo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<ProductDto>(entity);
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            await _productRepo.AddAsync(entity);
            return _mapper.Map<ProductDto>(entity);
        }

        public async Task<bool> UpdateProductAsync(int id, UpdateProductDto dto)
        {
            var entity = await _productRepo.GetByIdAsync(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);

            _productRepo.Update(entity);
            await _productRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var entity = await _productRepo.GetByIdAsync(id);
            if (entity == null) return false;

            _productRepo.Delete(entity);
            await _productRepo.SaveChangesAsync();

            return true;
        }

        public async Task<bool> StockInAsync(StockInDto dto)
        {
            var product = await _productRepo.GetByIdAsync(dto.ProductId);
            if (product == null) return false;

            product.QuantityInStock += dto.Quantity;

            _productRepo.Update(product);
            await _productRepo.SaveChangesAsync();
            var trans = new StockTransaction
            {
                ItemId = dto.ProductId,
                Quantity = dto.Quantity,
                TransactionType = Domain.Enums.TransactionType.In,
                Cost = dto.Cost,
                Note = dto.Notes,
                TransactionDate = DateTime.UtcNow
            };
            await _transRepo.AddAsync(trans);
            await _transRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> StockOutAsync(StockOutDto dto)
        {
            var product = await _productRepo.GetByIdAsync(dto.ProductId);
            if (product == null || product.QuantityInStock < dto.Quantity)
                return false;

            product.QuantityInStock -= dto.Quantity;

            _productRepo.Update(product);
            await _productRepo.SaveChangesAsync();

            var trans = new StockTransaction
            {
                ItemId = dto.ProductId,
                Quantity = dto.Quantity,
                TransactionType = Domain.Enums.TransactionType.Out,
                Cost = dto.Cost,
                Note = dto.Notes,
                TransactionDate = DateTime.UtcNow
            };

            await _transRepo.AddAsync(trans);
            await _transRepo.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AdjustStockAsync(StockAdjustmentDto dto)
        {
            var product = await _productRepo.GetByIdAsync(dto.ProductId);
            if (product == null) return false;

            product.QuantityInStock = dto.NewQuantity;

             _productRepo.Update(product);
            await _productRepo.SaveChangesAsync();

            var trans = new StockTransaction
            {
                ItemId = dto.ProductId,
                Quantity = dto.NewQuantity,
                TransactionType = Domain.Enums.TransactionType.Adjustment,
                Note = dto.Notes,
                TransactionDate = DateTime.UtcNow
            };

            await _transRepo.AddAsync(trans);
            await _transRepo.SaveChangesAsync();

            return true;
        }
    }

}
