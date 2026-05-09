using AutoMapper;
using CRM.Domain.Entities.DocumentManagement;
using CRM.WebApp.DTOs.DocumentManagement;

namespace CRM.WebApp.MappingProfiles.DocumentManagement
{
    public class DocumentFolderProfile : Profile
    {
        public DocumentFolderProfile()
        {
            CreateMap<DocumentFolder, DocumentFolderDto>()
                .ForMember(dest => dest.ParentFolderName, opt => opt.MapFrom(src => src.ParentFolder != null ? src.ParentFolder.Name : "Root"))
                .ForMember(dest => dest.DocumentCount, opt => opt.MapFrom(src => src.Documents != null ? src.Documents.Count : 0))
                .ForMember(dest => dest.SubFolderCount, opt => opt.MapFrom(src => src.SubFolders != null ? src.SubFolders.Count : 0));

            CreateMap<CreateDocumentFolderDto, DocumentFolder>();
            CreateMap<UpdateDocumentFolderDto, DocumentFolder>();

            CreateMap<DocumentFolder, FolderTreeDto>()
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.SubFolders))
                .ForMember(dest => dest.DocumentCount, opt => opt.MapFrom(src => src.Documents != null ? src.Documents.Count : 0));
        }
    }
}
