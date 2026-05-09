using AutoMapper;
using CRM.Domain.Entities.HR;
using CRM.WebApp.DTOs.HumanResources.JobTitle;
using CRM.WebApp.Repositories;
using CRM.WebApp.ViewModels.JobTitle;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.HumanResources
{
    public class JobTitleService : IJobTitleService
    {
        private readonly IRepository<JobTitle> _repo;
        private readonly IRepository<Department> _depRepo;
        private readonly IMapper _mapper;

        public JobTitleService(IRepository<JobTitle> repo, IRepository<Department> depRepo, IMapper mapper)
        {
            _repo = repo;
            _depRepo = depRepo;
            _mapper = mapper;
        }

        public async Task<List<JobTitleDto>> GetAllAsync()
        {
            var data = await _repo.GetAllAsync(includes: q => q.Department).ToListAsync();
            return data.Select(x => new JobTitleDto
            {
                Id = x.Id,
                Title = x.Title,
                DepartmentName = x.Department?.Name
            }).ToList();
        }


        public async Task<JobTitleDto> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            var model = _mapper.Map<JobTitleDto>(entity);
            model.Departments = await GetDepartments();

            return model;
        }

        public async Task<JobTitleViewModel> GetCreateModelAsync()
        {
            return new JobTitleViewModel
            {
                Departments = await GetDepartments()
            };
        }

        public async Task CreateAsync(CreateJobTitleDto model)
        {
            var entity = _mapper.Map<JobTitle>(model);
            await _repo.AddAsync(entity);
        }

        public async Task UpdateAsync(UpdateJobTitleDto model)
        {
            var entity = await _repo.GetByIdAsync(model.Id);
            if (entity == null) throw new Exception("Not found");
            _mapper.Map(model, entity);
            _repo.Update(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity != null)
                _repo.Delete(entity);
        }

        private async Task<IEnumerable<SelectListItem>> GetDepartments()
        {
            var deps = await _depRepo.GetAllAsync().ToListAsync();
            return deps.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name });
        }


    }
}
