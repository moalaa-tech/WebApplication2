using AutoMapper;
using CRM.Domain.Entities.HR;
using CRM.WebApp.DTOs.HumanResources.JobApplication;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.HumanResources
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorage;
        private readonly IRepository<JobPosting> JobPostingRepository;
        private readonly IRepository<JobApplication> JobApplicationRepository;

        public JobApplicationService(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorage, IRepository<JobPosting> _JobPostingRepository, IRepository<JobApplication> _JobApplicationRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileStorage = fileStorage;
            JobPostingRepository = _JobPostingRepository;
            JobApplicationRepository = _JobApplicationRepository;
        }

        public async Task<JobApplicationDto> SubmitApplicationAsync(CreateJobApplicationDto dto, IFormFile resume, IFormFile coverLetter)
        {
            var jobPosting = await JobPostingRepository.GetByIdAsync(dto.JobPostingId);
            //if (jobPosting == null) throw new NotFoundException("Job posting not found");

            var application = _mapper.Map<JobApplication>(dto);

            // Save files
            application.ResumePath = await _fileStorage.SaveFileAsync(resume, "resumes");
            if (coverLetter != null)
            {
                application.CoverLetterPath = await _fileStorage.SaveFileAsync(coverLetter, "coverletters");
            }

            await JobApplicationRepository.AddAsync(application);
            await JobApplicationRepository.SaveChangesAsync();

            return _mapper.Map<JobApplicationDto>(application);
        }

        public async Task UpdateApplicationStatusAsync(int id, UpdateJobApplicationDto dto)
        {
            var application = await JobApplicationRepository.GetByIdAsync(id);
            //if (application == null) throw new NotFoundException("Application not found");

            application.Status = dto.Status;
            application.InterviewDate = dto.InterviewDate;
            application.Notes = dto.Notes;

            JobApplicationRepository.Update(application);
            await JobApplicationRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<JobApplicationDto>> GetApplicationsForJobAsync(int jobPostingId)
        {
            var applications = await JobApplicationRepository.GetAllAsync(a => a.JobPosting).Where(ja => ja.JobPostingId == jobPostingId).ToListAsync();

            return _mapper.Map<IEnumerable<JobApplicationDto>>(applications);
        }

        public async Task<IEnumerable<JobApplicationDto>> GetAllApplicationsAsync()
        {
            var applications = await JobApplicationRepository
                .GetAllAsync(a => a.JobPosting)
                .OrderByDescending(a => a.ApplicationDate)
                .ToListAsync();

            return _mapper.Map<IEnumerable<JobApplicationDto>>(applications);
        }

        public async Task<JobApplicationDto?> GetApplicationByIdAsync(int id)
        {
            var application = await JobApplicationRepository
                .GetAllAsync(a => a.JobPosting)
                .FirstOrDefaultAsync(a => a.Id == id);

            return application == null ? null : _mapper.Map<JobApplicationDto>(application);
        }
    }
}
