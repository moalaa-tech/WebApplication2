using AutoMapper;
using CRM.Domain.Entities.InventoryManagement;
using CRM.WebApp.DTOs.InventoryManagement.Product;
using CRM.WebApp.DTOs.InventoryManagement.ProductType;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.InventoryManagment;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.WebApp.Services.Inventory
{
    public class ProductService : IProductService
    {

        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductType> _productTypeRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
    
        public ProductService(IRepository<Product> productRepository,
                              IRepository<ProductType> productTypeRepository,
                              IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync(p => p.ProductType).ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdWithIncludeAsync(p => p.Id == id, p => p.ProductType);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return false;
            }
            _productRepository.Delete(product);
            await _productRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ProductExistsAsync(int id)
        {
            var product = await _productRepository.ExistsAsync(a=>a.Id == id);
            return true;
        }

        public async Task<bool> UpdateProductAsync(UpdateProductDto productUpdateDto)
        {
            var existingProduct = await _productRepository.GetByIdAsync(productUpdateDto.Id);
            if (existingProduct == null)
            {
                return false; // Or throw a specific exception
            }

            _mapper.Map(productUpdateDto, existingProduct); // Map DTO to existing entity
            _productRepository.Update(existingProduct);
            await _productRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<SelectListItem>> GetProductTypeListItemsAsync()
        {
            var productTypes = await _productTypeRepository.GetAll().ToListAsync();
            return productTypes.Select(pt => new SelectListItem
            {
                Value = pt.Id.ToString(),
                Text = pt.Name
            });
        }

        private async Task<string> UploadImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0) return null;

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return uniqueFileName; // Store only the file name
        }

        private void DeleteImage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return;

            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products", fileName);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }



        public async Task<IEnumerable<ProductTypeDto>> GetAllProductTypesAsync()
        {
            var productTypes = _productTypeRepository.GetAll();
            var result = productTypes.Select(pt => new ProductTypeDto
            {
                Id = pt.Id,
                Name = pt.Name
            }).AsEnumerable();

            return await System.Threading.Tasks.Task.FromResult(result);
        }
    }
}
