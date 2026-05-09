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

namespace CRM.WebApp.Controllers
{
    public class ProductController : Controller
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
            return View(products);
        }

        [HttpGet]
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

            return View(product);
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.ProductTypes = DbContext.ProductTypes.AsEnumerable().Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            }).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductDto product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFileUpload != null && product.ImageFileUpload.Length > 0)
                {
                    // Save the file
                    product.ImageFile = await SaveFileAsync(product.ImageFileUpload, "product_images");
                }

                await _productService.CreateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ProductTypes = DbContext.ProductTypes.AsEnumerable().Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            }).ToList();

            return View(product);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
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
            var up = _mapper.Map<UpdateProductDto>(product);
            var ProductTypes = await _productTypeService.GetAllAsync();
            //ViewBag["ProductTypes"] = new SelectList(ProductTypes, "Id", "Name");
            ViewBag.ProductTypes = ProductTypes.AsEnumerable().Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            }).ToList();
            return View(up);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateProductDto product)
        {
            if (!ModelState.IsValid)
            {
                var ProductTypes = await _productTypeService.GetAllAsync();
                //ViewBag["ProductTypes"] = new SelectList(ProductTypes, "Id", "Name");
                ViewBag.ProductTypes = ProductTypes.AsEnumerable().Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                }).ToList();

                return View(product);
            }

            if (id != product.Id)
            {
                return NotFound();
            }
            var IfExist = await _productService.GetProductByIdAsync(id);
            if (IfExist == null)
            {
                return RedirectToAction(nameof(Index));
            }
            await _productService.UpdateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await DbContext.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
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
