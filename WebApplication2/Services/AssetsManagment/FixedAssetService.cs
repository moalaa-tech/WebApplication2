using AutoMapper;
using CRM.Domain.Entities.AssetsManagment;
using CRM.Domain.Enums.AssetsManagment;
using CRM.WebApp.DTOs.AssetsManagment;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.AssetsManagment
{
    public class FixedAssetService : IFixedAssetService
    {
        private readonly IRepository<FixedAsset> _assetRepository;
        private readonly IRepository<DepreciationSchedule> _depreciationRepository;
        private readonly IMapper _mapper;

        public FixedAssetService(
            IRepository<FixedAsset> assetRepository,
            IRepository<DepreciationSchedule> depreciationRepository,
            IMapper mapper)
        {
            _assetRepository = assetRepository;
            _depreciationRepository = depreciationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FixedAssetDto>> GetAllAssetsAsync()
        {
            var assets = await _assetRepository.GetAll()
                .Include(a => a.ParentAsset)
                .Include(a => a.ChildAssets)
                .Include(a => a.DepreciationSchedules)
                .OrderBy(a => a.AssetNumber)
                .ToListAsync();

            return _mapper.Map<IEnumerable<FixedAssetDto>>(assets);
        }

        public async Task<FixedAssetDto> GetAssetByIdAsync(int id)
        {
            var asset = await _assetRepository.GetAll()
                .Include(a => a.ParentAsset)
                .Include(a => a.ChildAssets)
                .Include(a => a.DepreciationSchedules)
                .FirstOrDefaultAsync(a => a.Id == id);

            return _mapper.Map<FixedAssetDto>(asset);
        }

        public async Task<FixedAssetDto> CreateAssetAsync(CreateFixedAssetDto createDto)
        {
            // Check if asset number already exists
            var existingAsset = await _assetRepository.GetAll()
                .FirstOrDefaultAsync(a => a.AssetNumber == createDto.AssetNumber);

            if (existingAsset != null)
            {
                throw new InvalidOperationException("An asset with this number already exists.");
            }

            var asset = _mapper.Map<FixedAsset>(createDto);
            await _assetRepository.AddAsync(asset);
            await _assetRepository.SaveChangesAsync();

            return _mapper.Map<FixedAssetDto>(asset);
        }

        public async Task<FixedAssetDto> UpdateAssetAsync(UpdateFixedAssetDto updateDto)
        {
            var asset = await _assetRepository.GetByIdAsync(updateDto.Id);
            if (asset == null)
            {
                throw new KeyNotFoundException("Asset not found.");
            }

            // Check for asset number conflict
            if (asset.AssetNumber != updateDto.AssetNumber)
            {
                var existingAsset = await _assetRepository.GetAll()
                    .FirstOrDefaultAsync(a => a.AssetNumber == updateDto.AssetNumber && a.Id != updateDto.Id);

                if (existingAsset != null)
                {
                    throw new InvalidOperationException("An asset with this number already exists.");
                }
            }

            _mapper.Map(updateDto, asset);
            asset.DateModified = DateTime.UtcNow;

            _assetRepository.Update(asset);
            await _assetRepository.SaveChangesAsync();

            return _mapper.Map<FixedAssetDto>(asset);
        }

        public async Task<bool> DeleteAssetAsync(int id)
        {
            var asset = await _assetRepository.GetByIdAsync(id);
            if (asset == null)
            {
                return false;
            }

            if (!await CanDeleteAssetAsync(id))
            {
                throw new InvalidOperationException("Cannot delete asset. It has child assets or depreciation schedules.");
            }

            _assetRepository.Delete(asset);
            await _assetRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CanDeleteAssetAsync(int assetId)
        {
            var hasChildAssets = await _assetRepository.GetAll().AnyAsync(a => a.ParentAssetId == assetId);
            var hasDepreciationSchedules = await _depreciationRepository.GetAll().AnyAsync(d => d.FixedAssetId == assetId);

            return !hasChildAssets && !hasDepreciationSchedules;
        }

        public async Task<decimal> CalculateMonthlyDepreciationAsync(int assetId)
        {
            var asset = await _assetRepository.GetByIdAsync(assetId);
            if (asset == null)
            {
                throw new KeyNotFoundException("Asset not found.");
            }

            return CalculateDepreciation(asset);
        }

        private decimal CalculateDepreciation(FixedAsset asset)
        {
            decimal depreciableBase = asset.AcquisitionCost - asset.SalvageValue;

            return asset.DepreciationMethod switch
            {
                DepreciationMethod.StraightLine => depreciableBase / asset.UsefulLife,
                DepreciationMethod.DecliningBalance => CalculateDecliningBalance(asset, 1.5m),
                DepreciationMethod.DoubleDecliningBalance => CalculateDecliningBalance(asset, 2.0m),
                DepreciationMethod.SumOfYearsDigits => CalculateSumOfYearsDigits(asset),
                //DepreciationMethod.UnitsOfProduction => 0, // Requires additional data
                _ => depreciableBase / asset.UsefulLife
            };
        }


        private decimal CalculateDepreciation(FixedAsset asset, DateTime asOfDate)
        {
            if (asOfDate < asset.AcquisitionDate)
                return 0;

            var monthsOwned = ((asOfDate.Year - asset.AcquisitionDate.Year) * 12) +
                             asOfDate.Month - asset.AcquisitionDate.Month;

            monthsOwned = Math.Max(0, Math.Min(monthsOwned, asset.UsefulLife));

            switch (asset.DepreciationMethod)
            {
                case DepreciationMethod.StraightLine:
                    return CalculateStraightLineDepreciation(asset, monthsOwned);

                case DepreciationMethod.DecliningBalance:
                    return CalculateDecliningBalanceDepreciation(asset, monthsOwned);

                // Add other depreciation methods here

                default:
                    return CalculateStraightLineDepreciation(asset, monthsOwned);
            }
        }

        private decimal CalculateDecliningBalanceDepreciation(FixedAsset asset, int monthsOwned)
        {
            // Simplified implementation - adjust rate as needed
            double rate = 1.5 / asset.UsefulLife; // 150% declining balance
            decimal bookValue = asset.AcquisitionCost;
            decimal totalDepreciation = 0;

            for (int i = 0; i < monthsOwned; i++)
            {
                decimal monthlyDepreciation = bookValue * (decimal)rate;
                totalDepreciation += monthlyDepreciation;
                bookValue -= monthlyDepreciation;

                if (bookValue <= asset.SalvageValue)
                    break;
            }

            return totalDepreciation;
        }

        private decimal CalculateStraightLineDepreciation(FixedAsset asset, int monthsOwned)
        {
            var depreciableAmount = asset.AcquisitionCost - asset.SalvageValue;
            var monthlyDepreciation = depreciableAmount / asset.UsefulLife;
            return monthlyDepreciation * monthsOwned;
        }

        private decimal CalculateDecliningBalance(FixedAsset asset, decimal rate)
        {
            decimal bookValue = asset.CurrentBookValue;
            return bookValue * (rate / asset.UsefulLife);
        }

        private decimal CalculateSumOfYearsDigits(FixedAsset asset)
        {
            int remainingLife = asset.RemainingLife;
            int sumOfYears = (asset.UsefulLife * (asset.UsefulLife + 1)) / 2;
            decimal depreciableBase = asset.AcquisitionCost - asset.SalvageValue;

            return depreciableBase * (remainingLife / (decimal)sumOfYears);
        }

        public async Task<bool> GenerateDepreciationSchedulesAsync(int assetId)
        {
            var asset = await _assetRepository.GetByIdAsync(assetId);
            if (asset == null)
            {
                return false;
            }

            var existingSchedules = await _depreciationRepository.GetAll()
                .Where(d => d.FixedAssetId == assetId)
                .OrderByDescending(d => d.ScheduleDate)
                .ToListAsync();

            DateTime startDate = existingSchedules.Any()
                ? existingSchedules.Max(d => d.ScheduleDate).AddMonths(1)
                : asset.AcquisitionDate;

            decimal accumulatedDepreciation = existingSchedules.Any()
                ? existingSchedules.Where(d => d.IsPosted).Sum(d => d.DepreciationAmount)
                : 0;

            decimal currentBookValue = asset.AcquisitionCost - accumulatedDepreciation;

            for (int i = 0; i < asset.RemainingLife; i++)
            {
                DateTime scheduleDate = startDate.AddMonths(i);
                decimal depreciationAmount = CalculateDepreciation(asset);

                // Ensure we don't depreciate below salvage value
                if (currentBookValue - depreciationAmount < asset.SalvageValue)
                {
                    depreciationAmount = currentBookValue - asset.SalvageValue;
                }

                accumulatedDepreciation += depreciationAmount;
                currentBookValue -= depreciationAmount;

                var schedule = new DepreciationSchedule
                {
                    FixedAssetId = assetId,
                    ScheduleDate = scheduleDate,
                    DepreciationAmount = depreciationAmount,
                    AccumulatedDepreciation = accumulatedDepreciation,
                    BookValue = currentBookValue,
                    IsPosted = false,
                    Notes = $"Auto-generated for {scheduleDate:yyyy-MM}"
                };

                await _depreciationRepository.AddAsync(schedule);
            }
            await _depreciationRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DepreciationScheduleDto>> GetAssetDepreciationSchedulesAsync(int assetId)
        {
            var schedules = await _depreciationRepository.GetAll()
                .Include(d => d.FixedAsset)
                .Where(d => d.FixedAssetId == assetId)
                .OrderBy(d => d.ScheduleDate)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DepreciationScheduleDto>>(schedules);
        }

        public async Task<bool> PostDepreciationScheduleAsync(int scheduleId)
        {
            var schedule = await _depreciationRepository.GetByIdAsync(scheduleId);
            if (schedule == null)
            {
                return false;
            }

            schedule.IsPosted = true;
            schedule.DateModified = DateTime.UtcNow;

            _depreciationRepository.Update(schedule);
            await _depreciationRepository.SaveChangesAsync();
            return  true;
        }

        public async Task<IEnumerable<FixedAssetDto>> GetChildAssetsAsync(int parentAssetId)
        {
            var assets = await _assetRepository.GetAll()
                .Include(a => a.ParentAsset)
                .Include(a => a.DepreciationSchedules)
                .Where(a => a.ParentAssetId == parentAssetId)
                .OrderBy(a => a.AssetNumber)
                .ToListAsync();

            return _mapper.Map<IEnumerable<FixedAssetDto>>(assets);
        }

        public async Task<string> GenerateAssetNumberAsync()
        {
            var lastAsset = await _assetRepository.GetAll()
                .OrderByDescending(a => a.DateModified)
                .FirstOrDefaultAsync();

            int nextNumber = 1;
            if (lastAsset != null && int.TryParse(lastAsset.AssetNumber, out int lastNumber))
            {
                nextNumber = lastNumber + 1;
            }

            return nextNumber.ToString("D6"); // 6-digit format
        }

        public async Task<decimal> CalculateDepreciationAsync(int assetId, DateTime asOfDate)
        {
            var asset = await _assetRepository.GetByIdAsync(assetId);
            if (asset == null)
            {
                throw new KeyNotFoundException("Asset not found.");
            }

            return CalculateDepreciation(asset, asOfDate);
        }

        public async Task<IEnumerable<DepreciationScheduleDto>> GetDepreciationScheduleAsync(int assetId)
        {
            var schedules = await _depreciationRepository.GetAll()
                .Include(ds => ds.FixedAsset)
                .Where(ds => ds.FixedAssetId == assetId)
                .OrderBy(ds => ds.ScheduleDate)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DepreciationScheduleDto>>(schedules);
        }

        public Task<bool> GenerateDepreciationScheduleAsync(int assetId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PostDepreciationAsync(int scheduleId)
        {
            throw new NotImplementedException();
        }

      
    }
}
