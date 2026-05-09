using AutoMapper;
using CRM.Domain.Entities.Banking;
using CRM.WebApp.DTOs.Banking;

namespace CRM.WebApp.MappingProfiles.Accounting
{
    public class BankTransactionProfile : Profile
    {
        public BankTransactionProfile()
        {
            CreateMap<BankTransaction, BankTransactionDto>().ReverseMap();
            CreateMap<CreateBankTransactionDto, BankTransaction>();
            CreateMap<Reconciliation, ReconciliationDto>().ReverseMap();
            CreateMap<ReconciliationItem, ReconciliationItemDto>().ReverseMap();
        }
    }
}
