using CRM.WebApp.DTOs.MarketingAutomation.Segment;

namespace CRM.WebApp.Services.MarketingAutomation.Segment
{
    public interface ISegmentService
    {
        Task<IEnumerable<SegmentDto>> GetAllSegmentsAsync();
        Task<SegmentDto> GetSegmentByIdAsync(int id);
        Task<SegmentDto> AddSegmentAsync(SegmentCreateDto segmentCreateDto);
        Task UpdateSegmentAsync(SegmentUpdateDto segmentUpdateDto);
        Task DeleteSegmentAsync(int id);
        Task<bool> SegmentExistsAsync(int id);
    }
}
