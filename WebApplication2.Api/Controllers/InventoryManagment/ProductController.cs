using AutoMapper;
using CRM.Domain.Entities.InventoryManagement;
using CRM.WebApp.DbContext;
using CRM.WebApp.DTOs.InventoryManagement.Product;
using CRM.WebApp.Services;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.Services.InventoryManagment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Api.Controllers.InventoryManagment
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private ApplicationContext DbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IProductTypeService _productTypeService;

        public ProductController(
            IProductService productService,
            ApplicationContext applicationContext,
            IWebHostEnvironment webHostEnvironment,
            IProductTypeService productTypeService,
            IMapper mapper
            )
        {
            DbContext = applicationContext;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
            _productService = productService;
            _productTypeService = productTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateProductDto product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFileUpload != null && product.ImageFileUpload.Length > 0)
                {
                    // Save the file
                    product.ImageFile = await SaveFileAsync(product.ImageFileUpload, "product_images");
                }

                await _productService.CreateProductAsync(product);
                return Ok(product);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UpdateProductDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return NotFound();
            }
            var IfExist = await _productService.GetProductByIdAsync(id);
            if (IfExist == null)
            {
                return Ok();
            }
            await _productService.UpdateProductAsync(product);
            return Ok(product);
        }

        [HttpGet("Upload")]
        public IActionResult Upload()
        {
            return Ok();
        }

        private async Task<string> SaveFileAsync(IFormFile file, string subdirectory)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", subdirectory, fileName);

            // Ensure the directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine("uploads", subdirectory, fileName).Replace("\\", "/"); // Store relative path
        }
    }
}