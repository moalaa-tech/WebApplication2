using AutoMapper;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.WebApp.DTOs.MarketingAutomation.Segment;

namespace CRM.WebApp.MappingProfiles.MarketingAutomation
{
    public class SegmentProfile : Profile
    {
        public SegmentProfile()
        {
            CreateMap<Segment, SegmentDto>();
            CreateMap<SegmentCreateDto, Segment>();
            CreateMap<SegmentUpdateDto, Segment>();
            CreateMap<Segment, SegmentUpdateDto>();
        }
    }
}
