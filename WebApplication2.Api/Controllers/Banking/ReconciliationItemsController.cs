using AutoMapper;
using CRM.WebApp.DTOs.Banking;
using CRM.WebApp.Services.Finance_Accounting;
using CRM.WebApp.ViewModels.Banking;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.Banking
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReconciliationItemsController : ControllerBase
    {
        private readonly IReconciliationItemService _service;
        private readonly IMapper mapper;

        public ReconciliationItemsController(IReconciliationItemService service,
            IMapper _mapper
            )
        {
            _service = service;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int reconciliationId)
        {
            var item = await _service.GetAllByReconciliationIdAsync(reconciliationId);
            return Ok(item);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] EditReconciliationItemViewModel vm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var EditReconciliationItemdto = mapper.Map<UpdateReconciliationItemDto>(vm);
            await _service.UpdateAsync(EditReconciliationItemdto);
            return Ok(vm);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}