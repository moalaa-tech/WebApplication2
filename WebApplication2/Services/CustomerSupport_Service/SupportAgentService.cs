using AutoMapper;
using CRM.Domain.Entities.CustomerService;
using CRM.WebApp.Repositories;
using CRM.WebApp.ViewModels.CustomerService;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.CustomerSupport_Service
{
    public class SupportAgentService : ISupportAgentService
    {
        private readonly IRepository<SupportAgent> _agentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SupportAgentService> _logger;

        public SupportAgentService(
            IRepository<SupportAgent> agentRepository,
            IMapper mapper,
            ILogger<SupportAgentService> logger)
        {
            _agentRepository = agentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<SupportAgentListViewModel>> GetAllAgentsAsync()
        {
            var agents = await _agentRepository.GetAllAsync(a => a.AssignedTickets).ToListAsync();
            return _mapper.Map<IEnumerable<SupportAgentListViewModel>>(agents);
        }

        public async Task<SupportAgentDetailViewModel> GetAgentByIdAsync(int id)
        {
            var agent = await _agentRepository.GetAllAsync(a => a.AssignedTickets).Where(a => a.Id == id).ToListAsync();
            return _mapper.Map<SupportAgentDetailViewModel>(agent.FirstOrDefault());
        }

        public async Task<int> CreateAgentAsync(SupportAgentCreateViewModel viewModel)
        {
            var agent = _mapper.Map<SupportAgent>(viewModel);
            agent.HireDate = DateTime.UtcNow;
            agent.IsActive = true;

            await _agentRepository.AddAsync(agent);
            await _agentRepository.SaveChangesAsync();

            return agent.Id;
        }

        public async Task UpdateAgentAsync(SupportAgentEditViewModel viewModel)
        {
            var agent = await _agentRepository.GetByIdAsync(viewModel.Id);
            if (agent == null)
            {
                throw new KeyNotFoundException("Agent not found");
            }

            _mapper.Map(viewModel, agent);
            agent.ModifiedDate = DateTime.UtcNow;

            _agentRepository.Update(agent);
            await _agentRepository.SaveChangesAsync();
        }

        public async Task ToggleAgentStatusAsync(int id, bool isActive)
        {
            var agent = await _agentRepository.GetByIdAsync(id);
            if (agent == null)
            {
                throw new KeyNotFoundException("Agent not found");
            }

            agent.IsActive = isActive;
            agent.ModifiedDate = DateTime.UtcNow;

            if (!isActive)
            {
                agent.TerminationDate = DateTime.UtcNow;
            }

            _agentRepository.Update(agent);
            await _agentRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<DepartmentStatsViewModel>> GetDepartmentStatsAsync()
        {
            var agents = await _agentRepository.GetAllAsync(a => a.AssignedTickets).ToListAsync();

            return agents
                .GroupBy(a => a.Department)
                .Select(g => new DepartmentStatsViewModel
                {
                    Department = g.Key,
                    AgentCount = g.Count(),
                    ActiveAgentCount = g.Count(a => a.IsActive),
                    AverageRating = g.Average(a => a.AverageRating),
                    TotalTicketsResolved = g.Sum(a => a.TotalTicketsResolved)
                });
        }
    }
}
