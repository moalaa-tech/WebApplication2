using AutoMapper;
using CRM.Domain.Entities.HR;
using CRM.WebApp.DTOs.HumanResources.Department;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.HumanResources
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<Employee> _employeeRepository; // To get manager list for dropdowns
        private readonly IMapper _mapper;

        public DepartmentService(IRepository<Department> departmentRepository, IRepository<Employee> employeeRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto)
        {
            var department = _mapper.Map<Department>(createDepartmentDto);
            await _departmentRepository.AddAsync(department);
            await _departmentRepository.SaveChangesAsync();
            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);

            _departmentRepository.Delete(department);

            return true;
        }

        public async Task<bool> DepartmentExistsAsync(int id)
        {
            var IsExist = await _departmentRepository.GetByIdAsync(id);
            if (IsExist == null)
                return false;
            return true;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var departments = await _departmentRepository.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        }

        public async Task<DepartmentDto> GetDepartmentByIdAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task<bool> UpdateDepartmentAsync(UpdateDepartmentDto updateDepartmentDto)
        {
            var department = await _departmentRepository.GetByIdAsync(updateDepartmentDto.Id);

            //var department = _mapper.Map<Department>(updateDepartmentDto);
            department.ManagerId = updateDepartmentDto.ManagerId;
            department.Name = updateDepartmentDto.Name;
            department.Description = updateDepartmentDto.Description;
            department.Location = updateDepartmentDto.Location;
            department.Budget = updateDepartmentDto.Budget;
            department.EstablishedDate = updateDepartmentDto.EstablishedDate;


            _departmentRepository.Update(department);
            await _departmentRepository.SaveChangesAsync();
            return true;
        }
    }
}
