using CRM.WebApp.DTOs.HumanResources.Department;

namespace CRM.WebApp.Services.HumanResources
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<DepartmentDto> GetDepartmentByIdAsync(int id);
        Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto);
        Task<bool> UpdateDepartmentAsync(UpdateDepartmentDto updateDepartmentDto);
        Task<bool> DeleteDepartmentAsync(int id);
        Task<bool> DepartmentExistsAsync(int id);
    }
}
