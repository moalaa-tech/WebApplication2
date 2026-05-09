using AutoMapper;
using CRM.Domain.Entities.Banking;
using CRM.WebApp.DTOs.Banking;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.Finance_Accounting
{
    public class BankTransactionService : IBankTransactionService
    {
        private readonly IRepository<BankTransaction> _repository;
        private readonly IMapper _mapper;

        public BankTransactionService(IRepository<BankTransaction> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BankTransactionDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync().ToListAsync();
            return _mapper.Map<IEnumerable<BankTransactionDto>>(entities);
        }

        public async Task<BankTransactionDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<BankTransactionDto>(entity);
        }

        public async Task CreateAsync(CreateBankTransactionDto dto)
        {
            var entity = _mapper.Map<BankTransaction>(dto);
            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            _repository.Delete(entity);
        }
    }
}
