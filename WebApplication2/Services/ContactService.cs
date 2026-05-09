using AutoMapper;
using CRM.Domain.Entities;
using CRM.WebApp.DTOs.Contact;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services
{
    public class ContactService : IContactService
    {
        private readonly IRepository<Contact> _repository;
        private readonly IMapper _mapper;

        public ContactService(IRepository<Contact> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContactDto>> GetAllAsync()
        {
            var contacts = await _repository.GetAllAsync(c => c.Company).ToListAsync();
            return _mapper.Map<IEnumerable<ContactDto>>(contacts);
        }

        public async Task<ContactDto> GetByIdAsync(int id)
        {
            var contact = await _repository.GetByIdAsync(id);
            return _mapper.Map<ContactDto>(contact);
        }

        public async Task AddAsync(CreateContactDto dto)
        {
            var entity = _mapper.Map<Contact>(dto);
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(UpdateContactDto dto)
        {
            var entity = _mapper.Map<Contact>(dto);
            _repository.Update(entity);
            await _repository.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var contact = await _repository.GetByIdAsync(id);

            _repository.Delete(contact);
            await _repository.SaveChangesAsync();
        }
    }

}
