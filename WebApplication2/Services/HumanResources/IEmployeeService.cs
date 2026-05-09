using CRM.WebApp.DTOs.HumanResources.Department;
using CRM.WebApp.DTOs.HumanResources.Employee;

namespace CRM.WebApp.Services.HumanResources
{
    public interface IEmployeeService
    {

        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesWithDepartmentAsync();
        Task<int> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto);
        Task UpdateEmployeeAsync(UpdateEmployeeDto updateEmployeeDto);
        Task DeleteEmployeeAsync(int id);
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync(); // For dropdowns
    }
}
