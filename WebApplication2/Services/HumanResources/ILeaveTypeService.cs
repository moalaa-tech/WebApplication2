using CRM.WebApp.DTOs.HumanResources.LeaveType;

namespace CRM.WebApp.Services.HumanResources
{
    public interface ILeaveTypeService
    {
        Task<IEnumerable<LeaveTypeDto>> GetAllLeaveTypesAsync();
        Task<LeaveTypeDto> GetLeaveTypeByIdAsync(int id);
        Task<LeaveTypeDto> CreateLeaveTypeAsync(CreateLeaveTypeDto createLeaveTypeDto);
        Task<bool> UpdateLeaveTypeAsync(UpdateLeaveTypeDto updateLeaveTypeDto);
        Task<bool> DeleteLeaveTypeAsync(int id);
    }
}
