using AutoMapper;
using CRM.Domain.Entities.HR;
using CRM.WebApp.DTOs.HumanResources.LeaveRequest;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.HumanResources
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly IRepository<LeaveRequest> _repository;
        private readonly IMapper _mapper;

        public LeaveRequestService(IRepository<LeaveRequest> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LeaveRequestDto>> GetAllLeaveRequestsAsync()
        {
            var leaveRequests = await _repository.GetAllAsync().ToListAsync();
            return _mapper.Map<IEnumerable<LeaveRequestDto>>(leaveRequests);
        }

        public async Task<LeaveRequestDto> GetLeaveRequestByIdAsync(int id)
        {
            var leaveRequest = await _repository.GetByIdAsync(id);
            return _mapper.Map<LeaveRequestDto>(leaveRequest);
        }

        public async Task<LeaveRequestDto> CreateLeaveRequestAsync(CreateLeaveRequestDto createDto)
        {
            var leaveRequest = _mapper.Map<LeaveRequest>(createDto);

            // Calculate number of days
            leaveRequest.NumberOfDays = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays + 1;
            leaveRequest.RequestedDate = DateTime.Now;
            leaveRequest.Status = Domain.Enums.HumanResources.LeaveStatus.Pending;

            await _repository.AddAsync(leaveRequest);
            return _mapper.Map<LeaveRequestDto>(leaveRequest);
        }

        public async Task<bool> UpdateLeaveRequestAsync(int id, UpdateLeaveRequestDto updateDto)
        {
            var leaveRequest = await _repository.GetByIdAsync(id);
            if (leaveRequest == null)
            {
                throw new Exception("Leave request not found");
            }

            _mapper.Map(updateDto, leaveRequest);

            if (updateDto.Status == "Approved" || updateDto.Status == "Rejected")
            {
                leaveRequest.ApprovedDate = DateTime.Now;
            }

            _repository.Update(leaveRequest);
            return true;

        }

        public async Task<bool> DeleteLeaveRequestAsync(int id)
        {
            var leaveRequest = await _repository.GetByIdAsync(id);

            _repository.Delete(leaveRequest);
            return true;
        }

    }
}
