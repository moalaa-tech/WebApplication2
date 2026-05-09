using AutoMapper;
using CRM.Domain.IdentityEntity;
using CRM.WebApp.DTOs.Register;
using CRM.WebApp.ViewModels.ApplicationUser;

namespace CRM.WebApp.MappingProfiles
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            // DTOs to Entities
            CreateMap<RegisterDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)); // Ensure UserName is set

            // Entities to DTOs
            CreateMap<ApplicationUser, ApplicationUserDto>();

            // ViewModels to DTOs
            CreateMap<RegisterViewModel, RegisterDto>();
            CreateMap<LoginViewModel, LoginDto>();

            // DTOs to ViewModels (if needed for displaying data)
            // CreateMap<ApplicationUserDto, UserProfileViewModel>();
        }
    }
}
