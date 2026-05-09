using AutoMapper;
using CRM.Domain.Entities.AssetsManagment;
using CRM.WebApp.DTOs.AssetsManagment;

namespace CRM.WebApp.MappingProfiles
{
    public class FixedAssetProfile : Profile
    {
        public FixedAssetProfile()
        {
            CreateMap<FixedAsset, FixedAssetDto>()
                .ForMember(dest => dest.DepreciationMethodName, opt => opt.MapFrom(src => src.DepreciationMethod.ToString()))
                .ForMember(dest => dest.ParentAssetName, opt => opt.MapFrom(src => src.ParentAsset != null ? src.ParentAsset.Name : null))
                .ForMember(dest => dest.ChildAssetsCount, opt => opt.MapFrom(src => src.ChildAssets != null ? src.ChildAssets.Count : 0))
                .ForMember(dest => dest.DepreciationSchedulesCount, opt => opt.MapFrom(src => src.DepreciationSchedules != null ? src.DepreciationSchedules.Count : 0));
               // .ForMember(dest => dest.MonthlyDepreciation, opt => opt.MapFrom(src => src.CalculateMonthlyDepreciation())
                

            CreateMap<CreateFixedAssetDto, FixedAsset>();
            CreateMap<UpdateFixedAssetDto, FixedAsset>();

            CreateMap<DepreciationSchedule, DepreciationScheduleDto>()
                .ForMember(dest => dest.AssetNumber, opt => opt.MapFrom(src => src.FixedAsset.AssetNumber))
                .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => src.FixedAsset.Name));
        }

    }
}
