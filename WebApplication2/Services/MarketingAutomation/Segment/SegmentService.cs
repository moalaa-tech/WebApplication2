using AutoMapper;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.WebApp.DTOs.MarketingAutomation.Segment;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.MarketingAutomation.Segment
{
    public class SegmentService : ISegmentService
    {
        private readonly IRepository<CRM.Domain.Entities.MarketingAutomation.Segment> _segmentRepository;
        private readonly IMapper _mapper;

        public SegmentService(IRepository<CRM.Domain.Entities.MarketingAutomation.Segment> segmentRepository, IMapper mapper)
        {
            _segmentRepository = segmentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SegmentDto>> GetAllSegmentsAsync()
        {
            var segments = await _segmentRepository.GetAllAsync().ToListAsync();
            return _mapper.Map<IEnumerable<SegmentDto>>(segments);
        }

        public async Task<SegmentDto> GetSegmentByIdAsync(int id)
        {
            var segment = await _segmentRepository.GetByIdAsync(id);
            return _mapper.Map<SegmentDto>(segment);
        }

        public async Task<SegmentDto> AddSegmentAsync(SegmentCreateDto segmentCreateDto)
        {
            var segment = _mapper.Map<CRM.Domain.Entities.MarketingAutomation.Segment>(segmentCreateDto);

            await _segmentRepository.AddAsync(segment);
            return _mapper.Map<SegmentDto>(segment);
        }

        public async Task UpdateSegmentAsync(SegmentUpdateDto segmentUpdateDto)
        {
            var segment = await _segmentRepository.GetByIdAsync(segmentUpdateDto.Id);
            if (segment == null)
                throw new ArgumentException("Segment not found");

            _mapper.Map(segmentUpdateDto, segment);
            segment.LastModifiedDate = DateTime.UtcNow;

             _segmentRepository.Update(segment);
        }

        public async Task DeleteSegmentAsync(int id)
        {
            var segment = await _segmentRepository.GetByIdAsync(id);
            if (segment == null)
                throw new ArgumentException("Segment not found");

             _segmentRepository.Delete(segment);
        }

        public async Task<bool> SegmentExistsAsync(int id)
        {
            return await _segmentRepository.ExistsAsync(s => s.Id == id);
        }
    }
}
