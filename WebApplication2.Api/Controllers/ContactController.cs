using AutoMapper;
using CRM.WebApp.DTOs.Contact;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.ViewModels.Contact;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var contacts = await _contactService.GetAllAsync();
            var viewModels = _mapper.Map<IEnumerable<ContactViewModel>>(contacts);
            return Ok(viewModels);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var contact = await _contactService.GetByIdAsync(id);
            return Ok(_mapper.Map<ContactViewModel>(contact));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateContactDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _contactService.AddAsync(dto);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UpdateContactDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _contactService.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _contactService.DeleteAsync(id);
            return Ok();
        }
    }
}