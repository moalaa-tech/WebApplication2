using AutoMapper;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.Domain.Entities.SalesManagement;
using CRM.WebApp.DTOs.Lead;
using CRM.WebApp.DTOs.MarketingAutomation;
using CRM.WebApp.EmailIntegration;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.SalesManagement
{
    public class LeadService : ILeadService
    {
        private readonly IRepository<Lead> _repo;
        private readonly IMapper _mapper;
        private readonly IEmailService EmailService;
        private readonly IRepository<LeadScore> LeadScoreRepository;
        private readonly IRepository<ScoreCriteria> CriteriaRepository;
        public LeadService(
            IRepository<Lead> repo, 
            IMapper mapper, 
            IEmailService _EmailService,
            IRepository<LeadScore> _LeadScoreRepository,
            IRepository<ScoreCriteria> _CriteriaRepository
            )
        {
            _repo = repo;
            _mapper = mapper;
            EmailService = _EmailService;
            LeadScoreRepository = _LeadScoreRepository;
            CriteriaRepository = _CriteriaRepository;
        }

        public async Task<List<LeadDto>> GetLeadsAsync()
        {
            var Leads = await _repo.GetAll().ToListAsync();


            return _mapper.Map<List<LeadDto>>(Leads);

        }
        public async Task<LeadDto?> GetLeadAsync(int id)
        {
            var lead = await _repo.GetByIdAsync(id);
            return _mapper.Map<LeadDto>(lead);
        }


        public async Task<bool> CreateLeadAsync(LeadDto lead)
        {
            var _lead = _mapper.Map<Lead>(lead);
            await _repo.AddAsync(_lead);
            await EmailService.SendEmailAsync(lead.Email, "Welcome!", $"Hi {lead.ContactPerson}, thanks for your interest. We'll follow up soon!");

            return true;
        }

        public async Task<bool> UpdateLeadAsync(LeadDto lead)
        {
            var _lead = _mapper.Map<Lead>(lead);

            _repo.Update(_lead);
            await EmailService.SendEmailAsync(lead.Email, "Welcome!", $"Hi {lead.ContactPerson}, thanks for your interest. We'll follow up soon!");

            return true;
        }

        public async Task<bool> DeleteLeadAsync(int id)
        {
            var lead = await _repo.GetByIdAsync(id);
            _repo.Delete(lead);
            return true;
        }

        public Task<LeadDto> GetLeadByIdAsync(int value)
        {
            throw new NotImplementedException();
        }


        public async Task<int> CalculateTotalScoreAsync(int leadId)
        {
            var scores = await LeadScoreRepository.GetAllAsync().ToListAsync();
            return scores.Where(ls => ls.LeadId == leadId).Sum(ls => ls.Points);
        }

        public async Task<IEnumerable<LeadScoreDto>> GetLeadScoresAsync(int leadId)
        {
            var scores = await LeadScoreRepository.GetByCondition(x => x.LeadId == leadId).ToListAsync();
            return _mapper.Map<IEnumerable<LeadScoreDto>>(scores);
        }


        public async Task AddScoreToLeadAsync(int leadId, int criteriaId, int? customPoints = null)
        {
            var criteria = await CriteriaRepository.GetByIdAsync(criteriaId);
            if (criteria == null) throw new Exception("Criteria not found");

            var existingScore = (await LeadScoreRepository.GetAllAsync().ToListAsync())
                .FirstOrDefault(ls => ls.LeadId == leadId && ls.ScoreCriteriaId == criteriaId);

            if (existingScore != null)
            {
                existingScore.Points = customPoints ?? criteria.DefaultPoints;
                LeadScoreRepository.Update(existingScore);
            }
            else
            {
                var newScore = new LeadScore
                {
                    LeadId = leadId,
                    ScoreCriteriaId = criteriaId,
                    Points = customPoints ?? criteria.DefaultPoints,
                    ScoredDate = DateTime.UtcNow
                };
                await LeadScoreRepository.AddAsync(newScore);
            }

            await LeadScoreRepository.SaveChangesAsync();
        }

    }

}
