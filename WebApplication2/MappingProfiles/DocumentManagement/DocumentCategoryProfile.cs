using AutoMapper;
using CRM.Domain.Entities.DocumentManagement;
using CRM.WebApp.DTOs.DocumentManagement;
using CRM.WebApp.ViewModels.DocumentManagement;

namespace CRM.WebApp.MappingProfiles.DocumentManagement
{
    public class DocumentCategoryProfile
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<DocumentCategory, DocumentCategoryDto>()
                    .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.ParentCategory != null ? src.ParentCategory.Name : null))
                    .ForMember(dest => dest.DocumentCount, opt => opt.MapFrom(src => src.Documents != null ? src.Documents.Count : 0));


                CreateMap<DocumentCategory, DocumentCategoryViewModel>()
                    .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.ParentCategory != null ? src.ParentCategory.Name : null))
                    .ForMember(dest => dest.DocumentCount, opt => opt.MapFrom(src => src.Documents != null ? src.Documents.Count : 0));

                CreateMap<DocumentCategoryDto, DocumentCategoryViewModel>().ReverseMap();
                    

                CreateMap<CreateDocumentCategoryDto, DocumentCategory>();
                CreateMap<UpdateDocumentCategoryDto, DocumentCategory>();
            }
        }
    }
}
