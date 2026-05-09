using CRM.WebApp.DTOs.HumanResources.Payroll;

namespace CRM.WebApp.Services.HumanResources
{
    public interface IPayrollService
    {
        Task<IEnumerable<PayrollDto>> GetPayrollsByPeriodAsync(DateTime startDate, DateTime endDate);
        Task ProcessPayrollAsync(CreatePayrollDto dto);
    }
}
