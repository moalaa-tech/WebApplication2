using AutoMapper;
using CRM.Domain.Entities.Banking;
using CRM.WebApp.DTOs.Banking;
using CRM.WebApp.ViewModels.Banking;

namespace CRM.WebApp.MappingProfiles
{
    public class ReconciliationProfile : Profile
    {
        public ReconciliationProfile()
        {
            CreateMap<Reconciliation, ReconciliationViewModel>()
                .ForMember(dest => dest.BankName, opt => opt.MapFrom(src => src.BankAccount.BankName));


            CreateMap<CreateReconciliationDto, Reconciliation>();


            CreateMap<CreateReconciliationViewModel, CreateReconciliationDto>();


            CreateMap<ReconciliationItem, ReconciliationItemViewModel>()
                .ForMember(dest => dest.TransactionRef, opt => opt.MapFrom(src => src.BankTransaction.ReferenceNumber));
        }
    }
}
