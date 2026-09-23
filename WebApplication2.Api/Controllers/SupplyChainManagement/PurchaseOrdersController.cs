using AutoMapper;
using CRM.WebApp.DTOs.SupplyChainManagement.Procurement;
using CRM.WebApp.DTOs.SupplyChainManagement.Purchase;
using CRM.WebApp.Services.SupplyChainManagement;
using CRM.WebApp.ViewModels.SupplyChainManagement.Purchase;
using CRM.WebApp.ViewModels.SupplyChainManagement.Supplier;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.SupplyChainManagement
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;

        public PurchaseOrdersController(
            IPurchaseOrderService purchaseOrderService,
            ISupplierService supplierService,
            IMapper mapper)
        {
            _purchaseOrderService = purchaseOrderService;
            _supplierService = supplierService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var purchaseOrders = await _purchaseOrderService.GetAllPurchaseOrdersAsync();
            var viewModels = _mapper.Map<IEnumerable<PurchaseOrderViewModel>>(purchaseOrders);
            return Ok(viewModels);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var purchaseOrder = await _purchaseOrderService.GetPurchaseOrderByIdAsync(id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<PurchaseOrderViewModel>(purchaseOrder);
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePurchaseOrderViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var createDto = _mapper.Map<CreatePurchaseOrderDto>(viewModel);
                await _purchaseOrderService.CreatePurchaseOrderAsync(createDto);
                return Ok(viewModel);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] EditPurchaseOrderViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var updateDto = _mapper.Map<UpdatePurchaseOrderDto>(viewModel);
                await _purchaseOrderService.UpdatePurchaseOrderAsync(updateDto);
                return Ok(viewModel);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _purchaseOrderService.DeletePurchaseOrderAsync(id);
            return Ok();
        }

        [HttpPost("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus([FromQuery] int id, [FromQuery] string status)
        {
            try
            {
                await _purchaseOrderService.ChangeStatusAsync(id, status);
                return Ok(new { message = $"Status changed to {status} successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("VerifyPONumber")]
        public async Task<IActionResult> VerifyPONumber([FromQuery] string poNumber, [FromQuery] int id = 0)
        {
            var exists = await _purchaseOrderService.PONumberExistsAsync(poNumber, id == 0 ? null : id);
            return Ok(!exists);
        }

        // Helper method to get supplier dropdown list
        private async Task<List<SupplierDropdownViewModel>> GetSupplierDropdownList()
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            return _mapper.Map<List<SupplierDropdownViewModel>>(suppliers);
        }
    }
}