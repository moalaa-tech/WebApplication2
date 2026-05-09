using AutoMapper;
using CRM.Domain.Entities.HR;
using CRM.WebApp.DTOs.HumanResources.Department;
using CRM.WebApp.DTOs.HumanResources.Employee;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.HumanResources
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IRepository<Employee> employeeRepository, IRepository<Department> departmentRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync(a => a.Department).ToListAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdWithIncludeAsync(a => a.Id == id, x => x.Department);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<int> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto)
        {
            var employee = _mapper.Map<Employee>(createEmployeeDto);
            await _employeeRepository.AddAsync(employee);
            await _employeeRepository.SaveChangesAsync();
            return employee.Id;
        }

        public async System.Threading.Tasks.Task UpdateEmployeeAsync(UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = await _employeeRepository.GetByIdAsync(updateEmployeeDto.Id);
            if (employee == null)
            {
                // Handle not found, e.g., throw an exception
                throw new KeyNotFoundException($"Employee with ID {updateEmployeeDto.Id} not found.");
            }

            _mapper.Map(updateEmployeeDto, employee);
            _employeeRepository.Update(employee);
            await _employeeRepository.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteEmployeeAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                // Handle not found
                throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }

            _employeeRepository.Delete(employee);
            await _employeeRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var departments = await _departmentRepository.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesWithDepartmentAsync()
        {
            var employees = await _employeeRepository.GetAllAsync(e => e.Department.Manager).ToListAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }
    }
}
