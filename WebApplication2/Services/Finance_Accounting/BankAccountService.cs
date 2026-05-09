using AutoMapper;
using CRM.Domain.Entities.Banking;
using CRM.WebApp.DTOs.Banking;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.Finance_Accounting
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IRepository<BankAccount> _repository;
        private readonly IMapper _mapper;

        public BankAccountService(IRepository<BankAccount> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BankAccountDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync().ToListAsync();
            return _mapper.Map<IEnumerable<BankAccountDto>>(entities);
        }

        public async Task<BankAccountDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<BankAccountDto>(entity);
        }

        public async Task CreateAsync(CreateBankAccountDto dto)
        {
            var entity = _mapper.Map<BankAccount>(dto);
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(UpdateBankAccountDto dto)
        {
            var entity = _mapper.Map<BankAccount>(dto);
            _repository.Update(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            _repository.Delete(entity);
        }
    }
}
