using AutoMapper;
using CRM.WebApp.DTOs.CustomerService.ServiceRequest;
using CRM.WebApp.Services.CustomerSupport_Service;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.ViewModels.CustomerService;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.CustomerService
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceRequestController : ControllerBase
    {
        private readonly IServiceRequestService _service;
        private readonly IMapper mapper;

        public ServiceRequestController(IServiceRequestService service,
            IMapper _mapper
            )
        {
            _service = service;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var requests = await _service.GetAllServiceRequestsAsync();
            return Ok(requests);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceRequestViewModel vm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var dto = mapper.Map<ServiceRequestCreateDto>(vm);
            await _service.CreateServiceRequestAsync(dto);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ServiceRequestViewModel vm)
        {
            if (id != vm.Id) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var dto = mapper.Map<ServiceRequestUpdateDto>(vm);

            await _service.UpdateServiceRequestAsync(dto);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteServiceRequestAsync(id);
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var request = await _service.GetServiceRequestByIdAsync(id);
            if (request == null) return NotFound();
            return Ok(request);
        }
    }
}