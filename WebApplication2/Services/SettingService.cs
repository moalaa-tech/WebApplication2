using AutoMapper;
using CRM.Domain.Entities;
using CRM.WebApp.DTOs.Setting;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services
{
    public class SettingService : ISettingService
    {
        private readonly IRepository<Setting> _settingRepository;
        private readonly IMapper _mapper;

        public SettingService(IRepository<Setting> settingRepository, IMapper mapper)
        {
            _settingRepository = settingRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SettingDto>> GetAllSettingsAsync()
        {
            var settings = await _settingRepository.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<SettingDto>>(settings);
        }

        public async Task<SettingDto> GetSettingByIdAsync(int id)
        {
            var setting = await _settingRepository.GetByIdAsync(id);
            return _mapper.Map<SettingDto>(setting);
        }

        public async Task<int> CreateSettingAsync(CreateSettingDto createSettingDto)
        {
            var setting = _mapper.Map<Setting>(createSettingDto);
            await _settingRepository.AddAsync(setting);
            await _settingRepository.SaveChangesAsync();
            return setting.Id;
        }

        public async Task<bool> UpdateSettingAsync(UpdateSettingDto updateSettingDto)
        {
            var setting = await _settingRepository.GetByIdAsync(updateSettingDto.Id);
            if (setting == null)
            {
                throw new KeyNotFoundException($"Setting with ID {updateSettingDto.Id} not found.");
            }

            _mapper.Map(updateSettingDto, setting); // Map DTO properties onto the existing entity
            _settingRepository.Update(setting);
            await _settingRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSettingAsync(int id)
        {
            var setting = await _settingRepository.GetByIdAsync(id);
            if (setting == null)
            {
                throw new KeyNotFoundException($"Setting with ID {id} not found.");
            }

            _settingRepository.Delete(setting);
            await _settingRepository.SaveChangesAsync();
            return true;
        }
    }
}
