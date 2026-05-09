using CRM.WebApp.DTOs.AssetsManagment;

namespace CRM.WebApp.Services.AssetsManagment
{
    public interface IFixedAssetService
    {
        Task<IEnumerable<FixedAssetDto>> GetAllAssetsAsync();
        Task<FixedAssetDto> GetAssetByIdAsync(int id);
        Task<FixedAssetDto> CreateAssetAsync(CreateFixedAssetDto createDto);
        Task<FixedAssetDto> UpdateAssetAsync(UpdateFixedAssetDto updateDto);
        Task<bool> DeleteAssetAsync(int id);
        Task<string> GenerateAssetNumberAsync();
        Task<IEnumerable<FixedAssetDto>> GetChildAssetsAsync(int parentAssetId);
        Task<decimal> CalculateDepreciationAsync(int assetId, DateTime asOfDate);
        Task<IEnumerable<DepreciationScheduleDto>> GetDepreciationScheduleAsync(int assetId);
        Task<bool> GenerateDepreciationScheduleAsync(int assetId, DateTime startDate, DateTime endDate);
        Task<bool> PostDepreciationAsync(int scheduleId);
    }
}
