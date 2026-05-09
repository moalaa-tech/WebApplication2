using AutoMapper;
using CRM.WebApp.DTOs.Contact;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.ViewModels.Contact;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await _contactService.GetAllAsync();
            var viewModels = _mapper.Map<IEnumerable<ContactViewModel>>(contacts);
            return View(viewModels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var contact = await _contactService.GetByIdAsync(id);
            return View(_mapper.Map<ContactViewModel>(contact));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateContactDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _contactService.AddAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var contact = await _contactService.GetByIdAsync(id);
            return View(_mapper.Map<UpdateContactDto>(contact));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateContactDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _contactService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _contactService.GetByIdAsync(id);
            return View(_mapper.Map<ContactViewModel>(contact));
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _contactService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
