using CRM.WebApp.DTOs.HumanResources.LeaveRequest;

namespace CRM.WebApp.Services.HumanResources
{
    public interface ILeaveRequestService
    {
        Task<IEnumerable<LeaveRequestDto>> GetAllLeaveRequestsAsync();
        Task<LeaveRequestDto> GetLeaveRequestByIdAsync(int id);
        Task<LeaveRequestDto> CreateLeaveRequestAsync(CreateLeaveRequestDto createDto);
        Task<bool> UpdateLeaveRequestAsync(int id, UpdateLeaveRequestDto updateDto);
        Task<bool> DeleteLeaveRequestAsync(int id);
    }
}
