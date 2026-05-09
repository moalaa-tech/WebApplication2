using AutoMapper;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.WebApp.DTOs.EmailTemplate;
using CRM.WebApp.Repositories;
using CRM.WebApp.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.MarketingAutomation
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IRepository<EmailTemplate> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmailTemplateService(IUnitOfWork unitOfWork, IRepository<EmailTemplate> repository, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmailTemplateDto>> GetAllAsync()
        {
            var templates = await _repository.GetAllAsync().ToListAsync();
            return _mapper.Map<IEnumerable<EmailTemplateDto>>(templates);
        }

        public async Task<EmailTemplateDto> GetByIdAsync(int id)
        {
            var template = await _repository.GetByIdAsync(id);
            return _mapper.Map<EmailTemplateDto>(template);
        }

        public async Task CreateAsync(CreateEmailTemplateDto dto)
        {
            var entity = _mapper.Map<EmailTemplate>(dto);
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateEmailTemplateDto dto)
        {
            var entity = _mapper.Map<EmailTemplate>(dto);
            _repository.Update(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
            {
                _repository.Delete(entity);
                await _repository.SaveChangesAsync();
            }
        }
    }

}
