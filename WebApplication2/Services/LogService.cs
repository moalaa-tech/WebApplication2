using AutoMapper;
using CRM.Domain.Entities;
using CRM.WebApp.DTOs.LoggingDto;
using CRM.WebApp.Paging;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services
{
    public class LogService : ILogService
    {
        private readonly IRepository<LogEntry> _logRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<LogService> _logger;

        public LogService(IRepository<LogEntry> logRepository, IMapper mapper, ILogger<LogService> logger)
        {
            _logRepository = logRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginatedList<LogDto>> GetPaginatedLogsAsync(LogFilterDto filter)
        {
            try
            {
                var query = _logRepository.GetAll();


                if (!string.IsNullOrEmpty(filter.Level))
                {
                    query = query.Where(l => l.Level == filter.Level);
                }

                if (!string.IsNullOrEmpty(filter.Search))
                {
                    query = query.Where(l =>
                        l.Message.Contains(filter.Search) ||
                        (l.Exception != null && l.Exception.Contains(filter.Search)) ||
                        l.Logger.Contains(filter.Search));
                }

                if (filter.FromDate.HasValue)
                {
                    query = query.Where(l => l.Timestamp >= filter.FromDate.Value);
                }

                if (filter.ToDate.HasValue)
                {
                    query = query.Where(l => l.Timestamp <= filter.ToDate.Value);
                }



                var totalCount = await query.CountAsync();

                var data = await query.OrderByDescending(l => l.Timestamp).Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
                var DTOs = _mapper.Map<List<LogDto>>(data);
                return PaginatedList<LogDto>.Create(DTOs, 1, 10);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting paginated logs");
                throw;
            }
        }

        public async Task<LogDto> GetLogByIdAsync(int id)
        {
            try
            {
                var log = await _logRepository.GetByIdAsync(id);
                return _mapper.Map<LogDto>(log);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting log by ID: {id}");
                throw;
            }
        }

        public async Task ClearLogsAsync(int daysToKeep)
        {
            try
            {
                var cutoffDate = DateTime.UtcNow.AddDays(-daysToKeep);
                var oldLogs = await _logRepository.GetAllAsync(l => l.Timestamp < cutoffDate).ToListAsync();
                _logRepository.RemoveRange(oldLogs);
                await _logRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error clearing logs older than {daysToKeep} days");
                throw;
            }
        }

        public async Task<LogSummaryDto> GetLogSummaryAsync()
        {
            try
            {
                return new LogSummaryDto
                {
                    TotalCount = await _logRepository.GetAll().CountAsync(),
                    InformationCount = await _logRepository.GetAll().CountAsync(l => l.Level == "Information"),
                    WarningCount = await _logRepository.GetAll().CountAsync(l => l.Level == "Warning"),
                    ErrorCount = await _logRepository.GetAll().CountAsync(l => l.Level == "Error"),
                    CriticalCount = await _logRepository.GetAll().CountAsync(l => l.Level == "Critical")
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting log summary");
                throw;
            }
        }

        public async Task<bool> CreateLogAsync(CreateLogDto dto)
        {
            LogEntry DTOs = _mapper.Map<LogEntry>(dto);
            await _logRepository.AddAsync(DTOs);
            await _logRepository.SaveChangesAsync();
            return true;
        }
    }
}
