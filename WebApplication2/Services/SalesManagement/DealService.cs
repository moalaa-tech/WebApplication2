using AutoMapper;
using CRM.Domain.Entities.SalesManagement;
using CRM.WebApp.DTOs.Deal;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace CRM.WebApp.Services.SalesManagement
{
    public class DealService : IDealService
    {
        private readonly IRepository<Deal> _repository;
        private readonly IRepository<DealFile> DealFileRepository;

        private readonly IMapper _mapper;
        private readonly ILogger<DealService> _logger;

        public DealService(IRepository<Deal> repository, IRepository<DealFile> _DealFileRepository, IMapper mapper, ILogger<DealService> logger)
        {
            _repository = repository;
            DealFileRepository = _DealFileRepository;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<IEnumerable<DealDto>> GetAllDealsAsync()
        {
            try
            {
                var deals = await _repository.GetAllAsync().ToListAsync();
                return _mapper.Map<IEnumerable<DealDto>>(deals);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all deals");
                throw;
            }
        }

        public async Task<DealDto> GetDealByIdAsync(int id)
        {
            try
            {
                var deal = await _repository.GetByIdAsync(id);
                return _mapper.Map<DealDto>(deal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting deal with ID {id}");
                throw;
            }
        }

        public async Task<DealDto> CreateDealAsync(CreateDealDto createDto)
        {
            try
            {
                var deal = _mapper.Map<Deal>(createDto);
                await _repository.AddAsync(deal);

                _logger.LogInformation($"Deal {deal.Id} created successfully");
                return _mapper.Map<DealDto>(deal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new deal");
                throw;
            }
        }


        public async Task<bool> UpdateDealAsync(int id, UpdateDealDto updateDto)
        {
            try
            {
                var deal = await _repository.GetByIdAsync(id);
                if (deal == null)
                {
                    throw new KeyNotFoundException($"Deal with ID {id} not found");
                }

                _mapper.Map(updateDto, deal);

                // Handle file deletions
                foreach (var fileId in updateDto.FilesToDelete)
                {
                    var dealFile = await DealFileRepository.GetByIdAsync(id);
                    DealFileRepository.Delete(dealFile);
                }

                _repository.Update(deal);
                _logger.LogInformation($"Deal {id} updated successfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating deal {id}");
                throw;
            }
        }

        public async Task<bool> DeleteDealAsync(int id)
        {
            try
            {
                var deal = await _repository.GetByIdAsync(id);

                _repository.Delete(deal);
                _logger.LogInformation($"Deal {id} deleted successfully");
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting deal {id}");
                throw;
            }
        }



        public async Task<string> SaveFileAsync(IFormFile file, string webRootPath)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    throw new ArgumentException("File is empty");
                }

                var uploadsFolder = Path.Combine(webRootPath, "deal-files");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                _logger.LogInformation($"File {uniqueFileName} saved successfully");
                return uniqueFileName;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving file");
                throw;
            }
        }

        public Task<IEnumerable> GetDealsAvailableForQuoteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
