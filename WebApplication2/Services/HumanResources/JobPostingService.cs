using AutoMapper;
using CRM.Domain.Entities.HR;
using CRM.WebApp.DTOs.HumanResources.JobPosting;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CRM.WebApp.Services.HumanResources
{
    public class JobPostingService : IJobPostingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorage;
        private readonly IRepository<JobPosting> JobPostingRepository;

        public JobPostingService(IUnitOfWork unitOfWork, IMapper mapper, IRepository<JobPosting> jobPostingRepository, IFileStorageService fileStorage)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileStorage = fileStorage;
            JobPostingRepository = jobPostingRepository;
        }

        public async Task<IEnumerable<JobPostingDto>> GetAllJobPostingsAsync(bool activeOnly = true)
        {
            Expression<Func<JobPosting, bool>> filter = activeOnly ?
                jp => jp.IsActive && jp.ClosingDate >= DateTime.Today :
                jp => true;

            var postings = await JobPostingRepository.GetAllAsync(a => a.Department, a => a.Applications).Where(filter).ToListAsync();
            return _mapper.Map<IEnumerable<JobPostingDto>>(postings);
        }

        public async Task<JobPostingDto> CreateJobPostingAsync(CreateJobPostingDto dto)
        {
            var posting = _mapper.Map<JobPosting>(dto);
            await JobPostingRepository.AddAsync(posting);
            await JobPostingRepository.SaveChangesAsync();
            return _mapper.Map<JobPostingDto>(posting);
        }

        public async Task CloseJobPostingAsync(int id)
        {
            var posting = await JobPostingRepository.GetByIdAsync(id);
            //if (posting == null) throw new NotFoundException("Job posting not found");

            posting.IsActive = false;
            JobPostingRepository.Update(posting);
            await JobPostingRepository.SaveChangesAsync();
        }

        public Task<JobPostingDto> GetJobPostingByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateJobPostingAsync(int id, CreateJobPostingDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteJobPostingAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}