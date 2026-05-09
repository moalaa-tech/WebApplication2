using AutoMapper;
using CRM.WebApp.DTOs.Accounting;
using CRM.WebApp.ViewModels.Accounting;

namespace CRM.WebApp.MappingProfiles.Accounting
{
    public class AccountsReceivableProfile : Profile
    {
        public AccountsReceivableProfile()
        {
            CreateMap<CreateAccountsReceivableViewModel, CreateAccountsReceivableDto>();
            CreateMap<AccountsReceivableDto, AccountsReceivableViewModel>().ReverseMap();
        }
    }
}