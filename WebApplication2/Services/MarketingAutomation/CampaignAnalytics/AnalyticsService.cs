using AutoMapper;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.WebApp.DTOs.MarketingAutomation.CampaignAnalytics;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.MarketingAutomation.CampaignAnalytics
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IRepository<CRM.Domain.Entities.MarketingAutomation.CampaignAnalytics> _analyticsRepository;
        private readonly IMapper _mapper;

        public AnalyticsService(IRepository<CRM.Domain.Entities.MarketingAutomation.CampaignAnalytics> analyticsRepository, IMapper mapper)
        {
            _analyticsRepository = analyticsRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CampaignAnalyticsDto>> GetAllAnalyticsAsync()
        {
            var analytics = await _analyticsRepository.GetAllAsync().ToListAsync();
            return _mapper.Map<IEnumerable<CampaignAnalyticsDto>>(analytics);
        }

        public async Task<CampaignAnalyticsDto> GetAnalyticsByIdAsync(int id)
        {
            var analytics = await _analyticsRepository.GetByIdAsync(id);
            return _mapper.Map<CampaignAnalyticsDto>(analytics);
        }

        public async Task<IEnumerable<CampaignAnalyticsDto>> GetAnalyticsByCampaignIdAsync(int campaignId)
        {
            var analytics = await _analyticsRepository.GetByCondition(a => a.CampaignId == campaignId).FirstOrDefaultAsync();
            return _mapper.Map<IEnumerable<CampaignAnalyticsDto>>(analytics);
        }

        public async Task<IEnumerable<CampaignAnalyticsDto>> GetAnalyticsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var analytics = await _analyticsRepository.GetByCondition(a => a.StartDate >= startDate && a.EndDate <= endDate)
                .OrderBy(a => a.StartDate)
                              .ToListAsync(); ;
            return _mapper.Map<IEnumerable<CampaignAnalyticsDto>>(analytics);
        }

        public async Task<IEnumerable<CampaignAnalyticsDto>> GetAnalyticsByChannelAsync(string channel)
        {
            var analytics = await _analyticsRepository.GetByCondition(a => a.Channel == channel).ToListAsync();
            return _mapper.Map<IEnumerable<CampaignAnalyticsDto>>(analytics);
        }

        public async Task<CampaignAnalyticsDto> AddAnalyticsAsync(CampaignAnalyticsCreateDto analyticsCreateDto)
        {
            var analytics = _mapper.Map<CRM.Domain.Entities.MarketingAutomation.CampaignAnalytics>(analyticsCreateDto);
            analytics.RecordedDate = DateTime.UtcNow;
            analytics.ROIPercentage = CalculateROI(analytics.RevenueGenerated, analytics.TotalSpent);

            await _analyticsRepository.AddAsync(analytics);
            return _mapper.Map<CampaignAnalyticsDto>(analytics);
        }

        public async Task UpdateAnalyticsAsync(CampaignAnalyticsUpdateDto analyticsUpdateDto)
        {
            var analytics = await _analyticsRepository.GetByIdAsync(analyticsUpdateDto.Id);
            if (analytics == null)
                throw new ArgumentException("Analytics record not found");

            _mapper.Map(analyticsUpdateDto, analytics);
            analytics.LastUpdated = DateTime.UtcNow;
            analytics.ROIPercentage = CalculateROI(analytics.RevenueGenerated, analytics.TotalSpent);

            _analyticsRepository.Update(analytics);
        }

        public async Task DeleteAnalyticsAsync(int id)
        {
            var analytics = await _analyticsRepository.GetByIdAsync(id);
            if (analytics == null)
                throw new ArgumentException("Analytics record not found");

            _analyticsRepository.Delete(analytics);
        }

        public async Task<decimal> CalculateTotalROIAsync()
        {
            var analytics = await _analyticsRepository.GetAllAsync().ToListAsync();
            if (!analytics.Any()) return 0;

            decimal totalRevenue = analytics.Sum(a => a.RevenueGenerated);
            decimal totalSpent = analytics.Sum(a => a.TotalSpent);

            totalSpent = totalSpent == 0 ? 0 : ((totalRevenue - totalSpent) / totalSpent) * 100;


            return totalSpent;
        }

        public async Task<decimal> CalculateChannelROIAsync(string channel)
        {
            var analytics = await _analyticsRepository.GetByCondition(a => a.Channel == channel).ToListAsync();
            if (!analytics.Any()) return 0;

            decimal channelRevenue = analytics.Sum(a => a.RevenueGenerated);
            decimal channelSpent = analytics.Sum(a => a.TotalSpent);
            channelSpent = channelSpent == 0 ? 0 : ((channelRevenue - channelSpent) / channelSpent) * 100;
            return channelSpent;
        }

        public async Task<Dictionary<string, decimal>> GetChannelPerformanceMetricsAsync()
        {
            var channels = new[] { "Social", "Email", "PPC", "SEO", "Direct" };
            var metrics = new Dictionary<string, decimal>();

            foreach (var channel in channels)
            {
                var roi = await CalculateChannelROIAsync(channel);
                metrics.Add(channel, roi);
            }

            return metrics;
        }

        private decimal CalculateROI(decimal revenue, decimal cost)
        {
            return cost == 0 ? 0 : ((revenue - cost) / cost) * 100;
        }

    }
}
