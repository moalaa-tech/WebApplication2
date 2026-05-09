using AutoMapper;
using CRM.Domain.Entities.SalesManagement;
using CRM.WebApp.DTOs.Opportunity;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.SalesManagement
{
    public class OpportunityService : IOpportunityService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Opportunity> _orderRepository;

        public OpportunityService(IMapper mapper, IRepository<Opportunity> orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }


        public async Task<bool> CreateOpportunityAsync(CreateOpportunityDto createOpportunity)
        {
            var order = _mapper.Map<Opportunity>(createOpportunity);

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOpportunityAsync(int id)
        {
            var opportunity = await _orderRepository.GetByIdAsync(id);
            var opportunityDto = _mapper.Map<OpportunityDto>(opportunity);
            _orderRepository.Delete(opportunity);
            return true;

        }

        public async Task<OpportunityDto?> GetOpportunityAsync(int id)
        {
            var opportunity = await _orderRepository.GetByIdAsync(id);
            var opportunityDto = _mapper.Map<OpportunityDto>(opportunity);
            return opportunityDto;
        }

        public async Task<List<OpportunityDto>> GetOpportunitiesAsync()
        {
            var opportunity = await _orderRepository.GetAll().ToListAsync();
            var result = _mapper.Map<List<OpportunityDto>>(opportunity);
            return result; ;
        }

        public async Task<bool> UpdateOpportunityAsync(UpdateOpportunityDto updateOpportunity)
        {
            var order = _mapper.Map<Opportunity>(updateOpportunity);
            _orderRepository.Update(order);
            await _orderRepository.SaveChangesAsync();
            return true;
        }

        public Task<OpportunityDto> GetOpportunityByIdAsync(int value)
        {
            throw new NotImplementedException();
        }
    }
}
