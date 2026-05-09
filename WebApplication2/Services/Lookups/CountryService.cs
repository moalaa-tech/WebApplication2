using AutoMapper;
using CRM.Domain.Entities;
using CRM.WebApp.DTOs.HumanResources;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace CRM.WebApp.Services.Lookups
{
    public class CountryService : ICountryService
    {
        private readonly IRepository<Country> CountryRepository;

        private readonly IMapper Mapper;

        public CountryService(IRepository<Country> _CountryRepository, IMapper mapper)
        {
            CountryRepository = _CountryRepository;
            Mapper = mapper;
        }

        public async Task<List<CountryDto>> GetAllAsync()
        {
            var entities = await CountryRepository.GetAll().ToListAsync();
            return Mapper.Map<List<CountryDto>>(entities);
        }

        public async Task<CountryDto?> GetByIdAsync(int id)
        {
            var entity = await CountryRepository.GetByIdAsync(id);
            return entity == null ? null : Mapper.Map<CountryDto>(entity);
        }

        public async Task<CountryDto> CreateAsync(CountryDto dto)
        {
            var entity = Mapper.Map<Country>(dto);
            await CountryRepository.AddAsync(entity);
            await CountryRepository.SaveChangesAsync();
            return Mapper.Map<CountryDto>(entity);
        }

        public async Task<CountryDto> UpdateAsync(CountryDto dto)
        {
            var entity = await CountryRepository.GetByIdAsync(dto.Id);
            if (entity == null) throw new Exception("Country not found");

            Mapper.Map(dto, entity);
            await CountryRepository.SaveChangesAsync();
            return Mapper.Map<CountryDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await CountryRepository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Country not found");

            CountryRepository.Delete(entity);
            await CountryRepository.SaveChangesAsync();
        }
    }

}
