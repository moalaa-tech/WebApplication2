using CRM.WebApp.ViewModels.CustomerService;

namespace CRM.WebApp.Services.CustomerSupport_Service
{
    public interface ISupportAgentService
    {
        Task<IEnumerable<SupportAgentListViewModel>> GetAllAgentsAsync();
        Task<SupportAgentDetailViewModel> GetAgentByIdAsync(int id);
        Task<int> CreateAgentAsync(SupportAgentCreateViewModel viewModel);
        Task UpdateAgentAsync(SupportAgentEditViewModel viewModel);
        Task ToggleAgentStatusAsync(int id, bool isActive);
        Task<IEnumerable<DepartmentStatsViewModel>> GetDepartmentStatsAsync();
    }
}
