using AutoMapper;
using CRM.Domain.Entities.SupplyChainManagement;
using CRM.WebApp.DTOs.SupplyChainManagement.ForecastData;
using CRM.WebApp.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.SupplyChainManagement
{
    public class ForecastDataService : IForecastDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public ForecastDataService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ForecastDataDto>> GetAllForecastDataAsync()
        {
            var forecastData = await _unitOfWork.Repository<ForecastData>().GetAll()
                .Include(f => f.DemandPlan)
                .OrderByDescending(f => f.DataDate)
                .ThenBy(f => f.DataType)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ForecastDataDto>>(forecastData);
        }

        public async Task<ForecastDataDto> GetForecastDataByIdAsync(int id)
        {
            var forecastData = await _unitOfWork.Repository<ForecastData>().GetAll()
                .Include(f => f.DemandPlan)
                .Include(f => f.SupplyChainEvent)
                .Include(f => f.Item)
                .Include(f => f.Shipping)
                .Include(f => f.Freight)
                .Include(f => f.HistoricalData)
                .FirstOrDefaultAsync(f => f.Id == id);

            return _mapper.Map<ForecastDataDto>(forecastData);
        }

        public async Task<ForecastDataDto> CreateForecastDataAsync(CreateForecastDataDto createDto)
        {
            var forecastData = _mapper.Map<ForecastData>(createDto);

            await _unitOfWork.Repository<ForecastData>().AddAsync(forecastData);
            await _unitOfWork.Repository<ForecastData>().SaveChangesAsync();

            // Reload with related data
            var createdData = await _unitOfWork.Repository<ForecastData>().GetAll()
                .Include(f => f.DemandPlan)
                .FirstOrDefaultAsync(f => f.Id == forecastData.Id);

            return _mapper.Map<ForecastDataDto>(createdData);
        }

        public async Task UpdateForecastDataAsync(UpdateForecastDataDto updateDto)
        {
            var forecastData = await _unitOfWork.Repository<ForecastData>().GetByIdAsync(updateDto.Id);
            if (forecastData == null)
                throw new ArgumentException("Forecast data not found");

            _mapper.Map(updateDto, forecastData);
            forecastData.DateModified = DateTime.UtcNow;

            _unitOfWork.Repository<ForecastData>().Update(forecastData);
            await _unitOfWork.Repository<ForecastData>().SaveChangesAsync();
        }

        public async Task DeleteForecastDataAsync(int id)
        {
            var forecastData = await _unitOfWork.Repository<ForecastData>().GetByIdAsync(id);
            if (forecastData != null)
            {
                _unitOfWork.Repository<ForecastData>().Remove(forecastData);
                await _unitOfWork.Repository<ForecastData>().SaveChangesAsync();
            }
        }

        public async Task<bool> ForecastDataExistsAsync(int id)
        {
            return await _unitOfWork.Repository<ForecastData>().GetAll().AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<ForecastDataDto>> GetFilteredForecastDataAsync(string dataType, DateTime? startDate, DateTime? endDate, int? demandPlanId)
        {
            IQueryable<ForecastData> query = _unitOfWork.Repository<ForecastData>().GetAll()
                .Include(f => f.DemandPlan);

            if (!string.IsNullOrEmpty(dataType))
            {
                query = query.Where(f => f.DataType == dataType);
            }

            if (startDate.HasValue)
            {
                query = query.Where(f => f.DataDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(f => f.DataDate <= endDate.Value);
            }

            if (demandPlanId.HasValue)
            {
                query = query.Where(f => f.DemandPlanId == demandPlanId.Value);
            }

            var results = await query
                .OrderBy(f => f.DataDate)
                .ThenBy(f => f.DataType)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ForecastDataDto>>(results);
        }

        public async Task<decimal> GetForecastSummaryAsync(string summaryType, DateTime? startDate, DateTime? endDate)
        {
            IQueryable<ForecastData> query = _unitOfWork.Repository<ForecastData>().GetAll();

            if (startDate.HasValue)
            {
                query = query.Where(f => f.DataDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(f => f.DataDate <= endDate.Value);
            }

            return summaryType.ToLower() switch
            {
                "total" => await query.SumAsync(f => f.Value),
                "average" => await query.AverageAsync(f => f.Value),
                "max" => await query.MaxAsync(f => f.Value),
                "min" => await query.MinAsync(f => f.Value),
                _ => await query.SumAsync(f => f.Value)
            };
        }
    }
}
