using AutoMapper;
using CRM.Domain.Entities.HR;
using CRM.WebApp.DTOs.HumanResources.Payroll;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.HumanResources
{
    public class PayrollService : IPayrollService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<Payroll> PayrollRepository;
        private readonly IRepository<Employee> EmployeeRepository;

        public PayrollService(IUnitOfWork unitOfWork, IMapper mapper, IRepository<Payroll> _PayrollRepository, IRepository<Employee> employeeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            PayrollRepository = _PayrollRepository;
            EmployeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<PayrollDto>> GetPayrollsByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            var payrolls = await PayrollRepository.GetAllAsync(a => a.Employee).Where(p => p.PayPeriodStart >= startDate && p.PayPeriodEnd <= endDate).ToListAsync();

            return _mapper.Map<IEnumerable<PayrollDto>>(payrolls);
        }

        public async Task ProcessPayrollAsync(CreatePayrollDto dto)
        {
            var employee = await EmployeeRepository.GetByIdAsync(dto.EmployeeId);
            //if (employee == null) throw new NotFoundException("Employee not found");

            var payrollDto = new PayrollDto
            {
                EmployeeId = dto.EmployeeId,
                BasicSalary = dto.BasicSalary,
                Allowances = dto.Allowances,
                Deductions = dto.Deductions,
                TaxAmount = CalculateTax(dto.BasicSalary + dto.Allowances),
                PayPeriodStart = dto.PayPeriodStart,
                PayPeriodEnd = dto.PayPeriodEnd,
                PaymentDate = DateTime.UtcNow,
                PaymentMethod = dto.PaymentMethod,
                IsProcessed = true
            };

            payrollDto.NetSalary = payrollDto.BasicSalary + payrollDto.Allowances - payrollDto.Deductions - payrollDto.TaxAmount;

            var payroll = _mapper.Map<Payroll>(payrollDto);
            await PayrollRepository.AddAsync(payroll);
            await PayrollRepository.SaveChangesAsync();
        }

        private decimal CalculateTax(decimal grossSalary)
        {
            // Simplified tax calculation - replace with actual tax logic
            if (grossSalary <= 1000) return 0;
            if (grossSalary <= 5000) return grossSalary * 0.1m;
            return grossSalary * 0.2m;
        }
    }
}
