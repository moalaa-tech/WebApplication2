using AutoMapper;
using CRM.Domain.Entities.CustomerService;
using CRM.WebApp.DTOs.CustomerService;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.CustomerSupport_Service
{
    public class SLAService : ISLAService
    {
        private readonly IRepository<ServiceLevelAgreement> _slaRepository;
        private readonly IRepository<SLAMetrics> _metricsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SLAService> _logger;

        public SLAService(
            IRepository<ServiceLevelAgreement> slaRepository,
            IRepository<SLAMetrics> metricsRepository,
            IMapper mapper,
            ILogger<SLAService> logger)
        {
            _slaRepository = slaRepository;
            _metricsRepository = metricsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<SLADto>> GetAllActiveSLAsAsync()
        {
            try
            {
                var slas = await _slaRepository.GetAllAsync().Where(s => s.IsActive)
                    .OrderBy(s => s.ServiceType).ToListAsync();

                return _mapper.Map<IEnumerable<SLADto>>(slas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active SLAs");
                throw;
            }
        }

        public async Task<SLADetailDto> GetSLADetailsAsync(int id)
        {
            try
            {
                var sla = await _slaRepository.GetAllAsync(a => a.Metrics).Where(s => s.Id == id).ToListAsync();

                var slaDetail = _mapper.Map<SLADetailDto>(sla.FirstOrDefault());

                if (slaDetail != null)
                {
                    slaDetail.ComplianceRate = await CalculateComplianceRateAsync(id);
                }

                return slaDetail;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving SLA details for ID: {id}");
                throw;
            }
        }

        public async Task<int> CreateSLAAsync(SLACreateDto slaDto)
        {
            try
            {
                var sla = _mapper.Map<ServiceLevelAgreement>(slaDto);
                sla.CreatedDate = DateTime.UtcNow;
                sla.IsActive = true;

                await _slaRepository.AddAsync(sla);
                await _slaRepository.SaveChangesAsync();

                _logger.LogInformation($"Created new SLA with ID: {sla.Id}");
                return sla.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new SLA");
                throw;
            }
        }

        public async Task UpdateSLAAsync(SLAUpdateDto slaDto)
        {
            try
            {
                var existingSla = await _slaRepository.GetByIdAsync(slaDto.Id);
                if (existingSla == null)
                {
                    throw new KeyNotFoundException($"SLA with ID {slaDto.Id} not found");
                }

                _mapper.Map(slaDto, existingSla);
                existingSla.LastModified = DateTime.UtcNow;

                _slaRepository.Update(existingSla);
                await _slaRepository.SaveChangesAsync();

                _logger.LogInformation($"Updated SLA with ID: {slaDto.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating SLA with ID: {slaDto.Id}");
                throw;
            }
        }

        public async Task ToggleSLAStatusAsync(int id, bool isActive)
        {
            try
            {
                var sla = await _slaRepository.GetByIdAsync(id);
                if (sla == null)
                {
                    throw new KeyNotFoundException($"SLA with ID {id} not found");
                }

                sla.IsActive = isActive;
                sla.LastModified = DateTime.UtcNow;

                _slaRepository.Update(sla);
                await _slaRepository.SaveChangesAsync();

                _logger.LogInformation($"{(isActive ? "Activated" : "Deactivated")} SLA with ID: {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error toggling SLA status for ID: {id}");
                throw;
            }
        }

        public async Task<SLADto> GetApplicableSLAAsync(string serviceType)
        {
            try
            {
                var sla = await _slaRepository.GetAllAsync().Where(s => s.ServiceType == serviceType && s.IsActive)
                    .OrderByDescending(s => s.CreatedDate).ToListAsync();

                return _mapper.Map<SLADto>(sla.FirstOrDefault());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving SLA for service type: {serviceType}");
                throw;
            }
        }

        public async Task<int> StartSLAMonitoringAsync(SLAMonitoringStartDto monitoringDto)
        {
            try
            {
                var metric = new SLAMetrics
                {
                    SLAId = monitoringDto.SLAId,
                    TicketId = monitoringDto.TicketId,
                    ServiceRequestId = monitoringDto.ServiceRequestId,
                    StartTime = DateTime.UtcNow,
                    IsMet = false
                };

                await _metricsRepository.AddAsync(metric);
                await _metricsRepository.SaveChangesAsync();

                _logger.LogInformation($"Started SLA monitoring for {GetMonitoringType(monitoringDto)} with ID: {metric.Id}");

                return metric.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error starting SLA monitoring");
                throw;
            }
        }

        public async Task RecordSLAResponseAsync(int metricId)
        {
            try
            {
                var metric = await _metricsRepository.GetByIdAsync(metricId);
                if (metric == null)
                {
                    throw new KeyNotFoundException($"SLA metric with ID {metricId} not found");
                }

                metric.ResponseTime = DateTime.UtcNow;

                _metricsRepository.Update(metric);
                await _metricsRepository.SaveChangesAsync();

                _logger.LogInformation($"Recorded response time for SLA metric ID: {metricId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error recording SLA response for metric ID: {metricId}");
                throw;
            }
        }

        public async Task CompleteSLAMonitoringAsync(int metricId, SLAMonitoringCompleteDto completeDto)
        {
            try
            {
                var metric = await _metricsRepository.GetByIdAsync(metricId);
                if (metric == null)
                {
                    throw new KeyNotFoundException($"SLA metric with ID {metricId} not found");
                }

                metric.ResolutionTime = DateTime.UtcNow;
                metric.IsMet = completeDto.IsMet;
                metric.Notes = completeDto.Notes;

                _metricsRepository.Update(metric);
                await _metricsRepository.SaveChangesAsync();

                _logger.LogInformation($"Completed SLA monitoring for metric ID: {metricId}. Met SLA: {completeDto.IsMet}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error completing SLA monitoring for metric ID: {metricId}");
                throw;
            }
        }

        private async Task<double> CalculateComplianceRateAsync(int slaId)
        {
            var metrics = await _metricsRepository.GetAllAsync().Where(m => m.SLAId == slaId && m.ResolutionTime.HasValue)
                .ToListAsync();

            if (!metrics.Any()) return 0;

            var metCount = metrics.Count(m => m.IsMet);
            return (double)metCount / metrics.Count() * 100;
        }

        private string GetMonitoringType(SLAMonitoringStartDto dto)
        {
            if (dto.TicketId.HasValue) return "ticket";
            if (dto.ServiceRequestId.HasValue) return "service request";
            return "unknown";
        }

       
    }
}
