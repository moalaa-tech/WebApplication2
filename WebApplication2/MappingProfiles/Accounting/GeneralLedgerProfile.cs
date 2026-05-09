using AutoMapper;
using CRM.Domain.Entities.Accounting;
using CRM.WebApp.DTOs.Accounting;
using CRM.WebApp.ViewModels.Accounting;

namespace CRM.WebApp.MappingProfiles.Accounting
{
    public class GeneralLedgerProfile : Profile
    {
        public GeneralLedgerProfile()
        {
            CreateMap<GLAccount, GeneralLedgerDTO>().ReverseMap();
            CreateMap<GeneralLedgerDTO, GeneralLedgerViewModel>().ReverseMap();
            // Add mappings for other entities


            CreateMap<CreateGeneralLedgerDto, CreateGeneralLedgerViewModel>().ReverseMap();
            CreateMap<GLAccount, CreateGeneralLedgerDto>().ReverseMap();

            
        }
    }
}
