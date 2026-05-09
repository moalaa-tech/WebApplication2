using AutoMapper;
using CRM.Domain.Entities.Accounting;
using CRM.WebApp.DTOs.Accounting;

namespace CRM.WebApp.MappingProfiles.Accounting
{
    public class ExpenseProfile : Profile
    {
        public ExpenseProfile()
        {
            CreateMap<Expense, ExpenseDto>()
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category != null ? s.Category.Name : null));
            CreateMap<CreateExpenseDto, Expense>();


            CreateMap<ExpenseCategory, ExpenseCategoryDto>();
            CreateMap<ExpenseCategoryCreateDto, ExpenseCategory>();
            CreateMap<ExpenseCategoryUpdateDto, ExpenseCategory>();
        }
    }
}
