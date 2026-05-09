using AutoMapper;
using CRM.Domain.Entities.HR;
using CRM.WebApp.DTOs.HumanResources.LeaveType;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.HumanResources
{
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly IRepository<LeaveType> _leaveTypeRepository;
        private readonly IMapper _mapper;

        public LeaveTypeService(IRepository<LeaveType> leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LeaveTypeDto>> GetAllLeaveTypesAsync()
        {
            var leaveTypes = await _leaveTypeRepository.GetAllAsync().ToListAsync();
            return _mapper.Map<IEnumerable<LeaveTypeDto>>(leaveTypes);
        }

        public async Task<LeaveTypeDto> GetLeaveTypeByIdAsync(int id)
        {
            var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
            return _mapper.Map<LeaveTypeDto>(leaveType);
        }

        public async Task<LeaveTypeDto> CreateLeaveTypeAsync(CreateLeaveTypeDto createLeaveTypeDto)
        {
            var leaveType = _mapper.Map<LeaveType>(createLeaveTypeDto);
            await _leaveTypeRepository.AddAsync(leaveType);
            return _mapper.Map<LeaveTypeDto>(leaveType);
        }

        public async Task<bool> UpdateLeaveTypeAsync(UpdateLeaveTypeDto updateLeaveTypeDto)
        {
            var leaveType = await _leaveTypeRepository.GetByIdAsync(updateLeaveTypeDto.Id);
            if (leaveType == null)
            {
                throw new Exception("Leave type not found");
            }

            _mapper.Map(updateLeaveTypeDto, leaveType);
            leaveType.DateModified = DateTime.UtcNow;
            _leaveTypeRepository.Update(leaveType);
            return true;
        }

        public async Task<bool> DeleteLeaveTypeAsync(int id)
        {
            var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
            _leaveTypeRepository.Delete(leaveType);
            return true;
        }
    }
}
