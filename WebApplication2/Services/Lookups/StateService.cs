using AutoMapper;
using CRM.Domain.Entities;
using CRM.WebApp.DTOs.HumanResources;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace CRM.WebApp.Services.Lookups
{
    public class StateService : IStateService
    {
        private readonly IRepository<State> StateRepository;

        private readonly IMapper Mapper;

        public StateService(IRepository<State> _StateRepository, IMapper mapper)
        {
            StateRepository = _StateRepository;
            Mapper = mapper;
        }

        public async Task<List<StateDto>> GetAllAsync()
        {
            var entities = await StateRepository.GetAll().Include(s => s.Country).ToListAsync();
            return Mapper.Map<List<StateDto>>(entities);
        }

        public async Task<StateDto?> GetByIdAsync(int id)
        {
            var entity = await StateRepository.GetAll().Include(s => s.Country)
                                              .FirstOrDefaultAsync(s => s.Id == id);
            return entity == null ? null : Mapper.Map<StateDto>(entity);
        }

        public async Task<StateDto> CreateAsync(StateDto dto)
        {
            var entity = Mapper.Map<State>(dto);
            await StateRepository.AddAsync(entity);
            await StateRepository.SaveChangesAsync();
            return Mapper.Map<StateDto>(entity);
        }

        public async Task<StateDto> UpdateAsync(StateDto dto)
        {
            var entity =  StateRepository.GetAll().FirstOrDefault(a=> a.Id == dto.Id);
            if (entity == null) throw new Exception("State not found");

            Mapper.Map(dto, entity);
            await StateRepository.SaveChangesAsync();
            return Mapper.Map<StateDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = StateRepository.GetAll().FirstOrDefault(a => a.Id == id);
            if (entity == null) throw new Exception("State not found");

            StateRepository.Delete(entity);
            await StateRepository.SaveChangesAsync();
        }
    }

}
