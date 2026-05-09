using AutoMapper;
using CRM.Domain.Entities.DocumentManagement;
using CRM.WebApp.DTOs.DocumentManagement;
using CRM.WebApp.ViewModels.DocumentManagement;

namespace CRM.WebApp.MappingProfiles.DocumentManagement
{
    public class DocumentMappingProfile : Profile
    {
        public DocumentMappingProfile()
        {
            // Entity to DTO
            CreateMap<Document, DocumentDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.FolderName, opt => opt.MapFrom(src => src.Folder.Name))
                .ForMember(dest => dest.FileSizeFormatted, opt => opt.MapFrom(src => FormatFileSize(src.FileSize)));

            CreateMap<DocumentCategory, DocumentCategoryDto>()
                .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.ParentCategory.Name));

            CreateMap<DocumentFolder, DocumentFolderDto>()
                .ForMember(dest => dest.ParentFolderName, opt => opt.MapFrom(src => src.ParentFolder.Name));

            // DTO to ViewModel
            CreateMap<DocumentDto, DocumentViewModel>();
            //CreateMap<DocumentCategoryDto, DocumentCategoryViewModel>();
            //CreateMap<DocumentFolderDto, DocumentFolderViewModel>();

            // ViewModel to DTO
            CreateMap<CreateDocumentViewModel, CreateDocumentDto>();
            CreateMap<EditDocumentViewModel, UpdateDocumentDto>();

            CreateMap<DocumentShare, DocumentShareDto>()
                .ForMember(dest => dest.DocumentTitle, opt => opt.MapFrom(src => src.Document.Title))
                .ForMember(dest => dest.SharedWithUserName, opt => opt.MapFrom(src => src.SharedWithUser.UserName))
                .ForMember(dest => dest.SharedByUserName, opt => opt.MapFrom(src => src.SharedByUser.UserName));
        }

        


        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            double len = bytes;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }
    }
}
