using AutoMapper;
using CRM.Domain.Entities;
using CRM.WebApp.DTOs.HumanResources;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace CRM.WebApp.Services.Lookups
{
    public class CountryService : ICountryService
    {
        private readonly IRepository<Country> CountryRepository;

        private readonly IMapper Mapper;
        private readonly ICacheService CacheService;

        public CountryService(IRepository<Country> _CountryRepository, IMapper mapper, ICacheService cacheService)
        {
            CountryRepository = _CountryRepository;
            Mapper = mapper;
            CacheService = cacheService;
        }

        public async Task<List<CountryDto>> GetAllAsync()
        {
            return await CacheService.GetOrCreateAsync(
                CacheExtensions.COUNTRIES_KEY,
                async () =>
                {
                    var entities = await CountryRepository.GetAll().ToListAsync();
                    return Mapper.Map<List<CountryDto>>(entities);
                },
                TimeSpan.FromHours(2)) ?? new List<CountryDto>();
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
            CacheService.Remove(CacheExtensions.COUNTRIES_KEY);
            return Mapper.Map<CountryDto>(entity);
        }

        public async Task<CountryDto> UpdateAsync(CountryDto dto)
        {
            var entity = await CountryRepository.GetByIdAsync(dto.Id);
            if (entity == null) throw new Exception("Country not found");

            Mapper.Map(dto, entity);
            await CountryRepository.SaveChangesAsync();
            CacheService.Remove(CacheExtensions.COUNTRIES_KEY);
            return Mapper.Map<CountryDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await CountryRepository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Country not found");

            CountryRepository.Delete(entity);
            await CountryRepository.SaveChangesAsync();
            CacheService.Remove(CacheExtensions.COUNTRIES_KEY);
        }
    }

}
