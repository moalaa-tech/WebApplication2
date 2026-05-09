using AutoMapper;
using CRM.Domain.Entities.AccountsReceivable;
using CRM.WebApp.DTOs.Customer;
using CRM.WebApp.Paging;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.CustomerSupport_Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(IRepository<Customer> customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync(int pageNumber, int pageSize)
        {
            var customers = await _customerRepository.GetAll().ToListAsync();
            var dtoR = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return PaginatedList<CustomerDto>.Create(dtoR, pageNumber, pageSize);
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<int> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            var customer = _mapper.Map<Customer>(createCustomerDto);
            await _customerRepository.AddAsync(customer);
            await _customerRepository.SaveChangesAsync();
            return customer.Id;
        }

        public async Task<bool> UpdateCustomerAsync(UpdateCustomerDto updateCustomerDto)
        {
            var customer = await _customerRepository.GetByIdAsync(updateCustomerDto.Id);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {updateCustomerDto.Id} not found.");
            }

            _mapper.Map(updateCustomerDto, customer);
            _customerRepository.Update(customer);
            await _customerRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            }

            _customerRepository.Delete(customer);
            await _customerRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAll().ToListAsync();
            var dtoR = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return dtoR;
        }
    }
}
