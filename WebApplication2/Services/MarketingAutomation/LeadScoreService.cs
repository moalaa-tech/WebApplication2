using AutoMapper;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.WebApp.DTOs.LeadScore;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.MarketingAutomation
{
    public class LeadScoreService : ILeadScoreService
    {
        private readonly IRepository<LeadScore> _repository;
        private readonly IMapper _mapper;

        public LeadScoreService(IRepository<LeadScore> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LeadScoreDto>> GetAllAsync()
        {
            var leadScores = await _repository.GetAllAsync().ToListAsync();
            return _mapper.Map<IEnumerable<LeadScoreDto>>(leadScores);
        }

        public async Task<LeadScoreDto> GetByIdAsync(int id)
        {
            var leadScore = await _repository.GetAsync(a => a.Id == id, z => z.Include(q => q.Criteria));
            return _mapper.Map<LeadScoreDto>(leadScore);
        }

        public async Task<LeadScoreDto> CreateAsync(LeadScoreCreateDto createDto)
        {
            var leadScore = _mapper.Map<LeadScore>(createDto);
            await _repository.AddAsync(leadScore);
            return _mapper.Map<LeadScoreDto>(leadScore);
        }

        public async Task UpdateAsync(LeadScoreUpdateDto updateDto)
        {
            var leadScore = await _repository.GetAsync(a => a.Id == updateDto.Id, z => z.Include(q => q.Criteria));
            if (leadScore == null)
                throw new System.ArgumentException("Lead score not found");

            _mapper.Map(updateDto, leadScore);
            _repository.Update(leadScore);
        }

        public async Task DeleteAsync(int id)
        {
            var leadScore = await _repository.GetByIdAsync(id);
            if (leadScore == null)
                throw new System.ArgumentException("Lead score not found");

            _repository.Delete(leadScore);
        }

        public async Task<IEnumerable<LeadScoreDto>> GetActiveScoresAsync()
        {
            var leadScores = await _repository.GetByCondition(a=>a.IsActive == true).ToListAsync();
            return _mapper.Map<IEnumerable<LeadScoreDto>>(leadScores);
        }

        public async Task<IEnumerable<LeadScoreDto>> GetByTypeAsync(string scoreType)
        {
            var leadScores = await _repository.GetByCondition(a => a.IsActive == true).ToListAsync();
            return _mapper.Map<IEnumerable<LeadScoreDto>>(leadScores);
        }

        public async Task CalculateLeadScoreAsync(int leadId)
        {
            var activeScores = await _repository.GetAll().Where(a => a.IsActive == true).Include(a => a.Rules).ToListAsync();
            int totalScore = 0;

            foreach (var score in activeScores)
            {
                // In a real implementation, you would evaluate the criteria against the lead
                // This is simplified for demonstration
                if (EvaluateCriteria(score.Criteria.Name, leadId))
                {
                    totalScore += score.ScoreCriteriaId;
                }
            }

            //await _repository.Update(leadId, totalScore);
        }

        private bool EvaluateCriteria(string criteria, int leadId)
        {
            // Placeholder for actual criteria evaluation logic
            // This would typically involve parsing the criteria and checking against lead attributes
            return true; // Simplified for example
        }
    }
}
