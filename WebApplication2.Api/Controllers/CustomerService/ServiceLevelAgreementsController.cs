using AutoMapper;
using CRM.WebApp.DTOs.CustomerService;
using CRM.WebApp.Services.CustomerSupport_Service;
using CRM.WebApp.ViewModels.CustomerService.ServiceLevelAgreement;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.CustomerService
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceLevelAgreementsController : ControllerBase
    {
        private readonly ISLAService _slaService;
        private readonly IMapper _mapper;

        public ServiceLevelAgreementsController(
            ISLAService slaService,
            IMapper mapper)
        {
            _slaService = slaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var slas = await _slaService.GetAllActiveSLAsAsync();
            var viewModels = _mapper.Map<IEnumerable<ServiceLevelAgreementViewModel>>(slas);
            return Ok(viewModels);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceLevelAgreementViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createDto = _mapper.Map<SLACreateDto>(viewModel);
                await _slaService.CreateSLAAsync(createDto);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error creating SLA: {ex.Message}" });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] EditServiceLevelAgreementViewModel viewModel)
        {
            if (id != viewModel.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updateDto = _mapper.Map<SLAUpdateDto>(viewModel);
                await _slaService.UpdateSLAAsync(updateDto);

                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error updating SLA: {ex.Message}" });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var sla = await _slaService.GetSLADetailsAsync(id);
            if (sla == null)
                return NotFound();

            var viewModel = _mapper.Map<ServiceLevelAgreementViewModel>(sla);
            return Ok(viewModel);
        }
    }
}