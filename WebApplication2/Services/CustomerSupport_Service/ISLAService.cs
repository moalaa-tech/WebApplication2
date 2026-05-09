using CRM.WebApp.DTOs.CustomerService;

namespace CRM.WebApp.Services.CustomerSupport_Service
{
    public interface ISLAService
    {
        Task<int> CreateSLAAsync(SLACreateDto slaDto);
        Task UpdateSLAAsync(SLAUpdateDto slaDto);
        Task<IEnumerable<SLADto>> GetAllActiveSLAsAsync();
        Task<SLADetailDto> GetSLADetailsAsync(int id);
        Task ToggleSLAStatusAsync(int id, bool isActive);
        Task<SLADto> GetApplicableSLAAsync(string serviceType);
        Task<int> StartSLAMonitoringAsync(SLAMonitoringStartDto monitoringDto);
        Task RecordSLAResponseAsync(int metricId);
        Task CompleteSLAMonitoringAsync(int metricId, SLAMonitoringCompleteDto completeDto);


    }
}
