using AutoMapper;
using CRM.Domain.Enums.SupplyChainManagement;
using CRM.WebApp.DTOs.SupplyChainManagement;
using CRM.WebApp.Services.SupplyChainManagement;
using CRM.WebApp.ViewModels.SupplyChainManagement.DemandPlan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.SupplyChainManagement
{
    //[Authorize]
    //[Route("scm/demand-plans")]
    [ApiController]
    [Route("api/[controller]")]
    public class DemandPlansController : ControllerBase
    {
        private readonly IDemandPlanService _demandPlanService;
        private readonly IMapper _mapper;
        private readonly ILogger<DemandPlansController> _logger;


        public DemandPlansController(IDemandPlanService demandPlanService, IMapper mapper, ILogger<DemandPlansController> logger)
        {
            _demandPlanService = demandPlanService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var demandPlans = await _demandPlanService.GetAllDemandPlansAsync();
            return Ok(demandPlans);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var demandPlan = await _demandPlanService.GetDemandPlanByIdAsync(id);
            if (demandPlan == null)
            {
                return NotFound();
            }

            return Ok(demandPlan);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DemandPlanCreateViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createDto = _mapper.Map<DemandPlanCreateDto>(model);
            await _demandPlanService.CreateDemandPlanAsync(createDto);

            return Ok(model);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] DemandPlanUpdateViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var updateDto = _mapper.Map<DemandPlanUpdateDto>(model);
                await _demandPlanService.UpdateDemandPlanAsync(id, updateDto);
            }
            catch
            {
                if (!await DemandPlanExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return Ok(model);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _demandPlanService.DeleteDemandPlanAsync(id);
            return Ok();
        }

        private async Task<bool> DemandPlanExists(int id)
        {
            var demandPlan = await _demandPlanService.GetDemandPlanByIdAsync(id);
            return demandPlan != null;
        }

        [HttpGet("Dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            try
            {
                var dashboard = await _demandPlanService.GetDashboardDataAsync();
                return Ok(dashboard);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving dashboard data");
                return Ok(new SupplyChainDashboardDto());
            }
        }

        [HttpPost("AddItem")]
        public async Task<IActionResult> AddItem([FromQuery] int planId, [FromBody] DemandPlanItemCreateDto itemDto)
        {
            try
            {
                var result = await _demandPlanService.AddItemToPlanAsync(planId, itemDto);
                return Ok(new { success = result, message = result ? "Item added successfully" : "Error adding item" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding item to plan");
                return BadRequest(new { success = false, message = "Error adding item to plan" });
            }
        }

        [HttpPost("UpdateItemStatus")]
        public async Task<IActionResult> UpdateItemStatus([FromQuery] int itemId, [FromQuery] bool isProcured)
        {
            try
            {
                var result = await _demandPlanService.UpdateItemProcurementStatusAsync(itemId, isProcured);
                return Ok(new { success = result, message = result ? "Status updated successfully" : "Error updating status" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating item status");
                return Ok(new { success = false, message = "Error updating status" });
            }
        }

        [HttpPost("GenerateForecast")]
        public async Task<IActionResult> GenerateForecast([FromQuery] int planId)
        {
            try
            {
                var result = await _demandPlanService.GenerateForecastAsync(planId);
                return Ok(new { success = result, message = result ? "Forecast generated successfully" : "Error generating forecast" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating forecast");
                return BadRequest(new { success = false, message = "Error generating forecast" });
            }
        }
    }
}