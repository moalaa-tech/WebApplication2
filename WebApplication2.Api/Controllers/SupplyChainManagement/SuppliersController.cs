using AutoMapper;
using CRM.WebApp.DTOs.SupplyChainManagement.Procurement;
using CRM.WebApp.Services.SupplyChainManagement;
using CRM.WebApp.ViewModels.SupplyChainManagement.Supplier;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.SupplyChainManagement
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;

        public SuppliersController(ISupplierService supplierService, IMapper mapper)
        {
            _supplierService = supplierService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            var viewModels = _mapper.Map<IEnumerable<SupplierViewModel>>(suppliers);
            return Ok(viewModels);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            return supplier == null ? NotFound() : Ok(_mapper.Map<SupplierViewModel>(supplier));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSupplierViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createDto = _mapper.Map<CreateSupplierDto>(viewModel);
            await _supplierService.CreateSupplierAsync(createDto);
            return Ok(viewModel);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] EditSupplierViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var updateDto = _mapper.Map<UpdateSupplierDto>(viewModel);
                await _supplierService.UpdateSupplierAsync(updateDto);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return Ok(viewModel);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _supplierService.DeleteSupplierAsync(id);
            return Ok();
        }
    }
}