using AutoMapper;
using CRM.Domain.Entities;
using CRM.WebApp.DTOs.Business;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services
{
    public class BusinessService : IBusinessService
    {

        private readonly IRepository<Business> _BusinessRepository;
        private readonly IMapper _mapper;

        public BusinessService(IRepository<Business> businessRepository, IMapper mapper)
        {
            _BusinessRepository = businessRepository;
            _mapper = mapper;
        }


        public async Task<bool> BusinessExistsAsync(int id)
        {
            var Business = await _BusinessRepository.GetByIdAsync(id);
            if (Business != null)
                return true;
            return false;
        }

        public async Task<BusinessDto> CreateBusinessAsync(CreateBusinessDto createBusinessDto)
        {
            var Business = _mapper.Map<Business>(createBusinessDto);

            await _BusinessRepository.AddAsync(Business);
            return _mapper.Map<BusinessDto>(Business);
        }

        public async Task<bool> DeleteBusinessAsync(int id)
        {
            var Business = await _BusinessRepository.GetByIdAsync(id);
            if (Business != null)
            {
                Business.IsActive = false;
                _BusinessRepository.Update(Business);
                return true;
            }          
            return false;
        }

        public async Task<IEnumerable<BusinessDto>> GetAllBusinesessAsync()
        {
            var companies = await _BusinessRepository.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<BusinessDto>>(companies);
        }

        public async Task<BusinessDto> GetBusinessByIdAsync(int id)
        {
            var business = await _BusinessRepository.GetByIdAsync(id);
            return _mapper.Map<BusinessDto>(business);
        }

        public async Task<bool> UpdateBusinessAsync(UpdateBusinessDto updateBusinessDto)
        {
            var business = await _BusinessRepository.GetByIdAsync(updateBusinessDto.Id);
            business.Name = updateBusinessDto.Name;
            business.DateModified = DateTime.Now;
            _BusinessRepository.Update(business);
            return true;
        }
    }
}
