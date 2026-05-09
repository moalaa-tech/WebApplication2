using AutoMapper;
using CRM.Domain.Entities.Banking;
using CRM.WebApp.DTOs.Banking;
using CRM.WebApp.ViewModels.Banking;

namespace CRM.WebApp.MappingProfiles.Accounting
{
    public class BankAccountProfile : Profile
    {
        public BankAccountProfile()
        {
            CreateMap<BankAccount, BankAccountDto>().ReverseMap();
            CreateMap<BankAccount, BankAccountViewModel>().ReverseMap();
            CreateMap<CreateBankAccountDto, BankAccount>();
            CreateMap<UpdateBankAccountDto, BankAccount>();
            CreateMap<CreateBankAccountViewModel, CreateBankAccountDto>();
            CreateMap<BankAccountViewModel, BankAccountDto>();
        }
    }
}
